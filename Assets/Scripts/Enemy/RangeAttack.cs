using UnityEngine;

public class RangeAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private ParticleSystem shotFlash;
    [SerializeField] private float fireRate;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask playerLayerMask;

    private Transform self;
    private float timer;

    public void Initialize(Transform _self)
    {
        self = _self;
    }

    public void TryAttack(Transform _target)
    {
        if (_target == null) return;

        timer += Time.deltaTime;
        if (timer < 1f / fireRate) return;
        timer = 0f;

        Vector3 dir = (_target.position - firePoint.position).normalized;

        if (Physics.Raycast(firePoint.position, dir, out RaycastHit hit, range, playerLayerMask))
        {
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }

        //if (Physics.Raycast(firePoint.position, dir, out RaycastHit hit, range, playerLayerMask))
        //{
        //    Debug.DrawRay(firePoint.position, dir * hit.distance, Color.red, 1f); // Попал

        //    if (hit.collider.TryGetComponent(out IDamageable damageable))
        //    {
        //        damageable.TakeDamage(damage);
        //    }
        //}
        //else
        //{
        //    Debug.DrawRay(firePoint.position, dir * range, Color.green, 1f); // Не попал
        //}

        if (shotFlash != null)
        {
            shotFlash.Play();
        }
    }
}
