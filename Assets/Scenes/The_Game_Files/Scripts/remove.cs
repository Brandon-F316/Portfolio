// the scatter parent object would remain despite it's children being destroyed
// this script removes the parent once the children are gone
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {

            Destroy(gameObject);

        }
    }
}
