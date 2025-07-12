using System;
using UnityEngine;

namespace spaceExplorer.Health
{
    public class HealthSystem : MonoBehaviour
    {
        public event EventHandler OnDeath;
        public event EventHandler OnHealthChanged;
        public class OnHealthChangedEventArgs : EventArgs
        {
            public float CurrentHealth {  get; private set; }
            public OnHealthChangedEventArgs(float currentHealth)
            {
                this.CurrentHealth = currentHealth;
            }
        }
        private float maxHealth;
        private float currentHealth;

        public void Setup(float maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        public void ReduceHealth(float amount)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Max(currentHealth, 0);
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(currentHealth));
            if (currentHealth == 0)
            {
                Die();
            }
        }

        public void IncreaseHealth(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Min(currentHealth, maxHealth);

            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs(currentHealth));
        }
        private void Die()
        {
            OnDeath?.Invoke(this, EventArgs.Empty);
            Debug.Log($"{gameObject.name} has died.");
            Destroy(gameObject); 
        }
    }
}

