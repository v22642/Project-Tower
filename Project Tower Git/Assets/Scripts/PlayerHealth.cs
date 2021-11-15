using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 3, force = 100f, forceTorque = 30;
    public static bool death;
    Rigidbody2D rb;
    BoxCollider2D boxCol;
    public GameObject player;
    public Transform respawnPoint;

    public bool devDeath;
    public bool revive;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();

        boxCol.sharedMaterial.friction = 0f;
    }

    private void Update()
    {
        if (devDeath)
        {
            takeDamage(health);
            devDeath = false;
        }

        if (revive)
        {
            if (!gameObject)
            {
                Instantiate(player, respawnPoint);
            }
            health++;
            death = false;
            revive = false;
            transform.rotation = Quaternion.identity;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            boxCol.sharedMaterial.friction = 0f;
        }
    }

    public void takeDamage(float Damage)
    {
        health -= Damage;
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
