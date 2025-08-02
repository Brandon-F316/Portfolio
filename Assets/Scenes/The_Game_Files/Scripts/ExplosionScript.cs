using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float ExplosionDuration;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, ExplosionDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
