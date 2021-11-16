using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    float currentAmount = 0f;
    public float slowMotionDuration = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Time.timeScale == 1.0f)
                Time.timeScale = 0.5f;
            else
                Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        if (Time.timeScale == 0.5f)
        {
            currentAmount += Time.deltaTime * 2f;
        }

        if (currentAmount > slowMotionDuration) 
        { 
            currentAmount = 0f;
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
}
