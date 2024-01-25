using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator; // Assume you have an Animator

    public float viewRadius;
    public float viewAngle;
    public float attackDistance; // The distance within which the alien can attack
    public float attackRate; // How often the alien can attack (attacks per second)

    private float lastAttackTime = 0f; // When the last attack happened

    Transform attackTrigger;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        attackTrigger = transform.Find("AttackTrigger");
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (IsInView() && !IsTargetBlockedByObstacles(target))
        {
            if (distanceToTarget <= viewRadius)
            {
                agent.SetDestination(target.position);
                SetMovementAnimation(true);

                if (distanceToTarget <= attackDistance)
                {
                    AttackTarget();
                }
            }
            else
            {
                SetMovementAnimation(false);
            }
        }
        else
        {
            SetMovementAnimation(false);
        }
    }
    bool IsInView()
    {
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        return Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2;
    }

    void AttackTarget()
    {
        // Check if enough time has passed since the last attack
        if (Time.time - lastAttackTime >= 1f / attackRate)
        {
            // Trigger the attack animation
            animator.SetTrigger("Attack");

            // Activate the attack trigger or other attack logic here
            // For example, turn on an AttackTrigger child of the alien
            
            if (attackTrigger != null)
            {
                // Assume AttackTrigger has a script that activates the attack
                attackTrigger.gameObject.SetActive(true);
            }

            // Update the time for the last attack
            lastAttackTime = Time.time;
        }
    }
    void SetMovementAnimation(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }

    bool IsTargetBlockedByObstacles(Transform target)
    {
        RaycastHit hit;
        Vector3 direction = target.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit, viewRadius))
        {
            if (hit.transform != target)
            {
                return true; // There is an obstacle between the enemy and the target
            }
        }
        return false; // No obstacles, target is visible
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(viewAngle / 2, transform.up) * transform.forward * viewRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-viewAngle / 2, transform.up) * transform.forward * viewRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
    }


    public void OnDeath()
    {
        animator.SetTrigger("StateOn");
        animator.SetInteger("State", 10);
        this.enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}
