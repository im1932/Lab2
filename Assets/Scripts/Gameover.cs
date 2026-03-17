using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject target;
    public Button restartButton;

    private bool isGameOver;

    private void Start()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(Restart);
    }

    private void Update()
    {
        if (isGameOver) return;

        if (target == null || !target.activeInHierarchy)
        {
            isGameOver = true;

            if (gameOverUI != null)
                gameOverUI.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    private void OnDestroy()
    {
        if (restartButton != null)
            restartButton.onClick.RemoveListener(Restart);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        ScoreOnDeath.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}