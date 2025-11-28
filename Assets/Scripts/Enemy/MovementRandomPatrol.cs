using UnityEngine;
using UnityEngine.AI;

public class MovementRandomPatrol : MonoBehaviour, IEnemyMovement
{
    [SerializeField] private float patrolRadius;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private NavMeshAgent agent;



    private Transform self;
    private Vector3 currentTarget;
    private bool hasTarget;

    public void Initialize(Transform _self)
    {
        self = _self;

        if (agent == null)
        {
            agent = self.GetComponent<NavMeshAgent>();
        }

        agent.stoppingDistance = stoppingDistance;

        ChooseNewPatrolPoint();
    }

    private void ChooseNewPatrolPoint()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius + self.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
            {
                if (Vector3.Distance(hit.position, self.position) > 1f)
                {
                    currentTarget = hit.position;
                    hasTarget = true;
                    agent.isStopped = false;
                    agent.SetDestination(currentTarget);
                    return;
                }
            }
        }

        hasTarget = false;
    }

    public void Patrol()
    {
        if (agent.isStopped) agent.isStopped = false;

        if (!hasTarget || (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending))
        {
            ChooseNewPatrolPoint();
        }
    }

    public void Chase(Transform _target)
    {
        agent.isStopped = false;
        agent.stoppingDistance = 0f;
        agent.SetDestination(_target.position);
    }

    public void RetreatFrom(Transform _target)
    {
        agent.isStopped = false;
        agent.stoppingDistance = 0f;

        Vector3 dir = (self.position - _target.position).normalized;
        Vector3 retreatPoint = self.position + dir * patrolRadius;

        if(NavMesh.SamplePosition(retreatPoint, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas)) 
        {
            agent.SetDestination(hit.position);
        }
    }

    public void StopMovement()
    {
        if (agent.isActiveAndEnabled && !agent.isStopped) 
        {
            agent.isStopped = true;

            agent.stoppingDistance = stoppingDistance;

            if (agent.hasPath) 
            {
                agent.ResetPath();
            }
        }
    }
}
