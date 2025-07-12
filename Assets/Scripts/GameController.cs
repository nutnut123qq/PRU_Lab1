using UnityEngine;

public class GameController : MonoBehaviour
{ 
    public static GameController Instance {  get; private set; }
    private int points;
    private float time;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is existing instance of game handler");
        }
        Instance = this;
    }

    private void Start()
    {
        points = 0;
        time = 0;
    }
    private void Update()
    {
        time += Time.deltaTime;
    }

    public void AddPoints()
    {
        points++;
    }
    public int GetPoints() => points;
    public float GetTime() => time;
}
