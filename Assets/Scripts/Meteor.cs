using UnityEngine;
using spaceExplorer.DamageSystem;

public class Meteor : MonoBehaviour
{
    private DamageDealer damageDealer;

    private void Awake()
    {
        damageDealer = GetComponent<DamageDealer>(); // Get DamageDealer component
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vulnerable vulnerable = collision.gameObject.GetComponent<Vulnerable>();
            if (vulnerable != null && damageDealer != null)
            {
                damageDealer.DealDamage(vulnerable); // Deal damage using DamageDealer
            }
        }
    }
}