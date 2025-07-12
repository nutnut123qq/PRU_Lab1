using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerInstance;
    private Vector3 playerOriginPosition;
    private void Awake()
    {
        Spawn();
    }
    public void Spawn()
    {
        playerOriginPosition = Vector3.zero;
        if (playerInstance != null) return;
        playerInstance = Instantiate(playerPrefab, playerOriginPosition, Quaternion.identity);
    }
}
