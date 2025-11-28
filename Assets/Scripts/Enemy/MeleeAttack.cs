using UnityEngine;

public class MeleeAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField] private float attackRate;

    private bool canPlayAttackAnimation;
    public bool CanPlayAttackAnimation => canPlayAttackAnimation;

    private Transform self;
    private float attackTimer;

    public void Initialize(Transform _self)
    {
        self = _self;
    }

    public void TryAttack(Transform _target)
    {
        if(canPlayAttackAnimation) return;

        attackTimer += Time.deltaTime;

        if (attackTimer < 1f / attackRate) return;

        attackTimer = 0f;

        canPlayAttackAnimation = true;

        if (_target != null) 
        {
            Vector3 targetDirection = (_target.position - self.position).normalized;
            targetDirection.y = 0f;

            if (targetDirection != Vector3.zero) 
            {
                self.rotation = Quaternion.LookRotation(targetDirection);
            }
        }
    }

    public void AnimationStarted() 
    {
        canPlayAttackAnimation = false;
    }
}
