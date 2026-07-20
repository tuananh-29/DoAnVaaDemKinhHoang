using UnityEngine;

// Gắn vào từng vật thể chứa số (bảng đen, dòng chữ phấn, hoành phi...) đặt rải rác ở 6 phòng
public class ClueDigit_TuanAnh : MonoBehaviour
{
    [Header("Chữ số ẩn trong manh mối này (vd: \"4\")")]
    [SerializeField] private string digitValue;

    [Header("Vị trí chữ số này trong mã 4 chữ số (0=đầu tiên, 3=cuối)")]
    [SerializeField] private int positionInCode;

    [Header("Tham chiếu quản lý mã chung")]
    [SerializeField] private ClueCodeManager_TuanAnh codeManager;

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2.5f;

    private bool alreadyFound = false;

    void Start()
    {
        if (player == null && Camera.main != null) player = Camera.main.transform;
        if (codeManager == null) codeManager = FindObjectOfType<ClueCodeManager_TuanAnh>();
    }

    void Update()
    {
        if (alreadyFound) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange)
            {
                alreadyFound = true;
                codeManager.AddDigit(positionInCode, digitValue);
            }
        }
    }
}
