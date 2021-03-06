using UnityEngine;

/// <summary>
/// Projectile behavior
/// </summary>
public class ShotScript : MonoBehaviour
{
  // 1 - Designer variables

  /// <summary>
  /// Damage inflicted
  /// </summary>
  public int damage = 1;

  /// <summary>
  /// Projectile damage player or enemies?
  /// </summary>
  public bool isEnemyShot = false;
  
  public bool areaDamage = false;

  public Transform ExplosionPrefab;
  public float explosionArea = 2.0f;

  void Start()
  {
    // 2 - Limited time to live to avoid any leak
    Destroy(gameObject, 20); // 20sec
  }
}
