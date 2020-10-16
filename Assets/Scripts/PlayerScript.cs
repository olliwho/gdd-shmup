using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// 1 - The speed of the ship
    /// </summary>
    public Vector2 speed = new Vector2(10, 10);
    public Transform explosionPrefab;

    // 2 - Store the movement and the component
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private AmmoCounterScript ammoCounter;
    
    //----------

    private void Start()
    {
	    ammoCounter = FindObjectOfType<AmmoCounterScript>();
    }

    void Update()
    {
        // 3 - Retrieve axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // 4 - Movement per direction
        movement = new Vector2(
          speed.x * inputX,
          speed.y * inputY);
          
        // 5 - Shooting
		bool shoot = Input.GetButtonDown("Fire1");
		//shoot |= Input.GetButtonDown("Fire2");
		// Careful: For Mac users, ctrl + arrow is a bad idea
		bool areaShoot = Input.GetButtonDown("Fire2");
		//areaShoot |= Input.GetButtonDown("Fire1");

		if (shoot)
		{
			WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
				// false because the player is not an enemy
				weapon.Attack(false);
				SoundEffectsHelper.Instance.MakeShotSound();
			}
		}
		
		if (areaShoot)
		{
			AreaWeaponScript weapon = GetComponent<AreaWeaponScript>();
			if (weapon != null)
			{
				// false because the player is not an enemy
				weapon.Attack(false);
			}
		}
		
		var currentPos = transform.position;
		
		// 6 - Make sure we are not outside the camera bounds
		var dist = (currentPos - Camera.main.transform.position).z;

		var leftBorder = Camera.main.ViewportToWorldPoint(
		  new Vector3(0, 0, dist)
		).x;

		var rightBorder = Camera.main.ViewportToWorldPoint(
		  new Vector3(1, 0, dist)
		).x;

		var topBorder = Camera.main.ViewportToWorldPoint(
		  new Vector3(0, 0, dist)
		).y;

		var bottomBorder = Camera.main.ViewportToWorldPoint(
		  new Vector3(0, 1, dist)
		).y;

		transform.position = new Vector3(
		  Mathf.Clamp(currentPos.x, leftBorder, rightBorder),
		  Mathf.Clamp(currentPos.y, topBorder, bottomBorder),
		  currentPos.z
		);

    }

    void FixedUpdate()
    {
        // 5 - Get the component and store the reference
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        // 6 - Move the game object
        rigidbodyComponent.velocity = movement;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
	{
		bool damagePlayer = false;

		// Collision with enemy
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
		if (enemy != null)
		{
			// Kill the enemy
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);
			
			//make explosion
			enemyHealth.ExplosionAnimation(explosionPrefab);
			SoundEffectsHelper.Instance.MakeDamageSound();


			damagePlayer = true;
		}

		// Damage the player
		if (damagePlayer)
		{
			HealthScript playerHealth = GetComponent<HealthScript>();
			if (playerHealth != null) playerHealth.Damage(1);
		}
		
		
		// Collision with area shot ammo
		AreaAmmoScript ammo = collision.gameObject.GetComponent<AreaAmmoScript>();
		if(ammo != null)
		{
			AreaWeaponScript weapon = GetComponent<AreaWeaponScript>();
			if(weapon)
			{
				weapon.ammunition += 1;
				ammoCounter.IncreaseCounter();
			}
			Destroy(ammo.gameObject);
		}
		
		// Collision with time stop
		TimeStopScript timeStop = collision.gameObject.GetComponent<TimeStopScript>();
		if(timeStop != null)
		{
			// disable box collider
			BoxCollider2D bc = timeStop.gameObject.GetComponent<BoxCollider2D>();
			if (bc != null) bc.enabled = false;

			// disable renderer
			SpriteRenderer sr = timeStop.gameObject.GetComponent<SpriteRenderer>();
			if (sr != null) sr.enabled = false;
			
			timeStop.StopTime();
		}

	}
	
	void OnDestroy()
	{
		// Game Over.
		var gameOver = FindObjectOfType<GameOverScript>(); 
		if(gameOver) gameOver.ShowGui();
	}
}
