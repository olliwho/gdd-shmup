using UnityEngine;

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

  /// <summary>
  /// Inflicts damage and check if the object should be destroyed
  /// </summary>
  /// <param name="damageCount"></param>
  public void Damage(int damageCount)
  {
    hp -= damageCount;

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
          Debug.Log(hitColliders.Length);
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
          var explosionTransform = Instantiate(shot.ExplosionPrefab) as Transform;
          // Assign position
          explosionTransform.position = transform.position;
        }
      }
    }
  }
}
