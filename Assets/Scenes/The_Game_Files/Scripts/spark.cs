using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spark : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    //[SerializeField]
    // private GameObject greenParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        //rb.velocity = new Vector3(speed, 0.0f, 0.0f);

    }

    void Update()
    {
        if (transform.position.x < -17.0f || transform.position.x > 17.0f)
        {
            Destroy(gameObject);
        }

        if (transform.position.y < -10.0f || transform.position.y > 10.0f)
        {
            Destroy(gameObject);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //Debug.Log(collision.GetComponent<Collider2D>().tag);

        if (collision.GetComponent<Collider2D>().tag == "Player")
        {
            Destroy(gameObject);

        }

    }
}