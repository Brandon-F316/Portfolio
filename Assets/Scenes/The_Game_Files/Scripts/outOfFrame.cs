using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outOfFrame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -17.0f)
        {
            Destroy(gameObject);
        }
    }
}
