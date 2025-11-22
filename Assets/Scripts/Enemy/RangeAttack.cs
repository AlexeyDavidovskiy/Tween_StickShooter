using UnityEngine;

public class RangeAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private ParticleSystem shotFlash;
    [SerializeField] private float fireRate;
    [SerializeField] private float range;

    private Transform self;
    private float timer;

    public void Initialize(Transform _self)
    {
        self = _self;
    }

    public void TryAttack(Transform _target)
    {
        if(_target == null) return;

        timer += Time.deltaTime;
        if (timer < 1f / fireRate) return;
        timer = 0f;

        Vector3 dir = (_target.position - firePoint.position).normalized;

        Physics.Raycast(firePoint.position, dir, range);

        if (shotFlash != null) 
        {
            shotFlash.Play();
        }
    }
}
