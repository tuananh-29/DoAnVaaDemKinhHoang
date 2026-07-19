using UnityEngine;

public class FusePanel_TuanAnh : MonoBehaviour
{
    [Header("Tên vật phẩm cầu chì cần có trong túi đồ")]
    [SerializeField] private string requiredItemName = "CauChi";

    [Header("Túi đồ player")]
    [SerializeField] private PlayerInventory_TuanAnh inventory;

    [Header("Đèn/hiệu ứng bật lên khi sửa xong (tùy chọn)")]
    [SerializeField] private Light[] lightsToTurnOn;

    [Header("Cổng bảo vệ sẽ mở khi sửa xong bảng điện")]
    [SerializeField] private DoorInteract_TuanAnh guardGate;

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2f;

    private bool alreadyFixed = false;

    void Start()
    {
        if (player == null && Camera.main != null)
        {
            player = Camera.main.transform;
        }
        if (inventory == null)
        {
            inventory = FindObjectOfType<PlayerInventory_TuanAnh>();
        }
    }

    void Update()
    {
        if (alreadyFixed) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange)
            {
                TryFixPanel();
            }
        }
    }

    private void TryFixPanel()
    {
        if (inventory != null && inventory.HasItem(requiredItemName))
        {
            alreadyFixed = true;
            inventory.RemoveItem(requiredItemName);

            Debug.Log("Lắp cầu chì vào ô F4. Điện đã được khôi phục.");

            foreach (var light in lightsToTurnOn)
            {
                if (light != null) light.enabled = true;
            }

            if (guardGate != null)
            {
                guardGate.Unlock();
                guardGate.ToggleDoor();
            }
        }
        else
        {
            Debug.Log("Bảng điện bị hỏng, cần tìm cầu chì trước.");
        }
    }
}
