using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
  /// <summary>
  /// Total hitpoints
  /// </summary>
  public int hp = 1;

  /// <summary>
  /// Enemy or player?
  /// </summary>
  public bool isEnemy = true;

  private Text hpText;
  private ScoreScript scoreScript;
  private void Start()
  {
    if (!isEnemy)
    {
      var scoreCanvas = GameObject.Find("ScoreCanvas");
      Text[] texts = scoreCanvas.GetComponentsInChildren<Text>();
      foreach (var t in texts)
      {
        if (t.name == "Lives") hpText = t;
      }

      hpText.text = hp.ToString();
    }
    else
    {
      scoreScript = FindObjectOfType<ScoreScript>();
    }
  }

  /// <summary>
  /// Inflicts damage and check if the object should be destroyed
  /// </summary>
  /// <param name="damageCount"></param>
  public void Damage(int damageCount)
  {
    hp -= damageCount;
    if (!isEnemy)
    {
      hpText.text = hp.ToString();
    }
    else
    {
      scoreScript.UpdateScore(10);
    }

    if (hp <= 0)
    {
      // Dead!
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D otherCollider)
  {
    // Is this a shot?
    ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
    if (shot != null)
    {
      // Avoid friendly fire
      if (shot.isEnemyShot != isEnemy)
      {
        Damage(shot.damage);

        // Destroy the shot
        Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
        
        if (shot.areaDamage)
        {
          // 'Splosion!
          SpecialEffectsHelper.Instance.Explosion(transform.position);
          Vector3 center = transform.position;
          
          Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, shot.explosionArea);
          foreach (var hitCollider in hitColliders)
          {
            // Collision with enemy
            EnemyScript enemy = hitCollider.gameObject.GetComponent<EnemyScript>();
            if (enemy != null)
            {
              // Damage the enemy
              HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
              if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);
            }
          }
          
        }
        else
        {
          ExplosionAnimation(shot.ExplosionPrefab);
        }
      }
    }
  }

  public void ExplosionAnimation(Transform explosion)
  {
    var explosionTransform = Instantiate(explosion) as Transform;
    // Assign position
    explosionTransform.position = transform.position;
  }
}
