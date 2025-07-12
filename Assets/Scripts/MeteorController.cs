using spaceExplorer.DamageSystem;
using spaceExplorer.Health;
using UnityEngine;

public class MeterorController : MonoBehaviour
{
    public float Meterorspeed= 25f;
    public float maxHealth = 10f;
    private HealthSystem healthSystem;
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Setup(maxHealth);
        healthSystem.OnDeath += OnDeath;
    }
    private void OnDeath(object sender, System.EventArgs e)
    {
        GameObject gm = Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);
        Destroy(gm, 2f);
        Destroy(gameObject);
    }
    void Update()
    {
        transform.Translate(Vector3.up * Meterorspeed * Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject gm = Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);

            Destroy(gm, 2f);


            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

}
