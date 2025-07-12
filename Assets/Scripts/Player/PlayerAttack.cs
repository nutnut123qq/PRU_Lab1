using spaceExplorer.DamageSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace spaceExplorer.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private InputSystem_Actions action;
        private readonly float laserRange = 100f;
        private LayerMask targetLayer;
        public DamageDealer DamageDealer {  get; set; }
        [SerializeField] private LineRenderer lazerRenderer;
        private AudioSource audioSource;
        [SerializeField] private AudioClip shootingBeamSFX;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            targetLayer = LayerMask.GetMask("Vulnerable");
            action = new InputSystem_Actions();
            action.Enable();
            lazerRenderer.enabled = false;
            action.Player.Attack.performed += OnShootPerformed;
        }

        /*private void OnEnable()
        {
            audioSource = GetComponent<AudioSource>();
            targetLayer = LayerMask.GetMask("Vulnerable");
            action = new InputSystem_Actions();
            action.Enable();
            lazerRenderer.enabled = false;
            action.Player.Attack.performed += OnShootPerformed;
        }*/
        private void OnShootPerformed(InputAction.CallbackContext context)
        {
            if (Player.Instance == null) return; 
            ShootLaser();
        }
        private void ShootLaser()
        {
            Ray ray = new Ray(transform.position, transform.up);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, laserRange, targetLayer);
            Vector3 endPosition = hit ? (Vector3)hit.point : ray.origin + ray.direction * laserRange;

            // Enable and set the LineRenderer positions
            lazerRenderer.enabled = true;
            lazerRenderer.SetPosition(0, transform.position); // Start point (player position)
            lazerRenderer.SetPosition(1, endPosition); // End point (either hit target or max range)

            // Hide the laser after a short delay
            Invoke(nameof(DisableLaser), 0.1f);

            // Check if the ray hits something
            if (hit)
            {
                Vulnerable target = hit.collider.GetComponent<Vulnerable>();
                DamageDealer.DealDamage(target);
                Debug.Log($"Hit {hit.collider.name} at {hit.point}");
            }

            // Play the shooting beam sound effect
            audioSource.PlayOneShot(shootingBeamSFX);
        }
        private void DisableLaser()
        {
            lazerRenderer.enabled = false;
        }
        public void DisableAction()
        {
            if(action == null) return;
            action.Player.Attack.performed -= OnShootPerformed;
            action.Player.Disable();
        }
        private void OnDisable()
        {
            DisableAction();
        }
        private void OnDestroy()
        {
            if (action != null)
            {
                action.Player.Attack.performed -= OnShootPerformed;
                action.Disable();
                action.Dispose(); // <Efinal cleanup
            }
        }

    }
}

