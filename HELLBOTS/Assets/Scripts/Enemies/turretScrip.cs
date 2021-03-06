﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretScrip : MonoBehaviour
{

    public float shootingRange;
    public float fireRate = 1f;
    public float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Hellbot");
    }


void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Playerbullet")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Explosion")
        {
            Destroy(gameObject);
        }
    }
}