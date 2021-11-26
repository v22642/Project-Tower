using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float force = 100f, forceTorque = 30;
    public int maxHealth = 3, health = 3;
    public static bool death;
    Rigidbody2D rb;
    BoxCollider2D boxCol;
    public GameObject player;
    public Transform respawnPoint;
    public HealthBar healthBar;

    public bool devDeath;
    public bool revive;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        boxCol.sharedMaterial.friction = 0f;

        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            takeDamage(1);
        }

        if (devDeath)
        {
            takeDamage(health);
            devDeath = false;
        }

        if (revive)
        {
            if (!gameObject)
            {
                Instantiate(player, respawnPoint.transform.position, gameObject.transform.rotation);
            }
            health = maxHealth;
            death = false;
            revive = false;
            transform.rotation = Quaternion.identity;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            boxCol.sharedMaterial.friction = 0f;
            healthBar.SetHealth(health);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Health"))
        {
            health = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            Destroy(col.gameObject);
        }
    }

    public void takeDamage(int Damage)
    {
        health -= Damage;
        healthBar.SetHealth(health);

        if(health <= 0 && !death)
        {
            death = true;
            boxCol.sharedMaterial.friction = 1f;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            rb.AddTorque(forceTorque * Random.Range(-1f, 1f));
        }
    }
}
