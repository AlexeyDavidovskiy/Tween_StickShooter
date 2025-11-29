using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar() 
    {
        healthSlider.value = healthSystem.CurrentHealth;
    }
}
