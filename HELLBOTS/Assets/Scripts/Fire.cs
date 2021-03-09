﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Rigidbody2D Bala2;
    public Transform Barrel;
    private float Bala2Speed = 500000f;

    private float fireRate = 1f;
    private float nextFire = 0f;
    private bool shoot;

    // Update is called once per frame
    void Update()
    {
        shoot = HellbotInput.Shoot;

        if (shoot && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            var spawnedBala2 = Instantiate(Bala2, Barrel.position, Barrel.rotation);
            spawnedBala2.AddForce(Barrel.right * Bala2Speed);
        }
    }
}