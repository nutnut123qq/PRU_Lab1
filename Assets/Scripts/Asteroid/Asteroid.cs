using spaceExplorer.DamageSystem;
using spaceExplorer.Health;
using spaceExplorer.Player;
using System;
using UnityEngine;

namespace spaceExplorer.Asteroid
{
    public class Asteroid : MonoBehaviour, IDamageSource
    {
        private HealthSystem healthSystem;
        private DamageDealer damageDealer;
        private readonly float damage = 10f;
        private readonly float maxHealth = 20f;
        [SerializeField] private GameObject starPrefab;
        private void Awake()
        {
            damageDealer = GetComponent<DamageDealer>();
            healthSystem = GetComponent<HealthSystem>();
            healthSystem.Setup(maxHealth);
            healthSystem.OnDeath += OnDeath;
        }
        private void Update()
        {
            Vector3 playerPosition = Player.Player.Instance.transform.position;
            if (transform.position.y < playerPosition.y - 10f)
            {
                Destroy(gameObject);
            }
        }
        private void OnDeath(object sender, EventArgs e)
        {
            Instantiate(starPrefab, transform.position, Quaternion.identity);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Vulnerable target = collision.GetComponent<Vulnerable>();
            if (target)
            {
                damageDealer.DealDamage(target);
            }
        }
        float IDamageSource.GetDamage() => damage;
    }
}

