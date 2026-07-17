using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUi : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;

    private bool isDead;

    public void DeathPanel()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}