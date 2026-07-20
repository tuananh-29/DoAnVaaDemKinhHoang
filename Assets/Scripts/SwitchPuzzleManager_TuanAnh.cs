using System.Collections;
using UnityEngine;

public class SwitchPuzzleManager_TuanAnh : MonoBehaviour
{
    [Header("Thứ tự đúng: 0=A, 1=B, 2=C -> bấm đúng thứ tự A-B-C")]
    private int nextExpectedIndex = 0;

    [Header("Đèn trong phòng (bật/tắt khi đúng/sai)")]
    [SerializeField] private Light[] roomLights;

    [Header("Cửa cần mở khi giải đúng puzzle")]
    [SerializeField] private DoorInteract_TuanAnh exitDoor;

    [Header("Tất cả công tắc trong puzzle này (để reset khi bấm sai)")]
    [SerializeField] private LightSwitch_TuanAnh[] allSwitches;

    [Header("Ma sẽ tiến gần hơn khi bấm sai")]
    [SerializeField] private Transform ghostTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float ghostApproachDistance = 5f; // ma dịch gần thêm bao nhiêu mét khi sai

    [Header("Âm thanh (tùy chọn)")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;
    [SerializeField] private AudioClip powerOffSound;

    private bool puzzleSolved = false;

    public void OnSwitchPressed(int pressedIndex, LightSwitch_TuanAnh switchScript)
    {
        if (puzzleSolved) return;

        if (pressedIndex == nextExpectedIndex)
        {
            // Bấm đúng thứ tự
            nextExpectedIndex++;
            PlaySound(correctSound);

            if (nextExpectedIndex >= 3) // đã bấm đủ A-B-C đúng thứ tự
            {
                SolvePuzzle();
            }
        }
        else
        {
            // Bấm sai thứ tự
            StartCoroutine(HandleWrongPress());
        }
    }

    private IEnumerator HandleWrongPress()
    {
        PlaySound(wrongSound);
        yield return new WaitForSeconds(0.3f);

        // Tắt điện
        PlaySound(powerOffSound);
        SetLights(false);

        // Ma tiến gần hơn về phía player - tạo áp lực thời gian thật
        if (ghostTransform != null && playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - ghostTransform.position).normalized;
            ghostTransform.position += direction * ghostApproachDistance;
        }

        // Reset chuỗi và toàn bộ công tắc để player thử lại
        nextExpectedIndex = 0;
        foreach (var sw in allSwitches)
        {
            if (sw != null) sw.ResetSwitch();
        }

        // Đợi 2 giây tối rồi bật đèn lại để player thử lại (không phạt quá nặng)
        yield return new WaitForSeconds(2f);
        SetLights(true);
    }

    private void SolvePuzzle()
    {
        puzzleSolved = true;
        SetLights(true);

        if (exitDoor != null)
        {
            exitDoor.Unlock();
            exitDoor.ToggleDoor();
        }
    }

    private void SetLights(bool state)
    {
        foreach (var light in roomLights)
        {
            if (light != null) light.enabled = state;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
