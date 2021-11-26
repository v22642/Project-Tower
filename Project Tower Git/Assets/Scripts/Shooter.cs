using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootDelay;
    bool isShooting;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!PlayerHealth.death)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float rotateY = 0f;

            if (mousePos.x < transform.position.x)
            {
                rotateY = 180;
            }

            transform.eulerAngles = new Vector3(transform.rotation.x, rotateY, transform.rotation.z);

            if (Input.GetButton("Fire1") && !isShooting)
            {
                Shoot();
                anim.SetBool("isShoot", true);
                isShooting = true;
                Invoke(nameof(AnimShootToFalse), shootDelay);
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Destroy(bullet, 5f);
    }

    private void AnimShootToFalse()
    {
        anim.SetBool("isShoot", false);
        isShooting = false;
    }
}
