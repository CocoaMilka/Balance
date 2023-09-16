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
    public GameObject warning;

    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;
    public TMP_Text ScoreText;
    public int plushieCount;

    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 9f;

    private float nextSpawnTime; // Time when the next object will be spawned
    private float spawnTimer; // Timer for spawning
    private bool isPaused = false; // Flag to track if the game is paused

    public int difficultydiff = 15;

    public float imbalanceThreshold = 10f; // Threshold for triggering game over due to imbalance
    private float imbalanceTimer = 0f;
    private bool imbalanceDetected = false;

    void Start()
    {
        // Initialize the timer for the first spawn
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        spawnTimer = 0f;

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

        // Check if one box weighs more than the other, should always be balanced with a margin of difficultydiff
        if (box1.GetComponent<Balance>().weight > box2.GetComponent<Balance>().weight + difficultydiff)
        {
            Debug.Log("Left box");
            leftWarning.SetActive(true);
            warning.SetActive(true);
            rightWarning.SetActive(false); // Ensure the other warning is turned off
            imbalanceTimer += Time.deltaTime;

            if (imbalanceTimer >= imbalanceThreshold && !imbalanceDetected)
            {
                Debug.Log("Imbalance detected for too long. Game Over!");
                gameOverScreen.SetActive(true);
                ScoreText.text = "Plushie count: " + plushieCount;
                imbalanceDetected = true;
            }
        }
        else if (box2.GetComponent<Balance>().weight > box1.GetComponent<Balance>().weight + difficultydiff)
        {
            Debug.Log("Right box");
            rightWarning.SetActive(true);
            warning.SetActive(true);
            leftWarning.SetActive(false); // Ensure the other warning is turned off
            imbalanceTimer += Time.deltaTime;

            if (imbalanceTimer >= imbalanceThreshold && !imbalanceDetected)
            {
                Debug.Log("Imbalance detected for too long. Game Over!");
                gameOverScreen.SetActive(true);
                ScoreText.text = "Plushie count: " + plushieCount;
                imbalanceDetected = true;
            }
        }
        else
        {
            leftWarning.SetActive(false);
            rightWarning.SetActive(false);
            warning.SetActive(false);
            imbalanceTimer = 0f;
            imbalanceDetected = false;
        }

        // Update the spawn timer
        spawnTimer += Time.deltaTime;
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
            if (spawnTimer >= nextSpawnTime - 5f)
            {
                // Spawn the WeightPrefab GameObject at the SpawnPoint's position
                Instantiate(WeightPrefab, SpawnPoint.transform.position, Quaternion.identity);

                // Update the timer for the next spawn
                nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
                Debug.Log(nextSpawnTime);
                spawnTimer = 0f; // Reset the spawn timer
            }
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


/*
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

        // Check if one box weights more than the other, should always be balanced with a margin of difficultydiff
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

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
*/