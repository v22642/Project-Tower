using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 1f, punchForce = 10f, attackSpeed = 0.8f;
    private Coroutine dmg;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            if(dmg == null)
            {
                dmg = StartCoroutine(setDamage(other));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            StopCoroutine(dmg);
            dmg = null;
        }
    }

    IEnumerator setDamage(Collider2D other)
    {
        while (true && !PlayerHealth.death)
        {
            other.transform.parent.GetComponent<PlayerHealth>().takeDamage(damage);
            other.transform.parent.GetComponent<Rigidbody2D>().AddForce(Vector2.left * punchForce, ForceMode2D.Impulse);
            other.transform.parent.GetComponent<Rigidbody2D>().AddTorque(20 * Random.Range(-1, 1));
            yield return new WaitForSeconds(attackSpeed);
        }
    }
}
