using UnityEngine;

public class FallRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fall"))
        {
            if (!PlayerHealth.death)
            {
                transform.position = respawnPoint.position;
                Debug.Log("Fall");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
