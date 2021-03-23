using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class flying_enemy : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;
    private GameObject bala;
    public GameObject bulletPrefab;
    private GameObject player;
    public bool MoveRight;
    private Vector2 StarterPos;

    enum typeStances { passive, follow, attack }
    typeStances stances = typeStances.passive;
    void Start()
    {
        player = GameObject.FindWithTag("Hellbot");
        nextFireTime = 0;
        StarterPos = transform.position;
    }
    void Update()
    {
        float delta = Time.deltaTime * 1000;

    }
    private void FixedUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);


        switch (stances)
        {
            case typeStances.passive:
                {
                    if (distanceFromPlayer < lineOfSite)
                    {
                        stances = typeStances.follow;
                    }
                    break;
                }
            case typeStances.follow:
                {
                    if (player.transform.position.x > transform.position.x)
                    {
                        MoveRight = true;
                    }
                    else if (player.transform.position.x < transform.position.x)
                    {
                        MoveRight = false;
                    }

                    if (distanceFromPlayer > lineOfSite)
                    {
                        stances = typeStances.passive;
                        transform.position = StarterPos;
                    }
                    if (distanceFromPlayer <= shootingRange)
                    {
                        stances = typeStances.attack;
                    }
                    break;
                }
            case typeStances.attack:
                {
                    checkIfTimeToFire();
                    if (distanceFromPlayer > shootingRange)
                    {
                        stances = typeStances.follow;
                    }
                    break;
                }


        }

        if (MoveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-1, -1);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("TurnPoint"))
        {
            if (stances == typeStances.passive)
            {
                if (MoveRight)
                {
                    MoveRight = false;
                }
                else
                {
                    MoveRight = true;
                }
            }
            if (collider.gameObject.tag == "Playerbullet")
            {
                Destroy(gameObject);
            }

            if (collider.gameObject.tag == "Explosion")
            {
                Destroy(gameObject);
            }


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
            bala = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Destroy(bala, 4);
            nextFireTime = 0;
        }
    }
}