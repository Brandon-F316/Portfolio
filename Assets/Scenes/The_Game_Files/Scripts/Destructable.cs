using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    bool canBeDestroyed = false;
    public int scoreValue = 100;
    public logicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < 15.0f)
        {
            canBeDestroyed = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed)
        {
            return;
        }
        if (collision.GetComponent<Collider2D>().tag == "Player" || collision.GetComponent<Collider2D>().tag == "plBullet")
        {
            Destroy(gameObject);
            logic.GetComponent<logicScript>().AddScore(scoreValue);
        }
        Bullet bullet = collision.GetComponent<Bullet>();
        ExplosionScript eScript = collision.GetComponent<ExplosionScript>();
        scatterScript s_script = collision.GetComponent<scatterScript>();
        if (bullet != null)
        {
            Destroy(bullet.gameObject);
        }
        if (eScript != null)
        {
            Destroy(eScript.gameObject, 0.5f);
        }
        if (s_script != null)
        {
            Destroy(s_script.gameObject);
        }
    }
}
