using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public static bool IsPaused { get; private set; }

    [Header("UI References")]
    public GameObject pausePanel;
    public GameObject settingsPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Phat hien 2 PauseManager cung ton tai! Huy bot 1 cai.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc nhan duoc luc " + Time.realtimeSinceStartup + " | IsPaused hien tai = " + IsPaused);

            if (settingsPanel != null && settingsPanel.activeSelf)
            {
                BackToPause();
                return;
            }

            if (IsPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        Debug.Log("Pause() da duoc goi!");
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        Debug.Log("PausePanel active state sau khi Pause: " + pausePanel.activeSelf);
    }

    public void Resume()
    {
        Debug.Log("Resume() da duoc goi!");
        pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void OpenSettings()
    {
        pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void BackToPause()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}