using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scatterScript : MonoBehaviour
{
    public float x;
    public float y;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(x, y, 0.0f);

    }

    // Update is called once per frame
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
}
