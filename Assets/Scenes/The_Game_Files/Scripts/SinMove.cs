using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMove : MonoBehaviour
{
    float sinCenter;
    public float amplitude;
    public float frequency;

    public bool inverted = false;

    // Start is called before the first frame update
    void Start()
    {
        sinCenter = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float sin = Mathf.Sin(pos.x * frequency) * amplitude;
        if (inverted)
        {
            sin *= -1;
        }
        pos.y = sinCenter + sin;

        transform.position = pos;
    }
}
