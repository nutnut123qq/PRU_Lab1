using UnityEngine;
using spaceExplorer.Health;

namespace spaceExplorer.DamageSystem
{
    public class Vulnerable : MonoBehaviour
    {
        private Health.HealthSystem healthSystem;
        private void Awake()
        {
            healthSystem = GetComponent<Health.HealthSystem>();
        }

        // Method to take damage, reducing health
        public void TakeDamage(float damage)
        {
            if (healthSystem != null)
            {
                healthSystem.ReduceHealth(damage);
            }
        }
    }
}

