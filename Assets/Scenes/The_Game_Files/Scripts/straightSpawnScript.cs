using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class straightSpawnScript : MonoBehaviour
{
    public GameObject fly_guy_group;
    public float spawnRate = 2f;
    private float timer = 0f;
    public float heightOffset = 8.5f;
    public bool canSpawn = false;

    // Start is called before the first frame update
    void Start()
    {

        //canSpawn = true;
        //boss = GameObject.Find("MAMA_SPACESHIP").GetComponent<bossHealth>();
        //m_Renderer = boss.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("MAMA_SPACESHIP(Clone)") != null)
        {
            canSpawn = false;
        }
        else canSpawn = true;

        if (canSpawn)
        {
            if (timer < spawnRate)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                spawnFlyGuy();
                timer = 0f;
            }
        }
    }

    void spawnFlyGuy()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(fly_guy_group, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0.0f), transform.rotation);
    }
}