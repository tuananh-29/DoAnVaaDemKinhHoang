using UnityEngine;

public class PlayerInteract_TuanAnh : MonoBehaviour
{
    [Header("Cài đặt tương tác")]
    [SerializeField] private Transform cameraTransform; // kéo Main Camera vào đây
    [SerializeField] private float interactRange = 2.5f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private LayerMask interactLayer = ~0; // mặc định check mọi layer

    private void Awake()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        if (cameraTransform == null) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
        {
            // Nếu cửa nằm ở object cha (model cửa phức tạp có nhiều mesh con),
            // dùng GetComponentInParent để tìm đúng script dù raycast trúng phần con
            DoorInteract_TuanAnh door = hit.collider.GetComponentInParent<DoorInteract_TuanAnh>();
            if (door != null)
            {
                door.ToggleDoor();
            }
        }
    }

    // Vẽ tia raycast trong Scene View để dễ debug (chỉ hiện khi chọn object trong Editor)
    private void OnDrawGizmosSelected()
    {
        if (cameraTransform == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * interactRange);
    }
}
