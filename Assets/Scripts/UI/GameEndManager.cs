using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // BẮT BUỘC có dòng này để dùng Coroutine

public class GameEndManager : MonoBehaviour
{
    public GameObject panelWin;
    public GameObject panelLose;
    public GameObject questNotificationPanel;
    
    [Header("UI Effect")]
    public CanvasGroup redVignetteCanvasGroup; // Kéo RedVignette vào đây
    public float fadeSpeed = 3f; // Tốc độ mờ hiện/ẩn (càng lớn càng nhanh)

    private Coroutine fadeCoroutine;

    void Start()
    {
        panelWin.SetActive(false);
        panelLose.SetActive(false);
        
        // Cho viền đỏ ẩn đi hoàn toàn lúc bắt đầu
        if (redVignetteCanvasGroup != null)
        {
            redVignetteCanvasGroup.alpha = 0f;
            redVignetteCanvasGroup.gameObject.SetActive(true);
        }
    }

    // Hàm gọi để TỪ TỪ BẬT viền đỏ (Fade In)
    public void ShowDetectedEffect()
    {
        StartFadeEffect(1f);
    }

    // Hàm gọi để TỪ TỪ TẮT viền đỏ (Fade Out)
    public void HideDetectedEffect()
    {
        StartFadeEffect(0f);
    }

    private void StartFadeEffect(float targetAlpha)
    {
        if (redVignetteCanvasGroup == null) return;
        
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha));
    }

    // Xử lý biến thiên độ trong suốt (Alpha) từ từ theo thời gian
    private IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(redVignetteCanvasGroup.alpha, targetAlpha))
        {
            redVignetteCanvasGroup.alpha = Mathf.MoveTowards(
                redVignetteCanvasGroup.alpha, 
                targetAlpha, 
                fadeSpeed * Time.unscaledDeltaTime
            );
            yield return null;
        }
    }

    public void ShowWinScreen()
    {
        panelWin.SetActive(true);
        questNotificationPanel.SetActive(false);
        HideDetectedEffect();
        Time.timeScale = 0f;
    }

    public void ShowLoseScreen()
    {
        ShowDetectedEffect();
        panelLose.SetActive(true);
        questNotificationPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        HideDetectedEffect();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        HideDetectedEffect();
        SceneManager.LoadScene("MainMenu");
    }

    // ---- PHÍM PHÍM TEST ----
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ShowWinScreen();
        if (Input.GetKeyDown(KeyCode.Alpha2)) ShowLoseScreen();
        
        // Bấm phím 3: Nhấp nháy Fade-in / Fade-out mượt mà để test!
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (redVignetteCanvasGroup != null)
            {
                if (redVignetteCanvasGroup.alpha < 0.5f)
                    ShowDetectedEffect();
                else
                    HideDetectedEffect();
            }
        }
    }
}