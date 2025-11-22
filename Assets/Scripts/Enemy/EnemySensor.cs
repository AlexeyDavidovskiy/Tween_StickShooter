using UnityEngine;

public class EnemySensor : MonoBehaviour, IEnemySensor
{
    [SerializeField] private Transform player;
    [SerializeField] private float viewDistance;

    private Transform self;

    public Transform Target => player;
    public void Initialize(Transform _self, Transform _player)
    {
       self = _self;
       player = _player;
    }

    public bool CanSeeTarget()
    {
        if (player == null) return false;
        return DistanceToTarget() <= viewDistance;
    }

    public float DistanceToTarget()
    {
        if(player == null) return float.MaxValue;
        return Vector3.Distance(self.position, player.position);
    }
}
