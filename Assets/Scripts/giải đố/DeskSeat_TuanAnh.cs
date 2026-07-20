using UnityEngine;

// Gắn vào TỪNG bàn học trong lớp (đặt tên rõ theo sơ đồ: Desk_A1, Desk_A2, Desk_A3, Desk_B1... Desk_C3)
public class DeskSeat_TuanAnh : MonoBehaviour
{
    [Header("Vị trí bàn này theo sơ đồ (vd: \"A1\", \"C3\")")]
    [SerializeField] private string seatCode;

    [Header("Đúng vị trí bàn của Lan theo câu đối")]
    [SerializeField] private string correctSeatCode = "C3";

    [Header("Vật phẩm sẽ hiện ra khi đúng bàn (chìa khóa)")]
    [SerializeField] private GameObject keyItem; // kéo model chìa khóa (đã ẩn sẵn) vào đây

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2f;

    private bool alreadyChecked = false;

    void Start()
    {
        if (player == null && Camera.main != null)
        {
            player = Camera.main.transform;
        }
        // Đảm bảo chìa khóa ẩn sẵn từ đầu, chỉ hiện khi đúng bàn được kiểm tra
        if (keyItem != null) keyItem.SetActive(false);
    }

    void Update()
    {
        if (alreadyChecked) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange)
            {
                CheckSeat();
            }
        }
    }

    private void CheckSeat()
    {
        alreadyChecked = true;

        if (seatCode == correctSeatCode)
        {
            Debug.Log($"Bàn {seatCode}: đúng chỗ ngồi của Lan. Tìm thấy chìa khóa!");
            if (keyItem != null) keyItem.SetActive(true);
        }
        else
        {
            Debug.Log($"Bàn {seatCode}: không có gì đặc biệt.");
        }
    }
}
