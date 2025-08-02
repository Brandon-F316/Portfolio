using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    public float xBorder;
    public float yBorder;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        Vector2 pos = transform.position;

        if (pos.x <= -xBorder)
        {
            pos.x = -xBorder;
        }
        if (pos.x >= xBorder)
        {
            pos.x = xBorder;
        }
        if (pos.y <= -yBorder)
        {
            pos.y = -yBorder;
        }
        if (pos.y >= yBorder)
        {
            pos.y = yBorder;
        }

        transform.position = pos;
    }
}
