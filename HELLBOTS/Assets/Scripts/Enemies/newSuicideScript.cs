﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class newSuicideScript : MonoBehaviour
{
    private Rigidbody2D rigidB;
    public GameObject ExplosionPrefab;

    public float limiteWalkLeft;
    public float limiteWalkRight;
    public float walkSpeed = 40f;
    int direction = 1;
    private GameObject CurrentExplosion;
    enum typeStances { passive, follow, attack }

    typeStances stances = typeStances.passive;

    float enterFollowZone = 1345f;
    float exitFollowZone = 1500f;
    float attackDistance = 1000f;

    float distancePlayer;
    public Transform player;



    void Start()
    {
        rigidB = GetComponent<Rigidbody2D>();
        limiteWalkLeft = transform.position.x - GetComponent<CircleCollider2D>().radius;
        limiteWalkRight = transform.position.x + GetComponent<CircleCollider2D>().radius;

    }

    // Update is called once per frame
    void Update()
    {
        distancePlayer = Mathf.Abs(player.position.x - transform.position.x);

        switch (stances)
        {
            case typeStances.passive:


                rigidB = GetComponent<Rigidbody2D>();
                rigidB.velocity = new Vector2(walkSpeed * direction, rigidB.velocity.y);
                if (transform.position.x < limiteWalkLeft)
                {
                    direction = 1;
                }
                if (transform.position.x > limiteWalkRight)
                {

                    direction = -1;
                }
                if (distancePlayer < enterFollowZone)
                {
                    stances = typeStances.follow;
                }

                break;

            case typeStances.follow:
                rigidB = GetComponent<Rigidbody2D>();
                rigidB.velocity = new Vector2(walkSpeed * 4f * direction, rigidB.velocity.y);
                if (player.position.x > transform.position.x)
                {
                    direction = 1;
                }
                if (player.position.x < transform.position.x)
                {

                    direction = -1;
                }
                if (distancePlayer > exitFollowZone)
                {
                    stances = typeStances.passive;
                }
                

                break;

        }
        transform.localScale = new Vector3(0.46f * direction, 0.46f, 0.46f);

        
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Hellbot")
        {
            Explosion();
        }
    }
    public void Explosion()
    {
        //Hacer sonido de explosion
        rigidB.velocity = new Vector2(0, 0);
        CurrentExplosion = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Destroy(CurrentExplosion, 0.2f);
        Destroy(gameObject);
    }
}