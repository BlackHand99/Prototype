using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeathUi : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;

    private bool isDead;

    public void DeathPanel()
    {
        deathPanel.SetActive(true);
        isDead = true;
        Time.timeScale = 0f;
    }

    public void OnRespawn(InputAction.CallbackContext context)
    {
        if (!isDead || !context.performed)
            return;
        deathPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}