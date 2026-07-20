using UnityEngine;
using UnityEngine.UI;

public class KeypadDoor_TuanAnh : MonoBehaviour
{
    [Header("Mật mã đúng")]
    [SerializeField] private string correctCode = "4312";

    [Header("Cửa cần mở khi nhập đúng")]
    [SerializeField] private DoorInteract_TuanAnh targetDoor;

    [Header("UI Keypad (tắt/bật khi player tương tác)")]
    [SerializeField] private GameObject keypadPanel; // Panel UI chứa các nút số
    [SerializeField] private Text displayText;         // Hiển thị số đang nhập
    [SerializeField] private Text feedbackText;         // Hiển thị "Sai mã" khi nhập sai

    [Header("Tương tác mở bảng nhập")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2.5f;

    private string currentInput = "";
    private bool isPanelOpen = false;

    void Start()
    {
        if (player == null && Camera.main != null)
        {
            player = Camera.main.transform;
        }
        if (keypadPanel != null) keypadPanel.SetActive(false);
    }

    void Update()
    {
        if (!isPanelOpen && Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange)
            {
                OpenKeypad();
            }
        }

        if (isPanelOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseKeypad();
        }
    }

    private void OpenKeypad()
    {
        isPanelOpen = true;
        currentInput = "";
        UpdateDisplay();
        if (keypadPanel != null) keypadPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // mở khóa chuột để bấm UI
    }

    private void CloseKeypad()
    {
        isPanelOpen = false;
        if (keypadPanel != null) keypadPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Gọi hàm này từ OnClick() của từng nút số 0-9 trên UI
    public void PressNumber(string number)
    {
        if (currentInput.Length >= 4) return; // giới hạn 4 số
        currentInput += number;
        UpdateDisplay();

        if (currentInput.Length == 4)
        {
            CheckCode();
        }
    }

    // Gọi từ nút Xóa trên UI
    public void ClearInput()
    {
        currentInput = "";
        UpdateDisplay();
        if (feedbackText != null) feedbackText.text = "";
    }

    private void UpdateDisplay()
    {
        if (displayText != null) displayText.text = currentInput.PadRight(4, '_');
    }

    private void CheckCode()
    {
        if (currentInput == correctCode)
        {
            if (feedbackText != null) feedbackText.text = "Đúng!";
            if (targetDoor != null)
            {
                targetDoor.Unlock();
                targetDoor.ToggleDoor();
            }
            Invoke(nameof(CloseKeypad), 0.8f);
        }
        else
        {
            if (feedbackText != null) feedbackText.text = "Sai mã, thử lại";
            currentInput = "";
            Invoke(nameof(UpdateDisplay), 0.5f);
        }
    }
}
