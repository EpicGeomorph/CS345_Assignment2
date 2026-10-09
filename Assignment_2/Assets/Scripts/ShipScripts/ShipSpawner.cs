using UnityEngine;

public class ShipSpawner : Ship
{
    private float defaultHealth;
    private float lastSpawned;
    public int enemiesToSpawn;
    public int spawnPeriod;
    public GameObject enemyToSpawn;
    private float rotationSpeed;
    private GameObject healthbar;
    private float originalWidth;
    private float originalHeight;

    protected override void CustomStart() // Abstract methods need to be overridden and implemented
    {
        defaultHealth = health;
        rotationSpeed = 1f;

        healthbar = GameObject.Find("Healthbar");
        originalHeight = healthbar.GetComponent<RectTransform>().sizeDelta.y;
        originalWidth = healthbar.GetComponent<RectTransform>().sizeDelta.x;
    }

    protected override void Move()
    {
    }

    void Update()
    {
        if (Time.time - lastSpawned > spawnPeriod)
        {
            lastSpawned = Time.time;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            }

            transform.rotation = Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
        }

        if (healthbar != null)
        {
            float perc = (float)health / defaultHealth;
            healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2(perc * originalWidth, originalHeight);
        }
    }
}
