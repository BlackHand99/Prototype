using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject controlsPanel;

    private bool paused;

    private void Start()
    {
        paused = false;

        pauseMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (controlsPanel.activeSelf)
            {
                CloseControls();
                return;
            }

            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
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
}