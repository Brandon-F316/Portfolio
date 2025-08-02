using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufo_Spawn_Script : MonoBehaviour
{

    public GameObject ufo;
    private GameObject spawnedObject;
    public bool canSpawn = false;
    public float spawnInterval = 0.5f;
    public float spawnDelay = 2.0f;
    public float spawnTimer = 0f;
    public float delayTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //timer = timer + Time.deltaTime;

        if (spawnedObject == null && GameObject.Find("MAMA_SPACESHIP(Clone)") == null)
        {
            canSpawn = true;
        }
        else canSpawn = false;

        if (canSpawn)
        {
            if (delayTimer >= spawnDelay)
            {
                if (spawnTimer >= spawnInterval)
                {
                    spawnUfo();
                    spawnTimer = 0;
                }
                else
                {
                    spawnTimer += Time.deltaTime;
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
            }

        }
    }


    void spawnUfo()
    {
        spawnedObject = Instantiate(ufo, new Vector3(transform.position.x, transform.position.y, 0.0f), transform.rotation);
        canSpawn = false;
    }
}
