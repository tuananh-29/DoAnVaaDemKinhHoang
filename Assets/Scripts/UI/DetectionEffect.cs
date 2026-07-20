using UnityEngine;
using UnityEngine.UI;

public class DetectionEffect : MonoBehaviour
{
    public Image overlay;
    public float fadeSpeed = 3f;
    [Range(0f, 1f)] public float maxAlpha = 0.5f;

    private bool isDetected = false;

   public void SetDetected(bool detected)
{
    if (detected && !isDetected)
    {
        AudioManager.Instance.PlayCanhBao();
    }
    isDetected = detected;
}

    void Update()
    {
        float targetAlpha = isDetected ? maxAlpha : 0f;
        Color c = overlay.color;
        c.a = Mathf.Lerp(c.a, targetAlpha, Time.deltaTime * fadeSpeed);
        overlay.color = c;
    }
}