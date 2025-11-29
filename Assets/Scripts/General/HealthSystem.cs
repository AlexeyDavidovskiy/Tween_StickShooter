using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private UnityEvent healthIsChanged;

    private int currentHealth;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if(currentHealth <= 0) return;

        currentHealth -= damage;

        healthIsChanged?.Invoke();
    }
    public void Heal(int healValue, int extraHealthValue)
    {
        if(currentHealth == 0 && currentHealth <= maxHealth)
        {
            currentHealth += healValue;
        }

        if (currentHealth >= maxHealth) 
        {
            currentHealth += extraHealthValue;
        }

        healthIsChanged?.Invoke();
    }
}