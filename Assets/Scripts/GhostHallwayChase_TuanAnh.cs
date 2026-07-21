using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GhostHallwayChase_TuanAnh : MonoBehaviour
{
    private enum GhostState { Patrol, Chase }
    private GhostState currentState = GhostState.Patrol;

    [Header("2 điểm đi lại trong hành lang")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 4.5f;

    [Header("Phát hiện player")]
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRange = 6f;
    [SerializeField] private float detectionAngle = 70f;
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Mất dấu player sau bao lâu (giây) không thấy nữa thì quay lại patrol")]
    [SerializeField] private float loseTargetTime = 3f;

    [Header("Bắt được player (khoảng cách này coi như bắt được)")]
    [SerializeField] private float catchDistance = 1f;

    public System.Action OnPlayerCaught;

    private NavMeshAgent agent;
    private Transform currentTarget;
    private float lastSeenTime = -999f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null && Camera.main != null) player = Camera.main.transform;
        currentTarget = pointA;
        agent.speed = patrolSpeed;
    }

    void Update()
    {
        bool canSeePlayer = CheckVision();

        if (canSeePlayer)
        {
            lastSeenTime = Time.time;
            if (currentState != GhostState.Chase)
            {
                currentState = GhostState.Chase;
                agent.speed = chaseSpeed;
                Debug.Log("Ma hành lang phát hiện player, bắt đầu đuổi!");
            }
        }

        if (currentState == GhostState.Chase)
        {
            agent.SetDestination(player.position);

            if (Vector3.Distance(transform.position, player.position) <= catchDistance)
            {
                OnPlayerCaught?.Invoke();
            }

            // Mất dấu quá lâu -> quay lại tuần tra
            if (Time.time - lastSeenTime > loseTargetTime)
            {
                currentState = GhostState.Patrol;
                agent.speed = patrolSpeed;
                currentTarget = pointA;
            }
        }
        else // Patrol
        {
            agent.SetDestination(currentTarget.position);
            if (Vector3.Distance(transform.position, currentTarget.position) < 0.5f)
            {
                currentTarget = currentTarget == pointA ? pointB : pointA;
            }
        }
    }

    private bool CheckVision()
    {
        if (player == null) return false;

        Vector3 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;
        if (distance > detectionRange) return false;

        float angle = Vector3.Angle(transform.forward, toPlayer);
        if (angle > detectionAngle * 0.5f) return false;

        if (Physics.Raycast(transform.position, toPlayer.normalized, out RaycastHit hit, distance, obstacleLayer))
        {
            return false; // bị tường chắn
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = currentState == GhostState.Chase ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
