using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public GameState currentState;
    private FirstPersonController player;
    
    public Canvas playerUI;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI EnemyCount;
    public TextMeshProUGUI YouWonText;
    public TextMeshProUGUI ObjectiveText;

    public Canvas pauseUI;
    public Canvas gameOverUI;

    public int enemyCount = 10;
    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        Won
    }
    
    public static GameStateManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        SetGameState(GameState.Playing);
        player = FindObjectOfType<FirstPersonController>();
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.Playing:
                OnPlaying();
                break;
            case GameState.Paused:
                OnPaused();
                break;
            case GameState.GameOver:
                OnGameOver();
                break;
            case GameState.Won:
                OnWon();
                break;
        }
    }

    void OnPlaying()
    {
        HealthText.text = "Health: " + player.health;
        EnemyCount.text = "Enemies: " + enemyCount;
        
        if (Input.GetKeyDown(KeyCode.Escape))
            SetGameState(GameState.Paused);
        
        if (player.health <= 0)
            SetGameState(GameState.GameOver);
        
        if(enemyCount <= 0)
            SetGameState(GameState.Won);
    }

    void OnPaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if(enemyCount <= 0)
                SetGameState(GameState.Won);
            else
                SetGameState(GameState.Playing);
    }

    void OnGameOver()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SetGameState(GameState.Playing);
        }
            
    }

    void OnWon()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SetGameState(GameState.Playing);
        }
    }

    public void SetGameState(GameState newState)
    {
        // Exit the current state
        ExitState(currentState);

        // Enter the new state
        currentState = newState;
        EnterState(currentState);
    }

    private void EnterState(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                Time.timeScale = 1; 
                playerUI.gameObject.SetActive(true);
                pauseUI.gameObject.SetActive(false);
                gameOverUI.gameObject.SetActive(false);
                ObjectiveText.gameObject.SetActive(true);
                YouWonText.gameObject.SetActive(false);
                Debug.Log("Game Started");
                break;

            case GameState.Paused:
                Time.timeScale = 0; 
                playerUI.gameObject.SetActive(false);
                pauseUI.gameObject.SetActive(true);
                gameOverUI.gameObject.SetActive(false);
                Debug.Log("Game Paused");
                break;

            case GameState.GameOver:
                Time.timeScale = 0; 
                playerUI.gameObject.SetActive(false);
                pauseUI.gameObject.SetActive(false);
                gameOverUI.gameObject.SetActive(true);
                Debug.Log("Game Over");
                break;
            
            case GameState.Won:
                Time.timeScale = 1;
                playerUI.gameObject.SetActive(true);
                pauseUI.gameObject.SetActive(false);
                gameOverUI.gameObject.SetActive(false);
                ObjectiveText.gameObject.SetActive(false);
                YouWonText.gameObject.SetActive(true);
                Debug.Log("Won Game");
                break;
        }
    }

    private void ExitState(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                // Code to cleanup when exiting playing state
                break;

            case GameState.Paused:
                // Code to cleanup when exiting paused state
                break;

            case GameState.GameOver:
                // Code to cleanup when exiting game over state
                break;
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}