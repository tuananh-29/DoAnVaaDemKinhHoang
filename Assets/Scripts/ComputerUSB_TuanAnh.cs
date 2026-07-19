using UnityEngine;

// Gắn vào object "phía sau màn hình" của máy tính số 1 (1 collider nhỏ đặt ở mặt sau màn hình)
public class ComputerUSB_TuanAnh : MonoBehaviour
{
    [Header("Vật phẩm USB hiện ra khi tìm đúng")]
    [SerializeField] private GameObject usbItem;

    [Header("Gợi ý: chỉ hiện log gợi ý bàn phím thiếu phím số 5 khi tương tác mặt trước trước")]
    [SerializeField] private bool hintShown = false;

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2f;

    private bool alreadyTaken = false;

    void Start()
    {
        if (player == null && Camera.main != null)
        {
            player = Camera.main.transform;
        }
        if (usbItem != null) usbItem.SetActive(false);
    }

    void Update()
    {
        if (alreadyTaken) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange)
            {
                TakeUSB();
            }
        }
    }

    private void TakeUSB()
    {
        alreadyTaken = true;
        Debug.Log("Phía sau màn hình máy tính: tìm thấy USB!");
        if (usbItem != null) usbItem.SetActive(true);
    }

    // Gọi hàm này từ script tương tác mặt trước máy tính (bàn phím) để hiện gợi ý trước
    public void ShowKeyboardHint()
    {
        if (!hintShown)
        {
            hintShown = true;
            Debug.Log("Bàn phím này thiếu mất phím số 5... có gì đó bất thường.");
        }
    }
}
