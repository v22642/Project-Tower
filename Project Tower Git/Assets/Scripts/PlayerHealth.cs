using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 3, force = 100f, forceTorque = 30;
    public static bool death;
    Rigidbody2D rb;

    public bool devDeath;
    public bool revive;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            health++;
            death = false;
            revive = false;
            transform.rotation = Quaternion.identity;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void takeDamage(float Damage)
    {
        health -= Damage;
        if(health <= 0 && !death)
        {
            death = true;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            rb.AddTorque(forceTorque * Random.Range(-1f, 1f));
        }
    }
}
