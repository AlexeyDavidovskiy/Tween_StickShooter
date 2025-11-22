using UnityEngine;

public enum EnemyAction 
{
    Patrol,
    Chase,
    Attack,
    Retreat
}

public class EnemyUtilityAI : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private AnimationCurve patrolCurve;
    [SerializeField] private AnimationCurve chaseCurve;
    [SerializeField] private AnimationCurve attackCurve;
    [SerializeField] private AnimationCurve retreatCurve;

    private Transform self;
    private IEnemySensor sensor;
    private IEnemyMovement movement;
    private IEnemyAttack attack;

    public Transform PLayer => player;

    public void Initialize(Transform _self, IEnemySensor _sensor, IEnemyMovement _movement, IEnemyAttack _attack)
    {
        self = _self;
        sensor = _sensor;
        movement = _movement;
        attack = _attack;
    }

    public void Tick() 
    {
        float distance = sensor.DistanceToTarget();

        float patrolScore = patrolCurve.Evaluate(distance);
        float chaseScore = chaseCurve.Evaluate(distance);
        float attackScore = attackCurve.Evaluate(distance);
        float retreatScore = retreatCurve.Evaluate(distance);

        EnemyAction bestAction = EnemyAction.Patrol;
        float bestScore = patrolScore;

        if (chaseScore > bestScore) { bestScore = chaseScore; bestAction = EnemyAction.Chase; }
        if (attackScore > bestScore) { bestScore = attackScore; bestAction = EnemyAction.Attack; }
        if (retreatScore > bestScore) { bestScore = retreatScore; bestAction = EnemyAction.Retreat; }

        switch (bestAction)
        {
            case EnemyAction.Patrol:
                movement.Patrol();
                break;
            case EnemyAction.Chase:
                movement.Chase(player);
                break;
            case EnemyAction.Attack:
                attack.TryAttack(player);
                break;
            case EnemyAction.Retreat:
                movement.RetreatFrom(player);
                break;
        }
    }
}
