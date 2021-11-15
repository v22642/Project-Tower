using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;

    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        if(aiPath.desiredVelocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
        }
    }
}