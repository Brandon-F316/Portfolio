using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMoveSet : MonoBehaviour
{
    public GameObject bullet;

    public bool autoShoot = true;
    public bool canSpawnFlyGuy = false;
    public float shootInterval = 0.5f;
    public float shootDelay = 2.0f;
    public float shootTimer = 0f;
    public float delayTimer = 0f;
    public GameObject fly_guy_group;
    public float spawnRate = 2f;
    private float timer = 0f;
    private float yOffset = 6;
    public Transform spawnPoint, shootingPoint_0, shootingPoint_1, shootingPoint_2;
    Transform player;
    Rigidbody2D rb;
    public bossHealth health;
    public int life;

    [SerializeField]
    private float speed;

    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<bossHealth>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(-speed, 0.0f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        intro();
        life = health.life;
        if (life == 90)
        {
            canSpawnFlyGuy = true;
            autoShoot = false;
        }
        if (life == 80)
        {
            canSpawnFlyGuy = false;
            autoShoot = true;
        }
        if (life == 70)
        {
            canSpawnFlyGuy = true;
            autoShoot = false;
        }
        if (life < 60 && life > 30 )
        {
            autoShoot = true;
            canSpawnFlyGuy = false;
            rotateShoot();
        }
        if (life <= 30)
        {
            reset();
            canSpawnFlyGuy = true;
        }

        if (canSpawnFlyGuy)
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

       // rotateShoot();
       // trackPlayer();



        if (autoShoot)
        {
            if (delayTimer >= shootDelay)
            {
                if (shootTimer >= shootInterval)
                {
                    Shoot();
                    shootTimer = 0;
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
            }
        }
    }


    public void Shoot()
    {
        Instantiate(bullet.gameObject, shootingPoint_0.position, shootingPoint_0.rotation);
        Instantiate(bullet.gameObject, shootingPoint_1.position, shootingPoint_1.rotation);
        Instantiate(bullet.gameObject, shootingPoint_2.position, shootingPoint_2.rotation);
    }

    void spawnFlyGuy()
    {
        Instantiate(fly_guy_group, spawnPoint.position + new Vector3(0, yOffset, 0), transform.rotation);
        //Instantiate(fly_guy_group, spawnPoint.position, transform.rotation);
    }

    void rotateShoot()
    {
        Vector2 middle = Vector2.MoveTowards(rb.position, Vector2.zero, 7 * Time.fixedDeltaTime);
        rb.MovePosition(middle);
        rotateSpeed = 200;
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        shootInterval = .2f;
    }

    void trackPlayer()
    {
        Vector2 target = new Vector2(rb.position.x, player.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

     void intro()
    {
        Vector2 pos = transform.position;
        if (pos.x > 12.5)
        {
            pos.x -= speed * Time.fixedDeltaTime;
        }
        if (pos.x < 12.5)
        {
            pos.x += speed * Time.fixedDeltaTime;
            trackPlayer();
        }
    }

    void reset()
    {
        transform.rotation = Quaternion.Euler(180, 0, 90);
        Vector2 back = Vector2.MoveTowards(rb.position, new Vector2(12.5f, player.position.y), 7 * Time.fixedDeltaTime);
        rb.MovePosition(back);
        shootInterval = 1.0f;
    }
}
