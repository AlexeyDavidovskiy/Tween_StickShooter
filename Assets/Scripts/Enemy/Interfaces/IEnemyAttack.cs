using UnityEngine;

public interface IEnemyAttack 
{
    public void Initialize(Transform _self);
    public void TryAttack(Transform _target);
}
