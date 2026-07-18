using UnityEngine;

public class TestQuestTrigger : MonoBehaviour
{
    public QuestNotification questNotification;
    public DetectionEffect detectionEffect;

    void Update()
    {
        // Test thông báo nhiệm vụ
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questNotification.ShowNotification("Nhiệm vụ mới", "Tìm 3 mảnh ký ức");
        }

        // Test hiệu ứng phát hiện
        if (Input.GetKeyDown(KeyCode.E)) detectionEffect.SetDetected(true);
        if (Input.GetKeyDown(KeyCode.R)) detectionEffect.SetDetected(false);
        if (Input.GetKeyDown(KeyCode.M))
    AudioManager.Instance.PlayMusic(AudioManager.Instance.nhacNenGameplay);

if (Input.GetKeyDown(KeyCode.N))
    AudioManager.Instance.PlayAmbience(AudioManager.Instance.ambienceGameplay);

if (Input.GetKeyDown(KeyCode.F))
    AudioManager.Instance.PlayFootstep();

if (Input.GetKeyDown(KeyCode.C))
    AudioManager.Instance.PlayCanhBao();
    }
}