using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private Player player;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject getReadyImage; // Reference to the "Get Ready" image
    [SerializeField] private GameObject gameOverImage; // Reference to the "Game Over" image
    private BackgroundMusic backgroundMusic; 

    private int score;
    public int Score => score;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);
            Pause();

            backgroundMusic = FindObjectOfType<BackgroundMusic>();
        }
    }

    private void Start()
    {
        // Show "Get Ready" image and play button when the game starts
        getReadyImage.SetActive(true);
        playButton.SetActive(true);
         gameOverImage.SetActive(false);
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        playButton.SetActive(false);
        gameOverImage.SetActive(false); // Hide "Game Over" image
        getReadyImage.SetActive(false); // Hide "Get Ready" image

        Time.timeScale = 1f;
        player.enabled = true;

        // Play background music
        if (backgroundMusic != null)
        {
            backgroundMusic.PlayMusic();
        }

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        // Restart the timer through the singleton instance
        if (TimerManager.Instance != null)
        {
            TimerManager.Instance.RestartTimer();
            Debug.Log("Play Button Pressed - Timer Restarted");
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        getReadyImage.SetActive(false); // Hide "Get Ready" image
        gameOverImage.SetActive(true); // Show "Game Over" image

        Pause();

        // Stop background music
        if (backgroundMusic != null)
        {
            backgroundMusic.StopMusic();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
