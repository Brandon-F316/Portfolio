using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{

    public Bullet bullet;
    //Vector2 direction;

    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.5f;
    public float shootDelaySeconds = 0.0f;
    public float shootTimer = 0f;
    public float delayTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       // direction = (transform.localRotation * Vector2.right).normalized;

        if (autoShoot)
        {
            if (delayTimer >= shootIntervalSeconds)
            {
                shoot();
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

    public void shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        //goBullet.direction = direction;

    }



}
