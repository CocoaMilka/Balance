using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject WeightPrefab;
    public GameObject SpawnPoint;

    public float minSpawnInterval = 1.0f;
    public float maxSpawnInterval = 3.0f;

    private float nextSpawnTime; // Time when the next object will be spawned

    void Start()
    {
        // Initialize the timer for the first spawn
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void FixedUpdate()
    {
        // Check if it's time to spawn a new object
        if (Time.time >= nextSpawnTime)
        {
            // Spawn the WeightPrefab GameObject at the SpawnPoint's position
            Instantiate(WeightPrefab, SpawnPoint.transform.position, Quaternion.identity);

            // Update the timer for the next spawn
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }
}
