using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GhostCatchEffect : MonoBehaviour
{
    [Header("Kéo script GhostHallwayChase_TuanAnh của ma vào đây")]
    [SerializeField] private GhostHallwayChase_TuanAnh ghostChase;

    [Header("Âm thanh khi bị bắt (tiếng thét, jumpscare sound...)")]
    [SerializeField] private AudioClip catchSound;
    [SerializeField] private AudioSource audioSource;

    [Header("Hình ảnh jumpscare (UI Image, để full màn hình, tắt sẵn lúc đầu)")]
    [SerializeField] private GameObject jumpscareUI;
    [SerializeField] private Image redFlashOverlay; // 1 Image màu đỏ trải full màn hình, alpha = 0 lúc đầu
    [SerializeField] private float flashDuration = 0.5f;

    [Header("Sau khi bị bắt bao lâu thì xử lý tiếp (vd: load lại scene)")]
    [SerializeField] private float delayBeforeGameOver = 2f;

    void OnEnable()
    {
        if (ghostChase != null)
            ghostChase.OnPlayerCaught += HandlePlayerCaught;
    }

    void OnDisable()
    {
        if (ghostChase != null)
            ghostChase.OnPlayerCaught -= HandlePlayerCaught;
    }

    private void HandlePlayerCaught()
    {
        // Phát âm thanh
        if (audioSource != null && catchSound != null)
        {
            audioSource.PlayOneShot(catchSound);
        }

        // Bật hình ảnh jumpscare
        if (jumpscareUI != null)
        {
            jumpscareUI.SetActive(true);
        }

        // Hiệu ứng nháy đỏ toàn màn hình
        if (redFlashOverlay != null)
        {
            StartCoroutine(FlashRed());
        }

        // Khóa input người chơi ngay lập tức (tùy game của bạn có script PlayerController riêng)
        Time.timeScale = 0f; // dừng game lại, giống jumpscare kinh dị

        // Sau khoảng thời gian delay, xử lý Game Over
        StartCoroutine(GameOverAfterDelay());
    }

    private IEnumerator FlashRed()
    {
        float t = 0f;
        Color c = redFlashOverlay.color;
        while (t < flashDuration)
        {
            t += Time.unscaledDeltaTime; // dùng unscaledDeltaTime vì Time.timeScale = 0
            c.a = Mathf.PingPong(t * 4f, 1f);
            redFlashOverlay.color = c;
            yield return null;
        }
    }

    private IEnumerator GameOverAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delayBeforeGameOver);
        Time.timeScale = 1f;
        // TODO: gọi hàm load lại scene hoặc hiện màn hình Game Over ở đây
        // Ví dụ: UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        Debug.Log("Game Over - Player đã bị ma bắt!");
    }
}