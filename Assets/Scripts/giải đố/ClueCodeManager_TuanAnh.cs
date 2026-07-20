using System.Collections.Generic;
using UnityEngine;

// Gắn 1 lần vào GameManager
public class ClueCodeManager_TuanAnh : MonoBehaviour
{
    // Lưu theo vị trí trong mã (0,1,2,3) -> chữ số tìm được
    private Dictionary<int, string> collectedDigits = new Dictionary<int, string>();

    [Header("UI hiển thị mã đang thu thập được (tùy chọn)")]
    [SerializeField] private UnityEngine.UI.Text codeDisplayText;

    public void AddDigit(int position, string digit)
    {
        if (!collectedDigits.ContainsKey(position))
        {
            collectedDigits[position] = digit;
            Debug.Log($"Tìm thấy số ở vị trí {position + 1}: {digit}");
            UpdateDisplay();
        }
    }

    public bool HasAllDigits(int totalCount)
    {
        return collectedDigits.Count >= totalCount;
    }

    public string GetAssembledCode(int totalCount)
    {
        string result = "";
        for (int i = 0; i < totalCount; i++)
        {
            result += collectedDigits.ContainsKey(i) ? collectedDigits[i] : "_";
        }
        return result;
    }

    private void UpdateDisplay()
    {
        if (codeDisplayText != null)
        {
            codeDisplayText.text = "Mã đã tìm: " + GetAssembledCode(4);
        }
    }
}
