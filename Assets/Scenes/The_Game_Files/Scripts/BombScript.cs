using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    //public float speed;
    private Rigidbody2D BombRB;
    public GameObject explosion;
    public Transform ExplosionPoint;


    // Start is called before the first frame update
    void Start()
    {
        BombRB = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector3(speed, 0.0f, 0.0f);

    }

    void Update()
    {
        Vector2 pos = transform.position;
        if (pos.y < -12)
        {
            Destroy(gameObject);
        }

        //rb.velocity = new Vector3(speed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log(collision.GetComponent<Collider2D>().tag);

        if (collision.GetComponent<Collider2D>().tag =="Enemy")
        {
            Instantiate(explosion, ExplosionPoint.position + new Vector3(0,0,-1), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}