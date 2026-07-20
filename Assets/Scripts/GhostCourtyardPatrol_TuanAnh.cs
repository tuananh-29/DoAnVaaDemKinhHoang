using UnityEngine;

public class GhostCourtyardPatrol_TuanAnh : MonoBehaviour
{
    [Header("Các điểm bay tuần tra quanh sân (kéo Empty GameObject đặt quanh sân vào)")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float flySpeed = 3f;

    [Header("Phát hiện qua cửa sổ")]
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private float detectionAngle = 60f; // góc nhìn phía trước ma
    [SerializeField] private LayerMask obstacleLayer; // tường/vật cản chặn tầm nhìn

    [Header("Khi phát hiện player")]
    [SerializeField] private float detectionCooldown = 1.5f; // giữ bao lâu mới báo phát hiện lại

    public System.Action OnPlayerSpotted; // đăng ký sự kiện này để tắt đèn/giảm an toàn khi bị phát hiện

    private int currentWaypointIndex = 0;
    private float lastDetectionTime = -999f;

    void Start()
    {
        if (player == null && Camera.main != null) player = Camera.main.transform;
    }

    void Update()
    {
        Patrol();
        CheckDetection();
    }

    private void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, flySpeed * Time.deltaTime);

        // Xoay ma hướng về phía đang bay tới, để "tầm nhìn" của nó khớp hướng di chuyển
        Vector3 direction = target.position - transform.position;
        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 2f);
        }

        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void CheckDetection()
    {
        if (player == null) return;
        if (Time.time - lastDetectionTime < detectionCooldown) return;

        Vector3 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        if (distance > detectionRange) return;

        float angle = Vector3.Angle(transform.forward, toPlayer);
        if (angle > detectionAngle * 0.5f) return;

        // Kiểm tra có vật cản (tường) chắn giữa ma và player không
        if (Physics.Raycast(transform.position, toPlayer.normalized, out RaycastHit hit, distance, obstacleLayer))
        {
            return; // bị tường chắn, không tính là phát hiện
        }

        // Phát hiện thành công
        lastDetectionTime = Time.time;
        Debug.Log("Ma ngoài sân đã phát hiện player qua cửa sổ!");
        OnPlayerSpotted?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
