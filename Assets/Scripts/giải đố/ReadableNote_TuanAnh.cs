using UnityEngine;
using TMPro;

public class ReadableNote_TuanAnh : MonoBehaviour
{
    [Header("Nội dung hiển thị khi đọc (hỗ trợ xuống dòng bằng \\n)")]
    [TextArea(3, 8)]
    [SerializeField] private string noteContent;

    [Header("UI Popup dùng chung (kéo từ Canvas vào)")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI popupText;

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2.5f;

    private bool isPopupOpen = false;

    void Start()
    {
        if (player == null && Camera.main != null) player = Camera.main.transform;
    }

    void Update()
    {
        if (!isPopupOpen && Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange) OpenNote();
        }

        if (isPopupOpen && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            CloseNote();
        }
    }

    private void OpenNote()
    {
        isPopupOpen = true;
        if (popupPanel != null) popupPanel.SetActive(true);
        if (popupText != null) popupText.text = noteContent;
        Cursor.lockState = CursorLockMode.None;
    }

    private void CloseNote()
    {
        isPopupOpen = false;
        if (popupPanel != null) popupPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
