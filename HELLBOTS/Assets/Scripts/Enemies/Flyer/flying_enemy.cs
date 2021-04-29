using System.Collections;
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
    public float hitPoints;

    private float nextFireTime;
    private GameObject bala;
    private GameObject player;
    private Vector2 CurrentPos;
    private AudioSource audioSource;
    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;
    private bool firstTimeSeen = true;
    private float WaitedTime = 0f;
    private bool damaged = false;
    private float TimeSinceDmg;
    private float changeSprite = 150;
    private float distanceFromPlayer;
    private bool dontMove = false;
    enum typeStances { passive, follow, attack }
    typeStances stances = typeStances.passive;
    void Start()
    {
        player = GameObject.FindWithTag("Hellbot");
        audioSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        nextFireTime = 0;
        CurrentPos = transform.position;

    }

    private void Update()
    {

        float delta = Time.deltaTime * 1000;

        if (damaged)
        {
            TimeSinceDmg += delta;
            sprite.color = Color.red;
            if (TimeSinceDmg > changeSprite)
            {
                sprite.color = Color.white;
                damaged = false;
                TimeSinceDmg = 0;
            }
        }

        distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        switch (stances)
        {
            case typeStances.passive:

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
                break;


            case typeStances.follow:

                if (firstTimeSeen)
                {
                    dontMove = true;
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
                        dontMove = false;
                    }


                }else{
                    
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
                        CurrentPos = transform.position;
                        firstTimeSeen = true;

                    }
                    if (distanceFromPlayer <= shootingRange)
                    {
                        stances = typeStances.attack;
                    }
                }

                break;

            case typeStances.attack:

                checkIfTimeToFire();
                if (player.transform.position.x + 100 > transform.position.x && player.transform.position.x - 100 < transform.position.x)
                {
                    dontMove = true;
                }else if (player.transform.position.x > transform.position.x){
                    dontMove = false;
                    MoveRight = true;
                }
                else if (player.transform.position.x < transform.position.x){
                    dontMove = false;
                    MoveRight = false;
                }

                if (distanceFromPlayer > shootingRange)
                {
                    stances = typeStances.follow;
                    dontMove = false;
                }
                break;

        }



    }


    /* distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
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
                MoveRight = true;
            }
            if (player.transform.position.x < transform.position.x)
            {
                MoveRight = false;
                direction = -1;
            }
            //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        }
        else if (distanceFromPlayer <= shootingRange)
        {
            checkIfTimeToFire();
        }
        else
        {

            if (transform.position.x > CurrentPos.x + maxBorder && MoveRight)
            {
                MoveRight = false;
            }
            if (transform.position.x < CurrentPos.x - maxBorder && !MoveRight)
            {
                MoveRight = true;
                direction = -1;

            }

        }
        */
    

    void FixedUpdate(){

        if (!dontMove)
        {
            if (MoveRight)
            {
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                //transform.Translate(2 * Time.deltaTime * speed, 0, 0);
                //rb2d.AddForce(Vector2.right * speed * 1);
            }
            else
            {
                rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                //transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
                //rb2d.AddForce(Vector2.right * speed * -1);
            }
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }


    }


    void OnTriggerEnter2D(Collider2D collider)
    {
       
        if (collider.gameObject.tag == "Playerbullet")
        {
            TakeHit();
        }

        if (collider.gameObject.tag == "SniperBullet")
        {
            TakeHit();
            TakeHit();
            TakeHit();
        }

        if (collider.gameObject.tag == "Explosion")
        {
            TakeHit();
            TakeHit();
            TakeHit();
            TakeHit();

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

    public void TakeHit()
    {

        damaged = true;
        hitPoints = hitPoints - 1;
        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}