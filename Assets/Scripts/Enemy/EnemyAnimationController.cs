using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private string attackAnimationBool = "Attack";

    private MeleeAttack meleeAttack;

    public void Initialize(MeleeAttack attackComponent) 
    {
        meleeAttack = attackComponent;

        if(anim == null) 
        {
            anim = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (meleeAttack != null)
        {
            bool isAttacking = meleeAttack.CanPlayAttackAnimation;

            anim.SetBool(attackAnimationBool, isAttacking);

            if (isAttacking) 
            {
                meleeAttack.AnimationStarted();
            }
        }
    }
}
