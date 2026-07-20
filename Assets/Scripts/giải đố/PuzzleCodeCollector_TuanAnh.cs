using System.Collections.Generic;
using UnityEngine;

// Gắn 1 lần duy nhất vào 1 GameObject quản lý chung (ví dụ "GameManager"),
// tất cả puzzle khác tham chiếu tới object này để lưu mật mã đã thu thập được.
public class PuzzleCodeCollector_TuanAnh : MonoBehaviour
{
    private Dictionary<string, string> collectedCodes = new Dictionary<string, string>();

    public void AddCode(string source, string code)
    {
        if (!collectedCodes.ContainsKey(source))
        {
            collectedCodes.Add(source, code);
            Debug.Log($"Đã thu thập mật mã từ [{source}]: {code}");
        }
    }

    public bool HasCode(string source)
    {
        return collectedCodes.ContainsKey(source);
    }

    public string GetCode(string source)
    {
        return collectedCodes.TryGetValue(source, out string code) ? code : null;
    }

    // Dùng khi cần kiểm tra tổng số mật mã đã thu thập (ví dụ để mở cửa boss cần đủ 3 mã)
    public int TotalCodesCollected()
    {
        return collectedCodes.Count;
    }
}
