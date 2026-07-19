using UnityEngine;

public class PlayerInteract_TuanAnh : MonoBehaviour
{
    [Header("Cài đặt tương tác")]
    [SerializeField] private Transform cameraTransform; 
    [SerializeField] private float interactRange = 2.5f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private LayerMask interactLayer = ~0; 

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
            DoorInteract_TuanAnh door = hit.collider.GetComponentInParent<DoorInteract_TuanAnh>();
            if (door != null)
            {
                // Gọi hàm Interact (public void) thay vì gọi thẳng IEnumerator
                door.Interact(); 
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (cameraTransform == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * interactRange);
    }
}