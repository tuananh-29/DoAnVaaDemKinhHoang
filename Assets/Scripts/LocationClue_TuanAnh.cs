using UnityEngine;

public class LocationClue_TuanAnh : MonoBehaviour
{
    [SerializeField] private string myIdentifier;
    [SerializeField] private string correctIdentifier;

    [Header("Tùy chọn A: hiện vật phẩm khi đúng (vd chìa khóa để nhặt)")]
    [SerializeField] private GameObject rewardItem;

    [Header("Tùy chọn B: mở khóa + mở các cánh cửa khi đúng (kéo 1 hoặc nhiều cửa, vd cửa đôi)")]
    [SerializeField] private DoorInteract_TuanAnh[] doorsToUnlock;

    [Header("Tùy chọn C: cho mã/thông tin khi đúng")]
    [SerializeField] private string codeToGive;
    [SerializeField] private string codeSourceName = "Puzzle";
    [SerializeField] private PuzzleCodeCollector_TuanAnh codeCollector;

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2.5f;

    private bool alreadyChecked = false;

    void Start()
    {
        if (player == null && Camera.main != null) player = Camera.main.transform;
        if (codeCollector == null) codeCollector = FindObjectOfType<PuzzleCodeCollector_TuanAnh>();
        if (rewardItem != null) rewardItem.SetActive(false);
    }

    void Update()
    {
        if (alreadyChecked) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange) CheckThis();
        }
    }

    private void CheckThis()
    {
        alreadyChecked = true;
        if (myIdentifier == correctIdentifier)
        {
            Debug.Log($"[{myIdentifier}] Đúng rồi!");

            if (rewardItem != null)
            {
                rewardItem.SetActive(true);
            }

            if (doorsToUnlock != null)
            {
                foreach (var door in doorsToUnlock)
                {
                    if (door != null)
                    {
                        door.Unlock();
                        door.ToggleDoor();
                    }
                }
                Debug.Log("Cửa đã mở khóa!");
            }

            if (!string.IsNullOrEmpty(codeToGive) && codeCollector != null)
            {
                codeCollector.AddCode(codeSourceName, codeToGive);
            }
        }
        else
        {
            Debug.Log($"[{myIdentifier}] Không có gì đặc biệt ở đây.");
        }
    }
}