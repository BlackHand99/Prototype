using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject quitConfirmPanel;
    [SerializeField] private string mainMenuScene = "MainMenu";

    private bool paused;

    private void Start()
    {
        paused = false;

        pauseMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);
        quitConfirmPanel.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (!Keyboard.current.escapeKey.wasPressedThisFrame)
            return;

        // Playing
        if (!paused)
        {
            Pause();
            return;
        }

        // Controls -> Pause
        if (controlsPanel.activeSelf)
        {
            CloseControls();
            return;
        }

        // Pause -> Quit Confirmation
        if (pauseMenuPanel.activeSelf)
        {
            OpenQuitConfirmation();
            return;
        }

        // Quit Confirmation -> Pause
        if (quitConfirmPanel.activeSelf)
        {
            CloseQuitConfirmation();
        }
    }

    public void Pause()
    {
        paused = true;

        pauseMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        paused = false;

        pauseMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);
        quitConfirmPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OpenControls()
    {
        pauseMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void CloseControls()
    {
        controlsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    public void OpenQuitConfirmation()
    {
        pauseMenuPanel.SetActive(false);
        quitConfirmPanel.SetActive(true);
    }

    public void CloseQuitConfirmation()
    {
        quitConfirmPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }
    public void QuitToMainMenu()
    {
        paused = false;

        Time.timeScale = 1f;

        pauseMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);
        quitConfirmPanel.SetActive(false);

        SceneManager.LoadScene(mainMenuScene);
    }
    public void ReturnToPauseMenu()
    {
        quitConfirmPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }
}