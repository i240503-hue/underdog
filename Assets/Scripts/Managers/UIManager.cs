using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image playerHealthFill;
    [SerializeField] private Image enemyHealthFill;
    [SerializeField] private Image staminaFill;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
        if (restartButton != null) restartButton.onClick.AddListener(() => GameManager.Instance?.RestartGame());
    }

    public void UpdatePlayerHealth(float current, float max)
    {
        if (playerHealthFill != null) playerHealthFill.fillAmount = current / max;
    }

    public void UpdateEnemyHealth(float current, float max)
    {
        if (enemyHealthFill != null) enemyHealthFill.fillAmount = current / max;
    }

    public void UpdateStamina(float current, float max)
    {
        if (staminaFill != null) staminaFill.fillAmount = current / max;
    }

    public void UpdateLevelUI(int level)
    {
        if (levelText != null) levelText.text = $"Level: {level}";
    }

    public void ShowGameOverScreen(bool playerWon)
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverText != null) gameOverText.text = playerWon ? "VICTORY!" : "DEFEAT!";
    }

    public void ShowPauseMenu(bool show)
    {
        if (pausePanel != null) pausePanel.SetActive(show);
    }
}