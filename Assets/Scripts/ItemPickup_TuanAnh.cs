using UnityEngine;

// Gắn vào BẤT KỲ vật phẩm nào có thể nhặt (cầu chì, USB, chìa khóa...)
public class ItemPickup_TuanAnh : MonoBehaviour
{
    [Header("Tên vật phẩm (dùng để kiểm tra sau này, vd: \"CauChi\", \"Usb\")")]
    [SerializeField] private string itemName;

    [Header("Tham chiếu túi đồ")]
    [SerializeField] private PlayerInventory_TuanAnh inventory;

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2f;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange)
            {
                Pickup();
            }
        }
    }

    private void Pickup()
    {
        if (inventory != null)
        {
            inventory.AddItem(itemName);
        }
        gameObject.SetActive(false);
    }
}
