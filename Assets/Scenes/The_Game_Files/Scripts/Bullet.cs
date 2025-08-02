using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(speed, 0.0f, 0.0f);

    }

    
    void Update()
    {
        if (transform.position.x < -17.0f || transform.position.x > 17.0f)
        {
            Destroy(gameObject);
        }

    }


}
