using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HealthComponent))]
public class EnemyAI : MonoBehaviour
{
    private IEnemyState currentState;

    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;

    [Header("Navigation")]
    public NavMeshAgent agent;
    [HideInInspector] public int patrolIndex;

    [Header("Detection Settings")]
    public EnemyDetectionConfig detectionConfig;

    [Header("Death Settings")]
    public GameObject deathEffect;
    public float destroyDelay = 2f;

    [Header("Weapon")]
    public AIWeaponController equippedWeaponController;

    [HideInInspector] public HealthComponent health;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        // Auto-detect player if not manually assigned
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        health.OnDeath += OnDeath;

        SwitchState(new PatrolState());
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    public void SwitchState(IEnemyState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState?.EnterState(this);
    }

    private void OnDeath()
    {
        SwitchState(new DieState());
    }

    public bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 eyePos = transform.position + Vector3.up * 1.5f;
        Vector3 directionToPlayer = player.position - eyePos;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > detectionConfig.detectionRange)
            return false;

        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer.normalized);
        if (angleToPlayer > detectionConfig.viewAngle / 2f)
            return false;

        if (Physics.SphereCast(eyePos, detectionConfig.sphereCastRadius, directionToPlayer.normalized, out RaycastHit hit, detectionConfig.detectionRange, ~detectionConfig.ignoreVisionLayers))
        {
            if (hit.transform != player)
                return false;
        }

        return true;
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (detectionConfig == null || !detectionConfig.drawGizmos) return;

        Vector3 eyePos = transform.position + Vector3.up * 1.5f;
        float range = detectionConfig.detectionRange;
        float angle = detectionConfig.viewAngle;
        float radius = detectionConfig.sphereCastRadius;

        // Draw detection range
        Gizmos.color = detectionConfig.rangeColor;
        Gizmos.DrawWireSphere(transform.position, range);
        
        //Draw Stopping Dist
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionConfig.stoppingDistance);

        //Draw Attack Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionConfig.attackRange);

        // Draw FOV cone rays
        Vector3 forward = transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, -angle / 2f, 0) * forward;
        Vector3 rightDir = Quaternion.Euler(0, angle / 2f, 0) * forward;

        Gizmos.color = detectionConfig.viewConeColor;
        Gizmos.DrawRay(eyePos, leftDir * range);
        Gizmos.DrawRay(eyePos, rightDir * range);

        // Draw SphereCast beam
        if (player != null)
        {
            Vector3 toPlayer = player.position - eyePos;
            float distance = toPlayer.magnitude;

            if (distance <= range)
            {
                Vector3 dir = toPlayer.normalized;

                // Optional: perform the same SphereCast used in CanSeePlayer
                if (Physics.SphereCast(eyePos, radius, dir, out RaycastHit hit, range, ~detectionConfig.ignoreVisionLayers))
                {
                    Gizmos.color = hit.transform == player ? Color.black : Color.white;
                    Gizmos.DrawLine(eyePos, hit.point);
                    Gizmos.DrawWireSphere(hit.point, radius);
                }
                else
                {
                    // No hit — draw the full length
                    Gizmos.color = Color.gray;
                    Gizmos.DrawLine(eyePos, eyePos + dir * range);
                }

                // Draw source of cast
                Gizmos.color = new Color(1f, 0.5f, 0f, 0.3f);
                Gizmos.DrawWireSphere(eyePos, radius);
            }
        }
    }
#endif


}
