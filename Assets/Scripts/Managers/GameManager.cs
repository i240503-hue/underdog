using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private UIManager uiManager;

    private GameState gameState = GameState.Playing;

    public enum GameState { Playing, Paused, PlayerWon, PlayerLost }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        if (playerController == null) playerController = FindObjectOfType<PlayerController>();
        if (enemyController == null) enemyController = FindObjectOfType<EnemyController>();
        if (uiManager == null) uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (gameState == GameState.Playing) CheckGameOver();
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    private void CheckGameOver()
    {
        if (playerController != null && playerController.GetHealth() <= 0) EndGame(false);
        if (enemyController != null && enemyController.GetHealth() <= 0) EndGame(true);
    }

    public void EndGame(bool playerWon)
    {
        if (gameState != GameState.Playing) return;
        gameState = playerWon ? GameState.PlayerWon : GameState.PlayerLost;
        Time.timeScale = 0f;
        if (uiManager != null) uiManager.ShowGameOverScreen(playerWon);
    }

    public void TogglePause()
    {
        if (gameState != GameState.Playing) return;
        gameState = GameState.Paused;
        Time.timeScale = 0f;
        if (uiManager != null) uiManager.ShowPauseMenu(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public GameState GetGameState() => gameState;
}