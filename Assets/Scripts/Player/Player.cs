using spaceExplorer.DamageSystem;
using System.Collections;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace spaceExplorer.Player
{
    public class Player : MonoBehaviour, IDamageSource
    {
        public static Player Instance { get; private set; }
        private PlayerMove playerMove;
        private PlayerAttack playerAttack;
        private readonly float damage = 10f;
        private PlayerEx playerExplode;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Keep player persistent
            }
            else
            {
                Destroy(gameObject); // Destroy duplicate instances
            }

            playerExplode = GetComponent<PlayerEx>();
            playerAttack = GetComponent<PlayerAttack>();
            playerAttack.DamageDealer = GetComponent<DamageDealer>();
            playerMove = GetComponent<PlayerMove>();
            playerMove.PlayerTransform = transform;

            transform.position = Vector3.zero; // Reset player position
            SceneManager.sceneLoaded += OnSceneLoaded;
            playerExplode.OnDeath += PlayerExplode_OnDeath;
        }
        private void PlayerExplode_OnDeath(object sender, System.EventArgs e)
        {
            playerAttack.DisableAction();
            Destroy(gameObject);
        }
        private void Start()
        {
            CinemachineCameraManager.Instance.SetFollowTarget(transform);
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(scene.name == "EndMenu")
            {
                Destroy(gameObject);
                return;
            }
            CinemachineCameraManager.Instance.SetFollowTarget(transform);


/*
            if (scene.name == "1-1")
            {
                gameObject.SetActive(true); // Enable player in scene 1-1
                CinemachineCameraManager.Instance.SetFollowTarget(transform);
                GetComponent<PlayerMove>().enabled = true; // Re-enable PlayerMove
            }
            else if (scene.name == "EndMenu")
            {
                gameObject.SetActive(false); // Disable player in End Menu
            }
            else
            {
                CinemachineCameraManager.Instance.SetFollowTarget(transform);
            }*/

        }
        float IDamageSource.GetDamage() // Implement GetDamage
        {
            return damage;
        }
        private void OnDestroy()
        {
            if (playerAttack != null)
            {
                playerAttack.DisableAction(); // Ensure input is disabled
            }
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}

