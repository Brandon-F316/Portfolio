using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class Life : MonoBehaviour
{
    private Rigidbody2D shipRB;
    public int life = 3;
    private bool isInvincible = false;
    public float invincibilityDurationSeconds;
    public float invincibilityDeltaTime;
    public float timer;
    //public logicScript logic;
    public Image playerHeart, playerHeart_1, playerHeart_2;
    public Text deathText;
    public AudioSource source1, source2;     // source1 = hurt player, source2 = player death
    [SerializeField]
    private GameObject model;


    // Start is called before the first frame update
    void Start()
    {
        deathText.enabled = false;
        shipRB = GetComponent<Rigidbody2D>();
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        //UnityEngine.Debug.Log("Collision!");

        if (collision.GetComponent<Collider2D>().tag == "pickUp")
        {
            //dropHealth healthPack = collision.GetComponent<dropHealth>();

            if (life != 3)
            {
                life += 1;
                if (life == 3)
                {
                    playerHeart_2.enabled = true;
                }
                if (life == 2)
                {
                    playerHeart_1.enabled = true;
                }

            }

            Destroy(collision.gameObject);

        }

        if (isInvincible) return;

        if (collision.GetComponent<Collider2D>().tag == "Enemy" || collision.GetComponent<Collider2D>().tag == "Bullet")
        {
            source1.Play();
            life -= 1;
            if (life == 2)
            {
                playerHeart_2.enabled = !playerHeart_2.enabled;
            }
            if (life == 1)
            {
                playerHeart_1.enabled = !playerHeart_1;
            }
            if (life == 0)
            {
                playerHeart.enabled = !playerHeart;
                Destroy(gameObject);
            }

            StartCoroutine(BecomeTemporarilyInvincible());
        }
       
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        //UnityEngine.Debug.Log("Player turned invincible!");
        isInvincible = true;

        for (float i = 0; i < invincibilityDurationSeconds; i += invincibilityDeltaTime)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            
            if (model.transform.localScale == Vector3.one)
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(Vector3.one);
            }
            
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        //Debug.Log("Player is no longer invincible!");
        ScaleModelTo(Vector3.one);
        isInvincible = false;
    }

    private void ScaleModelTo(Vector3 scale)
    {
        model.transform.localScale = scale;
    }


    void OnDestroy()
    {
        deathText.enabled = true;
    }


}
