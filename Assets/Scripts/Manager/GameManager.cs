using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // Singleton instance of the game manager
    public static GameManager Instance { get; private set; }

    private UnityEvent _playerDeath = new();
    private UnityEvent _gamePaused = new();

    public UnityEvent PlayerDeath => _playerDeath;
    public UnityEvent GamePaused => _gamePaused;

    // Check for game timescale
    public bool isGameFrozen = false;

    public enum GameState
    {
        MAIN_MENU,
        PLAYING,
        PAUSED,
        GAME_WIN,
        GAME_OVER,
        TRANSITION
    }

    public GameState gameState = GameState.MAIN_MENU;

    private void Awake()
    {
        // Ensure only one instance of the GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayerDeath.AddListener(GameOver);
    }

    private void Update()
    {
        //Debug.Log(gameState);
    }

    public void GameOver()
    {
        gameState = GameState.GAME_OVER;
        UIManager.Instance.hideTimer();
        FreezeGame();
        UIManager.Instance.gameOverMenu.SetActive(true);    
    }

    public void GameWin()
    {
        gameState = GameState.GAME_WIN;
        UIManager.Instance.hideTimer();
        FreezeGame();
        UIManager.Instance.GameWin();
    }

    //toggle GamePaused
    public void OnPause()
    {
        if (gameState != GameState.PLAYING && gameState != GameState.PAUSED)
            return;

        if (!isGameFrozen)
        {
            GamePaused.Invoke();
            FreezeGame();
            gameState = GameState.PAUSED;
        }
        else
        {
            UnfreezeGame();
            gameState = GameState.PLAYING;
        }
        UIManager.Instance.pauseMenu.SetActive(isGameFrozen);
    }

    public void FreezeGame()
    {
        isGameFrozen = true;
        Time.timeScale = 0f;
    }

    public void UnfreezeGame()
    {
        isGameFrozen = false;
        Time.timeScale = 1f;
    }

    public bool IsPlaying()
    {
        return gameState == GameState.PLAYING;
    }

    public bool IsTransitioning()
    {
        return gameState == GameState.TRANSITION;
    }
}
