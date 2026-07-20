using UnityEngine;

// Gắn vào TỪNG quyển sách trong giá (Book_1, Book_2, Book_3...)
public class BookItem_TuanAnh : MonoBehaviour
{
    [Header("Số thứ tự quyển sách này")]
    [SerializeField] private int bookNumber;

    [Header("Đúng quyển sách theo câu đối là số mấy")]
    [SerializeField] private int correctBookNumber = 1;

    [Header("Mật mã nhận được khi lấy đúng sách")]
    [SerializeField] private string codeReceived = "LEQUYDON"; // đổi theo puzzle máy tính đã có

    [Header("Tham chiếu tới hệ thống lưu mật mã đã thu thập (tùy chọn)")]
    [SerializeField] private PuzzleCodeCollector_TuanAnh codeCollector;

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2.5f;

    private bool isInteractable = false; // chỉ bật true sau khi InspectShelf() gọi tới
    private bool alreadyTaken = false;

    void Start()
    {
        if (player == null && Camera.main != null)
        {
            player = Camera.main.transform;
        }
    }

    void Update()
    {
        if (!isInteractable || alreadyTaken) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange)
            {
                TakeBook();
            }
        }
    }

    // Được gọi từ BookShelf_TuanAnh khi giá sách đã được xác nhận đúng
    public void SetInteractable(bool value)
    {
        isInteractable = value;
    }

    private void TakeBook()
    {
        alreadyTaken = true;

        if (bookNumber == correctBookNumber)
        {
            Debug.Log($"Lấy đúng quyển sách số {bookNumber}. Mật mã nhận được: {codeReceived}");
            if (codeCollector != null)
            {
                codeCollector.AddCode("Thư viện", codeReceived);
            }
        }
        else
        {
            Debug.Log($"Quyển sách số {bookNumber} không có gì đặc biệt.");
        }

        // Ẩn quyển sách sau khi đã lấy/kiểm tra (tùy chọn - có thể bỏ nếu muốn giữ hiển thị)
        gameObject.SetActive(false);
    }
}
