﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class flying_enemy : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate;
    public GameObject bulletPrefab;
    public GameObject throwZone;
    public float maxBorder;
    public bool MoveRight;
    public GameObject alert;
    public AudioClip AlertSound;
    public float WaitTime;

    private float nextFireTime;
    private GameObject bala;
    private GameObject player;
    private Vector2 CurrentPos;
    private AudioSource audioSource;
    private Rigidbody2D rb2d;
    private bool firstTimeSeen = true;
    private float WaitedTime = 0f;
    private int direction = 1;
    enum typeStances { passive, follow, attack }
    typeStances stances = typeStances.passive;
    void Start()
    {
        player = GameObject.FindWithTag("Hellbot");
        audioSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        nextFireTime = 0;
        CurrentPos = transform.position;
    }

    void FixedUpdate(){
        float delta = Time.deltaTime * 1000;
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            if (firstTimeSeen)
            {
                //hacer sonido
                if (WaitedTime == 0)
                {
                    audioSource.PlayOneShot(AlertSound);
                }

                //empezar a contar
                WaitedTime += delta;

                //set active alerta
                alert.SetActive(true);

                if (WaitedTime > WaitTime)
                {
                    //cuando el contador este tal poner firsttimeseen a fasle
                    firstTimeSeen = false;
                    //set active false alerta
                    alert.SetActive(false);
                    WaitedTime = 0;
                }


            }
            if (player.transform.position.x > transform.position.x)
            {
                direction = 1;
            }
            if (player.transform.position.x < transform.position.x)
            {

                direction = -1;
            }
            rb2d.velocity = new Vector2(speed * 1.5f * direction, rb2d.velocity.y);
            //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        } else if (distanceFromPlayer <= shootingRange){
            checkIfTimeToFire();
        }else{

            if (transform.position.x > CurrentPos.x + maxBorder && MoveRight)
            {
                MoveRight = false;
            }
            if (transform.position.x < CurrentPos.x - maxBorder && !MoveRight)
            {
                MoveRight = true;
            }

            if (distanceFromPlayer < lineOfSite)
            {
                stances = typeStances.follow;
            }

            if (MoveRight)
            {
                transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            }
            else
            {
                transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            }
        }

    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        
            if (collider.gameObject.tag == "Playerbullet")
            {
                Destroy(gameObject);
            }

            if (collider.gameObject.tag == "Explosion")
            {
                Destroy(gameObject);
            }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }
    void checkIfTimeToFire()
    {
        float delta = Time.deltaTime * 1000;
        nextFireTime += delta;
        if (nextFireTime > fireRate)
        {
            bala = Instantiate(bulletPrefab, throwZone.transform.position, Quaternion.identity);
            Destroy(bala, 4);
            nextFireTime = 0;
        }
    }
}