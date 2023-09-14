using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject WeightPrefab;
    public GameObject SpawnPoint;
    public GameObject Destroyer;
    private Destroyer isGameover;

    public GameObject gameOverScreen;
    public TMP_Text ScoreText;
    public int plushieCount;

    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 9f;

    private float nextSpawnTime; // Time when the next object will be spawned

    void Start()
    {
        // Initialize the timer for the first spawn
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);

        isGameover = Destroyer.GetComponent<Destroyer>();
    }

    void FixedUpdate()
    {
        if (isGameover.isGameover)
        {
            Debug.Log("Game Over!");
            gameOverScreen.SetActive(true);
            ScoreText.text = "Plushie count: " + plushieCount;
            Time.timeScale = 0;
        }
        else
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
}
