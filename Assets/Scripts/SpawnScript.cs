using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform ammoPrefab;
    public Transform timeStopPrefab;

    public int maxEnemy = 5;
    public int maxAmmo = 2;
    public int maxTime = 1;
    
    public int currentEnemy = 0;
    public int currentAmmo = 0;
    public int currentTime = 0;

    public float cooldown;
    
    private float height;
    private float width;


    private void Start()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            height = 2f * cam.orthographicSize;
            width = height * cam.aspect;
        }

        cooldown = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }
        
        // Same for the right edge
        float right = transform.position.x + width/2;

        // Randomly pick a point within the spawn object
        if (currentEnemy < maxEnemy)
        {
            Vector2 spawnPoint = new Vector2(right, Random.Range(-(height/2), (height/2)));
            var enemyTransform = Instantiate(enemyPrefab);
            enemyTransform.position = spawnPoint;
            currentEnemy++;
        }
        if (currentAmmo < maxAmmo)
        {
            Vector2 spawnPoint = new Vector2(Random.Range(-right/2, right), Random.Range(-(height/2), (height/2)));
            var ammoTransform = Instantiate(ammoPrefab);
            ammoTransform.position = spawnPoint;
            currentAmmo++;
        }
        if (currentTime < maxTime)
        {
            Vector2 spawnPoint = new Vector2(Random.Range(-right/2, right), Random.Range(-(height/2), (height/2)));
            var timeTransform = Instantiate(timeStopPrefab);
            timeTransform.position = spawnPoint;
            currentTime++;
        }

        cooldown = 3f;
    }
}
