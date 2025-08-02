using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;

    public bool autoShoot = false;
    public float shootInterval = 0.5f;
    public float shootDelay = 2.0f;
    public float shootTimer = 0f;
    public float delayTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (autoShoot)
        {
            if (delayTimer >= shootDelay)
            {
                if (shootTimer >= shootInterval)
                {
                    Shoot();
                    shootTimer = 0;
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
            }
        }
    }


public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.Euler(0, 0, 180));
    }
}
