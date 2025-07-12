using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 instance;
    public GameObject Meteor1Prefab;
    public float minInstantiatevalue;
    public float maxInstantiatevalue;

    public float Meteor1DestroyTime = 10f;

    [Header("Particle Effects")]
    public GameObject explosion;
    private void Awake()
    {
        instance = this;
    }
    //private void Start()
    //{
    //    InvokeRepeating("InstantiateMeteor1", 1f, 2f);
    //}

    private void Start()
    {
        Invoke("StartMeteorInstantiation", 2f); // Delay by 3 seconds
    }

    void StartMeteorInstantiation()
    {
        InvokeRepeating("InstantiateMeteor1", 1f, 2f); // Start repeating after the delay
    }


    void InstantiateMeteor1()
    {

        float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        Vector3 Meteor1pos = new Vector3(Random.Range(minX, maxX), Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 1f, 0);
        GameObject Meteor1 = Instantiate(Meteor1Prefab, Meteor1pos, Quaternion.Euler(0f, 0f, 180f));
        Destroy(Meteor1, Meteor1DestroyTime);
    }
 
}