using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class shoot : MonoBehaviour
{

    public int selectedWeapon;
    public GameObject[] Weapons;
    public Transform shootingPoint;
    public Transform bombPoint;
    private float scatterTimeCounter;
    private float laserTimeCounter;
    public float scatterTime;
    public float laserTime;
    public bool laserCanShoot = true;
    public bool scatterCanShoot = true;

    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        shootDelay();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2;
        }


        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            //instantiate spawns a clone of first argument bulletPrefab
            //shootingPoint is the position of spawn
            //Quaternion.identity is saying to spawn in rotation (0,0,0)
            if (selectedWeapon == 1)
            {
                Instantiate(Weapons[selectedWeapon], bombPoint.position, Quaternion.Euler(0, 0, 180));
            }
            else if (selectedWeapon == 2)
            {
                if (scatterCanShoot == true)
                {
                    bulletShoot();
                    scatterCanShoot = false;
                    scatterTimeCounter = 0;
                }
            }
            else
            {
                if (laserCanShoot == true)
                {
                    bulletShoot();
                    laserCanShoot = false;
                    laserTimeCounter = 0;
                }
            }

        }

    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (GameObject weapon in Weapons)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
        }
    }

    void shootDelay()
    {
        if (scatterCanShoot == false)
        {
            scatterTimeCounter += Time.deltaTime;
            if (scatterTimeCounter >= scatterTime)
            {
                scatterCanShoot = true;
                scatterTimeCounter = scatterTime;
            }

        }
        if (laserCanShoot == false)
        {
            laserTimeCounter += Time.deltaTime;
            if (laserTimeCounter >= laserTime)
            {
                laserCanShoot = true;
                laserTimeCounter = laserTime;
            }

        }
    }

    void bulletShoot()
    {
        Instantiate(Weapons[selectedWeapon], shootingPoint.position, Quaternion.identity);
    }
}