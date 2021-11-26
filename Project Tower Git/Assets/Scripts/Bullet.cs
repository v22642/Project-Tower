using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int bulletDamage = 1;

    //public Color damageColor;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 force = transform.right * bulletSpeed;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyHealth>().takeDamage(bulletDamage);
            //SpriteRenderer sr = col.gameObject.GetComponent<SpriteRenderer>();
            //sr.color = damageColor;
        }
        Destroy(gameObject);
    }
}
