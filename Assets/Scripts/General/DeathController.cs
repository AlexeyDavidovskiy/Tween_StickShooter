using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    public void Death() 
    {
        if(healthSystem.CurrentHealth <= 0) 
        {
            gameObject.SetActive(false);
        }
    }
}
