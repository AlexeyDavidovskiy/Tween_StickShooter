using UnityEngine;
using System.Collections.Generic;

public class MeleeDamageDealer : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private LayerMask playerLayerMAsk;


   

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageable damageable)) 
        {
            damageable.TakeDamage(damage);
        }
    }
}
