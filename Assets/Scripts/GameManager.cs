using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private Player player;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text startoffFeedback;
    [SerializeField] private Text score1Feedback;
    [SerializeField] private Text score5Feedback;
    [SerializeField] private Text score10Feedback;
    [SerializeField] private Text score20Feedback;
    [SerializeField] private Text score30Feedback;
    [SerializeField] private Text score50Feedback;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject getReadyImage; // Reference to the "Get Ready" image
    [SerializeField] private GameObject gameOverImage; // Reference to the "Game Over" image
    private BackgroundMusic backgroundMusic; 

    private int score;
    public int Score => score;
    private bool isGetReady = true;
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
        getReadyImage.SetActive(true);
        playButton.SetActive(true);
        gameOverImage.SetActive(false);
        startoffFeedback.gameObject.SetActive(true);
        score1Feedback.gameObject.SetActive(false);
        score5Feedback.gameObject.SetActive(false);
        score10Feedback.gameObject.SetActive(false);
        score20Feedback.gameObject.SetActive(false);
        score30Feedback.gameObject.SetActive(false);
        score50Feedback.gameObject.SetActive(false);
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        isGetReady = false; 
        playButton.SetActive(false);
        gameOverImage.SetActive(false); // Hide "Game Over" image
        getReadyImage.SetActive(false); // Hide "Get Ready" image
        startoffFeedback.gameObject.SetActive(false);
        score1Feedback.gameObject.SetActive(false);
        score5Feedback.gameObject.SetActive(false);
        score10Feedback.gameObject.SetActive(false);
        score20Feedback.gameObject.SetActive(false);
        score30Feedback.gameObject.SetActive(false);
        score50Feedback.gameObject.SetActive(false);

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

        if (isGetReady)
        {
            StartCoroutine(DisplayStartoffFeedback());
        }

        // Check if the game is not in "Get Ready" state and the score is 1
        if (!isGetReady && score == 1)
        {
            StartCoroutine(DisplayScore1Feedback());
        }

        // Check if the game is not in "Get Ready" state and the score is 5
        if (!isGetReady && score == 5)
        {
            StartCoroutine(DisplayScore5Feedback());
        }

        if (!isGetReady && score == 10)
        {
            StartCoroutine(DisplayScore10Feedback());
        }

        if (!isGetReady && score == 20)
        {
            StartCoroutine(DisplayScore20Feedback());
        }

        if (!isGetReady && score == 30)
        {
            StartCoroutine(DisplayScore30Feedback());
        }
        if (!isGetReady && score == 50)
        {
            StartCoroutine(DisplayScore50Feedback());
        }
    }

    private IEnumerator DisplayStartoffFeedback()
    {
        startoffFeedback.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        startoffFeedback.gameObject.SetActive(false);
    }
    private IEnumerator DisplayScore1Feedback()
    {
        score1Feedback.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        score1Feedback.gameObject.SetActive(false);
    }
    private IEnumerator DisplayScore5Feedback()
    {
        score5Feedback.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        score5Feedback.gameObject.SetActive(false);
    }
    private IEnumerator DisplayScore10Feedback()
    {
        score10Feedback.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        score10Feedback.gameObject.SetActive(false);
    }
    private IEnumerator DisplayScore20Feedback()
    {
        score20Feedback.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        score20Feedback.gameObject.SetActive(false);
    }
    private IEnumerator DisplayScore30Feedback()
    {
        score30Feedback.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        score30Feedback.gameObject.SetActive(false);
    }
    private IEnumerator DisplayScore50Feedback()
    {
        score50Feedback.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        score50Feedback.gameObject.SetActive(false);
    }
}
