using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    public GameObject panelWin;
    public GameObject panelLose;
    public GameObject questNotificationPanel;

    public void ShowWinScreen()
    {
        panelWin.SetActive(true);
        questNotificationPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ShowLoseScreen()
    {
        panelLose.SetActive(true);
        questNotificationPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // ---- CHI DUNG DE TEST, XOA SAU KHI CO GAME THAT ----
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ShowWinScreen();
        if (Input.GetKeyDown(KeyCode.Alpha2)) ShowLoseScreen();
    }
    void Start()
{
    panelWin.SetActive(false);
    panelLose.SetActive(false);
}
}