using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_controller : MonoBehaviour
{
    public GameObject bala;
    public float fireRate;
    private float nextFire;
    private float limiteWalkLeft;
    private float limiteWalkRight;
    public float walkSpeed = 40f;
    public float hitPoints;
    public float maxHitPoints = 5;
    private int direction = 1;
    //public GameObject castPoint;
    enum typeStances { passive, follow, attack }

    typeStances stances = typeStances.passive;

    float enterFollowZone = 1345f;
    float exitFollowZone = 1500f;
    float attackDistance = 1000f;

    float distancePlayer;
    private GameObject player;


    private Rigidbody2D rigidB;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindWithTag("Hellbot");

        
        nextFire = Time.time;

        //Los límites para la patrulla

        rigidB = GetComponent<Rigidbody2D>();
        limiteWalkLeft = transform.position.x - GetComponent<CircleCollider2D>().radius;
        limiteWalkRight = transform.position.x + GetComponent<CircleCollider2D>().radius;
        hitPoints = maxHitPoints;
    }





    // Update is called once per frame
    void Update()
    {
        distancePlayer = Mathf.Abs(player.transform.position.x - transform.position.x);
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
                rigidB.velocity = new Vector2(walkSpeed * 1.5f * direction, rigidB.velocity.y);
                if (player.transform.position.x > transform.position.x)
                {
                    direction = 1;
                }
                if (player.transform.position.x < transform.position.x)
                {

                    direction = -1;
                }
                if (distancePlayer > exitFollowZone)
                {
                    stances = typeStances.passive;
                }
                if (distancePlayer < attackDistance)
                {
                    stances = typeStances.attack;
                }

                break;
            case typeStances.attack:
                rigidB = GetComponent<Rigidbody2D>();

                if (distancePlayer > attackDistance)
                {
                    stances = typeStances.follow;
                }

                checkIfTimeToFire();
                break;

        }
        transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, transform.localScale.z);

    }

    void checkIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bala, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Playerbullet")
        {
            TakeHit();
        }

        if (collision.gameObject.tag == "Explosion")
        {
            TakeHit();
            TakeHit();
            TakeHit();
        }
    }
    public void TakeHit ()
    {
        hitPoints = hitPoints - 1;
        if(hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}