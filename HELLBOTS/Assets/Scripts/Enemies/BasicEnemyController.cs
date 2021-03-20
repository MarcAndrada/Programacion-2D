using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    public GameObject bala;
    public float fireRate;
    public float walkSpeed = 40f;
    public GameObject alert;
    public AudioClip AlertSound;
    public float WaitTime;

    
    private float nextFire;
    private float limiteWalkLeft;
    private float limiteWalkRight;
    private int direction = 1;
    private bool firstTimeSeen = true;
    private float WaitedTime = 0f;
    private AudioSource audioSource;
    //public GameObject castPoint;
    enum typeStances { passive, follow, attack }

    typeStances stances = typeStances.passive;

    public float enterFollowZone = 1345f;
    public float exitFollowZone = 1500f;
    public float attackDistance = 1000f;

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
        audioSource = GetComponent<AudioSource>();

    }





    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;

        distancePlayer = Mathf.Abs(player.transform.position.x - transform.position.x);
        switch (stances)
        {
            case typeStances.passive:


                
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

                if (!firstTimeSeen) {
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
                        firstTimeSeen = true;
                    }
                    if (distancePlayer < attackDistance)
                    {
                        stances = typeStances.attack;
                    }
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
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Explosion")
        {
            Destroy(gameObject);
        }
    }
}