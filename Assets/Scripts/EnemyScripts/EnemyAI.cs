using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;

    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    [SerializeField] Collider explotionArea;
    [SerializeField] Transform explotionParticle;

    [SerializeField] float damageAmount = 4;

    NavMeshAgent navMeshAgent;
    PlayerHealth playerHealth;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    void EngageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        Debug.Log(name + " is attacking " + target.name);

        Instantiate(explotionParticle, transform.position, Quaternion.identity);
        explotionArea.enabled = true;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.Damage(damageAmount);
            Debug.Log("Youve been hit dude");
            Destroy(gameObject);
        }
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
