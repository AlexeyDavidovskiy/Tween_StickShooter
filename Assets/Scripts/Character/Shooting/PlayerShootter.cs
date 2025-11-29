using UnityEngine;

public class PlayerShootter : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Ammo ammo;
    [SerializeField] private Reload reload;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private ParticleSystem[] shotFlashes;
    [SerializeField] private ParticleSystem hitFlashesPrefab;

    [Header("Shootting Settings")]
    [SerializeField] private float fireRate;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask enemyLayerMask;

    private int currentGunIndex;
    private float nextFireTime;
    private bool shotRequested;

    private void Update()
    {
        if (reload.IsReloading) return;

        if (ammo.CurrentAmmo <= 0)
        {
            reload.StartAutoReload();
            return;
        }

        if (playerInput.IsShooting && Time.time >= nextFireTime && !shotRequested)
        {
            Shoot();
            shotRequested = true;
        }

        if (!playerInput.IsShooting)
        {
            shotRequested = false;
        }
    }

    private void Shoot()
    {
        if (ammo.CurrentAmmo <= 0 || reload.IsReloading) return;

        Transform firePoint = firePoints[currentGunIndex];
        ParticleSystem flash = shotFlashes[currentGunIndex];

        flash?.Play();

        Ray camRay = mainCamera.ScreenPointToRay(playerInput.LookInput);
        Vector3 targetPoint;

        if (Physics.Raycast(camRay, out RaycastHit camHit, range))
        {
            targetPoint = camHit.point;
        }
        else
        {
            targetPoint = camRay.origin + camRay.direction * range;
        }

        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

        if (Physics.Raycast(firePoint.position, shootDirection, out RaycastHit hit, range, enemyLayerMask))
        {
            if(hit.collider.TryGetComponent(out IDamageable damageable)) 
            {
                damageable.TakeDamage(damage);
            }

            ParticleSystem hitFX = Instantiate(hitFlashesPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            hitFX.Play();
            Destroy(hitFX.gameObject, 0.4f);
        }

        currentGunIndex = (currentGunIndex + 1) % firePoints.Length;

        ammo.UseAmmo();

        nextFireTime = Time.time + fireRate;
    }
}
