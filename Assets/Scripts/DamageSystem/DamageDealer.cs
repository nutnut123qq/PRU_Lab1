using UnityEngine;

namespace spaceExplorer.DamageSystem
{
    public class DamageDealer : MonoBehaviour
    {
        private IDamageSource damageSource;
        private void Awake()
        {
            damageSource = GetComponent<IDamageSource>();
        }
        // Call this method to deal damage to a Vulnerable target
        public void DealDamage(Vulnerable target)
        {
            if (target != null)
            {
                Debug.Log("Deal: " + damageSource.GetDamage());
                target.TakeDamage(damageSource.GetDamage());
            }
        }
    }
}

