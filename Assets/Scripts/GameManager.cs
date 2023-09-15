using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject WeightPrefab;
    public GameObject SpawnPoint;
    public GameObject Destroyer;
    private Destroyer isGameover;

    public GameObject box1;
    public GameObject box2;

    public GameObject leftWarning;
    public GameObject rightWarning;

    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;
    public TMP_Text ScoreText;
    public int plushieCount;

    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 9f;

    private float nextSpawnTime; // Time when the next object will be spawned
    private bool isPaused = false; // Flag to track if the game is paused

    public int difficultydiff = 15;

    void Start()
    {
        // Initialize the timer for the first spawn
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);

        isGameover = Destroyer.GetComponent<Destroyer>();
    }

    void Update()
    {
        // Check for the Escape key press to toggle pause/unpause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // Unpause the game
                Time.timeScale = 1;
                pauseMenuScreen.SetActive(false);
                isPaused = false;
            }
            else
            {
                // Pause the game
                Time.timeScale = 0;
                pauseMenuScreen.SetActive(true);
                isPaused = true;
            }
        }

        if (box1.GetComponent<Balance>().weight > box2.GetComponent<Balance>().weight + difficultydiff)
        {
            Debug.Log("Left box");
            leftWarning.SetActive(true);
        }
        else if (box2.GetComponent<Balance>().weight > box1.GetComponent<Balance>().weight + difficultydiff)
        {
            Debug.Log("Right box");
            rightWarning.SetActive(true);
        }
        else
        {
            leftWarning.SetActive(false);
            rightWarning.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (isGameover.isGameover)
        {
            Debug.Log("Game Over!");
            gameOverScreen.SetActive(true);
            ScoreText.text = "Plushie count: " + plushieCount;
        }
        else if (!isPaused) // Check if the game is not paused
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

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}