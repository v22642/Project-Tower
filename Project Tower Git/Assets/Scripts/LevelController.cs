using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Transform enemyRespawn;
    public GameObject enemyPrefab;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N) && !PlayerHealth.death)
        {
            Instantiate(enemyPrefab, enemyRespawn.transform.position, enemyRespawn.transform.rotation);
        }
    }
}
