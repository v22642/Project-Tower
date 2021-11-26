using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 2;
    SpriteRenderer sr;
    public Color damageColor;
    public float damageColorDuration = 0.2f;
    public HealthBar healthBar;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        healthBar.SetHealth(health);
    }

    public void takeDamage(int Damage)
    {
        health -= Damage;
        sr.color = damageColor;
        healthBar.SetHealth(health);
        Invoke(nameof(SetColorToWhite), damageColorDuration);
        if (health <= 0)
            Destroy(gameObject);
    }

    void SetColorToWhite()
    {
        sr.color = Color.white;
    }
}
