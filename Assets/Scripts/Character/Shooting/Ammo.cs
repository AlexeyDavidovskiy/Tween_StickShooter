using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    [SerializeField] private Text ammoText;
    [SerializeField] private int maxAmmo;

    private int currentAmmo;

    public int MaxAmmo => maxAmmo;
    public int CurrentAmmo => currentAmmo;

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    public void UseAmmo() 
    {
        currentAmmo = Mathf.Max(0, currentAmmo - 1);
        UpdateAmmoUI();
    }

    public void RefillAmmo() 
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI() 
    {
        ammoText.text = currentAmmo.ToString();
    }
}
