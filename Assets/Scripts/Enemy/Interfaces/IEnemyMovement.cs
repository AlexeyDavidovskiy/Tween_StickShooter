using UnityEngine;

public interface IEnemyMovement 
{
    public void Initialize(Transform _self);
    public void Patrol();
    public void Chase(Transform _target);
    public void RetreatFrom(Transform _target);
    public void StopMovement();
}
