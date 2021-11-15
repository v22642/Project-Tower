using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    public float hitLength;
    public LayerMask hitLayer;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, hitLength, hitLayer);
        Debug.DrawLine(transform.position, Vector2.down, Color.blue);
    }
}
