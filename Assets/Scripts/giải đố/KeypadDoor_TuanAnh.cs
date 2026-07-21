using UnityEngine;
using TMPro;

public class KeypadDoor_TuanAnh : MonoBehaviour
{
    [SerializeField] private string correctCode = "4312";
    [SerializeField] private SlidingGate_TuanAnh targetGate;
    [SerializeField] private GameObject keypadPanel;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2.5f;

    private string currentInput = "";
    private bool isPanelOpen = false;

    void Start()
    {
        if (player == null && Camera.main != null) player = Camera.main.transform;
        if (keypadPanel != null) keypadPanel.SetActive(false);
    }

    void Update()
    {
        if (!isPanelOpen && Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange) OpenKeypad();
        }
        if (isPanelOpen && Input.GetKeyDown(KeyCode.Escape)) CloseKeypad();
    }

    private void OpenKeypad()
    {
        isPanelOpen = true;
        currentInput = "";
        UpdateDisplay();
        if (keypadPanel != null) keypadPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    private void CloseKeypad()
    {
        isPanelOpen = false;
        if (keypadPanel != null) keypadPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PressNumber(string number)
    {
        if (currentInput.Length >= 4) return;
        currentInput += number;
        UpdateDisplay();
        if (currentInput.Length == 4) CheckCode();
    }

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
            if (targetGate != null)
            {
                targetGate.Unlock();
                targetGate.ToggleGate();
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
