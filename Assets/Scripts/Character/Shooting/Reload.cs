using System.Collections;
using UnityEngine;

public class Reload : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Ammo ammo;
    [SerializeField] private float reloadTime;

    private bool isReloading;
    public bool IsReloading => isReloading;

    private void Update()
    {
        if(isReloading) return;

        if (playerInput.IsReloading && ammo.CurrentAmmo < ammo.MaxAmmo) 
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    public void StartAutoReload() 
    {
        if (!isReloading) 
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine() 
    {
        isReloading = true;
        
        yield return new WaitForSeconds(reloadTime);

        ammo.RefillAmmo();

        isReloading = false;
    }
}
