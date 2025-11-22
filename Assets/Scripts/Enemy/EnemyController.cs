using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour movementBehaviour;
    [SerializeField] private MonoBehaviour attackBehaviour;
    [SerializeField] private MonoBehaviour sensorBehaviour;
    [SerializeField] private EnemyUtilityAI utilityAI;

    private IEnemyMovement movement;
    private IEnemyAttack attack;
    private IEnemySensor sensor;

    private void Awake()
    {
        movement = movementBehaviour as IEnemyMovement;
        attack = attackBehaviour as IEnemyAttack;
        sensor = sensorBehaviour as IEnemySensor;

        if (movement == null) Debug.LogError("Movement behaviour missing or wrong interface", this);
        if (attack == null) Debug.LogError("Attack behaviour missing or wrong interface", this);
        if (sensor == null) Debug.LogError("Sensor behaviour missing or wrong interface", this);
        if (utilityAI == null) Debug.LogError("UtilityAI missing", this);
    }

    private void Start()
    {
        movement.Initialize(transform);
        attack.Initialize(transform);

        sensor.Initialize(transform, utilityAI.PLayer);
        utilityAI.Initialize(transform, sensor, movement,attack);
    }

    private void Update()
    {
        utilityAI.Tick();
    }
}
