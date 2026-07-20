using UnityEngine;

// Gắn vào GameManager, kéo đủ tham chiếu để nối sự kiện giữa các ma và hệ thống xử lý bị bắt
public class GameEventBinder_TuanAnh : MonoBehaviour
{
    [SerializeField] private GhostCourtyardPatrol_TuanAnh courtyardGhost;
    [SerializeField] private GhostHallwayChase_TuanAnh hallwayGhost;
    [SerializeField] private PlayerCaughtHandler_TuanAnh caughtHandler;

    void Start()
    {
        if (hallwayGhost != null && caughtHandler != null)
        {
            hallwayGhost.OnPlayerCaught += caughtHandler.HandleCaught;
        }

        if (courtyardGhost != null)
        {
            // Ma ngoài sân chỉ "phát hiện" (không bắt trực tiếp) - có thể dùng để tắt đèn,
            // tăng nhạc căng thẳng, hoặc gọi ma hành lang tới gần hơn
            courtyardGhost.OnPlayerSpotted += () =>
            {
                Debug.Log("Cảnh báo: ma ngoài sân đã thấy player, cẩn thận!");
            };
        }
    }
}
