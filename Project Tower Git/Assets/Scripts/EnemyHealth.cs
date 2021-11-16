using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 2f;

    public void takeDamage(float Damage)
    {
        health -= Damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
