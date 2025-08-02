using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpawnScript : MonoBehaviour
{
    public GameObject boss;
    private GameObject spawnedObject;
    public logicScript logic;
    public bool canSpawn = false;
    public int spawn;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedObject == null && logic.GetComponent<logicScript>().spawnNumber >= spawn)
        {
            canSpawn = true;
        }
        else
        {
            canSpawn = false;
        }

        if(canSpawn)
        {
            spawnBoss();
        }

    }

    void spawnBoss()
    {
        spawnedObject = Instantiate(boss, transform.position, Quaternion.Euler(180, 0, 90));
        canSpawn = false;
    }
}
