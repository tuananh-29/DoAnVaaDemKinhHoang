using UnityEngine;

public class PlayerCaughtHandler_TuanAnh : MonoBehaviour
{
    [Header("Điểm hồi sinh gần nhất (đặt tại đầu mỗi khu vực/phòng)")]
    [SerializeField] private Transform respawnPoint;

    [Header("Nhân vật player (kéo TestPlayer vào)")]
    [SerializeField] private CharacterController playerController;

    [Header("Thời gian đứng hình/tối màn hình khi bị bắt (giây)")]
    [SerializeField] private float catchStunDuration = 1.5f;

    private bool isBeingCaught = false;

    public void HandleCaught()
    {
        if (isBeingCaught) return;
        isBeingCaught = true;

        Debug.Log("Player bị bắt! Đưa về điểm hồi sinh gần nhất.");
        Invoke(nameof(RespawnPlayer), catchStunDuration);
    }

    private void RespawnPlayer()
    {
        if (playerController != null && respawnPoint != null)
        {
            playerController.enabled = false; // tắt tạm để teleport không bị Character Controller chặn
            playerController.transform.position = respawnPoint.position;
            playerController.transform.rotation = respawnPoint.rotation;
            playerController.enabled = true;
        }
        isBeingCaught = false;
    }

    // Cập nhật checkpoint gần nhất, gọi khi player đi qua từng khu vực an toàn
    public void SetRespawnPoint(Transform newPoint)
    {
        respawnPoint = newPoint;
    }
}
