using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour movementBehaviour;
    [SerializeField] private MonoBehaviour attackBehaviour;
    [SerializeField] private MonoBehaviour sensorBehaviour;
    [SerializeField] private MonoBehaviour animationControllerBehaviour;
    [SerializeField] private EnemyUtilityAI utilityAI;

    private IEnemyMovement movement;
    private IEnemyAttack attack;
    private IEnemySensor sensor;

    private EnemyAnimationController animationController;

    private void Awake()
    {
        movement = movementBehaviour as IEnemyMovement;
        attack = attackBehaviour as IEnemyAttack;
        sensor = sensorBehaviour as IEnemySensor;
        animationController = animationControllerBehaviour as EnemyAnimationController;

        if (movement == null) Debug.LogError("Movement behaviour missing or wrong interface", this);
        if (attack == null) Debug.LogError("Attack behaviour missing or wrong interface", this);
        if (sensor == null) Debug.LogError("Sensor behaviour missing or wrong interface", this);
        if (utilityAI == null) Debug.LogError("UtilityAI missing", this);
        
        if(animationControllerBehaviour !=  null && animationController == null) 
        {
            Debug.LogError("Animation Controller assigned but failde to cast to EnemyAnimationController. Check component type.", this);
        }
    }

    private void Start()
    {
        movement.Initialize(transform);
        attack.Initialize(transform);

        sensor.Initialize(transform, utilityAI.PLayer);

        MeleeAttack meleeAttackComponent = attack as MeleeAttack;

        if (meleeAttackComponent != null) 
        {
            if (animationController != null) 
            {
                animationController.Initialize(meleeAttackComponent);
            }
            else 
            {
                Debug.LogError("Melle enemy is missing its AnimationController component!", this);
            }
        }

        utilityAI.Initialize(transform, sensor, movement,attack);
    }

    private void Update()
    {
        utilityAI.Tick();
    }
}
