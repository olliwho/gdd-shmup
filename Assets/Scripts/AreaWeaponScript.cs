using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class AreaWeaponScript : MonoBehaviour
{
  //--------------------------------
  // 1 - Designer variables
  //--------------------------------

  /// <summary>
  /// Projectile prefab for shooting
  /// </summary>
  public Transform shotPrefab;

  /// <summary>
  /// Cooldown in seconds between two shots
  /// </summary>
  public float shootingRate = 0.25f;
  
  public int ammunition;

  private AmmoCounterScript ammoCounter;

  //--------------------------------
  // 2 - Cooldown
  //--------------------------------

  private float shootCooldown;

  void Start()
  {
    shootCooldown = 0f;
    ammoCounter = FindObjectOfType<AmmoCounterScript>();
  }

  void Update()
  {
    if (shootCooldown > 0)
    {
      shootCooldown -= Time.deltaTime;
    }
  }

  //--------------------------------
  // 3 - Shooting from another script
  //--------------------------------

  /// <summary>
  /// Create a new projectile if possible
  /// </summary>
  public void Attack(bool isEnemy)
  {
    if (CanAttack)
    {
      SoundEffectsHelper.Instance.MakeShotSound();
      shootCooldown = shootingRate;
      ammunition -= 1;

      var shotTransform = Instantiate(shotPrefab) as Transform;


      // Assign position
      shotTransform.position = transform.position;

      // The is enemy property
      ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
      if (shot != null)
      {
        shot.isEnemyShot = isEnemy;
      }

      // Make the weapon shot always towards it
      MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
      if (move != null)
      {
        move.direction = transform.right; // towards in 2D space is the right of the sprite
      }
      
      ammoCounter.DecreaseCounter();
    }
  }

  /// <summary>
  /// Is the weapon ready to create a new projectile?
  /// </summary>
  public bool CanAttack
  {
    get
    {
      return ((shootCooldown <= 0f) && (ammunition > 0));
    }
  }
}

