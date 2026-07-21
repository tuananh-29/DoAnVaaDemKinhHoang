using UnityEngine;

// Gắn vào GameObject Light đã được gắn sẵn lên Camera/tay nhân vật (ban đầu để tắt trong Hierarchy hoặc disable Light).
// Chỉ cho phép bật/tắt khi player đã nhặt được đèn pin (kiểm tra qua PlayerInventory_TuanAnh).
public class FlashlightController_TuanAnh : MonoBehaviour
{
    [Header("Tên vật phẩm đèn pin trong túi đồ")]
    [SerializeField] private string flashlightItemName = "DenPin";

    [Header("Tham chiếu")]
    [SerializeField] private PlayerInventory_TuanAnh inventory;
    [SerializeField] private Light flashlightLight;

    [Header("Phím bật/tắt")]
    [SerializeField] private KeyCode toggleKey = KeyCode.F;

    [Header("Âm thanh click (tùy chọn)")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;

    void Start()
    {
        if (inventory == null)
            inventory = FindObjectOfType<PlayerInventory_TuanAnh>();

        if (flashlightLight == null)
            flashlightLight = GetComponent<Light>();

        if (flashlightLight != null)
            flashlightLight.enabled = false; // Đèn tắt ban đầu, kể cả khi đã nhặt
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            TryToggle();
        }
    }

    private void TryToggle()
    {
        // Chỉ cho bật/tắt nếu đã nhặt được đèn pin trong túi đồ
        if (inventory == null || !inventory.HasItem(flashlightItemName))
        {
            Debug.Log("Chưa có đèn pin trong túi đồ.");
            return;
        }

        if (flashlightLight == null) return;

        flashlightLight.enabled = !flashlightLight.enabled;

        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }
}
