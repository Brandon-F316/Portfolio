using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoMovement : MonoBehaviour
{
    //public GameObject ufo;
    public float moveSpeedx = 5;
    public float moveSpeedy = 5;
    //public float spawnRate = 2f;
    //private float timer = 0f;
    //public float heightOffset = 8.5f;
    //public bool canSpawn = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (canSpawn)
        {
            if (timer < spawnRate)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                spawnUfo();
                timer = 0f;
            }
        }
        */
    }


    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        if (pos.x > 14)
        {
            pos.x -= moveSpeedx * Time.fixedDeltaTime;
        }
        if (pos.x < 14)
        {
            if (pos.y > 11)
            {
                pos.y = -11;
            }

            pos.y += .08f;
        }


        transform.position = pos;
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if(bullet!= null)
        {
                Destroy(gameObject);
                Destroy(bullet.gameObject);
        }
        Destructable destructable = collision.GetComponent<Destructable>();


    }
    */

}
