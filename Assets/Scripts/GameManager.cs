using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject MeteorPrefab;
    public float minInstantiatevalue;
    public float maxInstantiatevalue;

    public float MeteorDestroyTime = 10f;

    [Header("Particle Effects")]
    public GameObject explosion;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Invoke("StartMeteorInstantiation", 1f); // Delay by 3 seconds
    }

    void StartMeteorInstantiation()
    {
        InvokeRepeating("InstantiateMeteor", 1f, 2f); // Start repeating after the delay
    }


    void InstantiateMeteor()
    {
        float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        Vector3 Meteorpos = new Vector3(Random.Range(minX, maxX), Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 1f, 0);
        GameObject Meteor = Instantiate(MeteorPrefab, Meteorpos, Quaternion.Euler(0f, 0f, 180f));
        Destroy(Meteor, MeteorDestroyTime);
    }
 
}