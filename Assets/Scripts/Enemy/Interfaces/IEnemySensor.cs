using UnityEngine;

public interface IEnemySensor 
{
    public void Initialize(Transform _self, Transform _player);
    public bool CanSeeTarget();
    public float DistanceToTarget();
    public Transform Target { get; }
}
