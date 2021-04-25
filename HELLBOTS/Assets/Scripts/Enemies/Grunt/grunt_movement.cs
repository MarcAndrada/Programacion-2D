using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class grunt_movement : MonoBehaviour
{

    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public bool MoveRight;
    public GameObject alert;
    public AudioClip AlertSound;
    public float WaitTime;
    public float maxBorder;
    public AudioClip EnemyShoot;
    public Transform refManoArma;
    public GameObject LimbSolverMano;
    public float hitPoints;

    private float nextFireTime;
    private GameObject bala;
    private GameObject player;
    private Vector2 CurrentPos;
    private SpriteRenderer sprite;
    private AudioSource audioSource;
    private bool firstTimeSeen = true;
    private float WaitedTime = 0f;
    private bool damaged = false;
    private float TimeSinceDmg;
    private float changeSprite = 150;
    private Rigidbody2D rb2d;
    

    private enum typeStances { passive, follow, attack }
    private typeStances stances = typeStances.passive;

    Animator anim;


    void Start() {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Hellbot");
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
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

        if (stances == typeStances.follow || stances == typeStances.attack)
        {
            LimbSolverMano.transform.position = player.transform.position;
        }
        

    }
    private void FixedUpdate()
    {
        float distanceFromPlayer = 100000;
        if (player != null)
        {
        distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        }

        float delta = Time.deltaTime * 1000;
        nextFireTime += delta;

        switch (stances){
            case typeStances.passive:
                anim.SetBool("Walking", true);
                if (MoveRight)
                {
                    rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                    //transform.Translate(2 * Time.deltaTime * speed, 0, 0);
                    //rb2d.AddForce(Vector2.right * speed * 1);
                    if (transform.localScale.x > 0)
                    {
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    }
                }
                else{
                    rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                    //transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
                    //rb2d.AddForce(Vector2.right * speed * -1);
                    if (transform.localScale.x < 0)
                    {
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    }
                }

                if (transform.position.x > CurrentPos.x + maxBorder && MoveRight)
                {
                    MoveRight = false;
                }
                if (transform.position.x < CurrentPos.x - maxBorder && !MoveRight)
                {
                    MoveRight = true;
                }

                if (distanceFromPlayer < lineOfSite){
                    stances = typeStances.follow;
                }
                break;

                
            case typeStances.follow:
                anim.SetBool("Walking", true);
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


                }else{

                    if (player.transform.position.x > transform.position.x)
                    {
                        MoveRight = true;
                    }
                    else if (player.transform.position.x < transform.position.x)
                    {
                        MoveRight = false;
                    }
                    if (MoveRight)
                    {
                        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                        //transform.Translate(2 * Time.deltaTime * speed, 0, 0);
                        //rb2d.AddForce(Vector2.right * speed * 1);
                        if (transform.localScale.x > 0)
                        {
                            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                        }
                    }
                    else
                    {
                        rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                        //transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
                        //rb2d.AddForce(Vector2.right * speed * -1);
                        if (transform.localScale.x < 0)
                        {
                            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                        }

                    }

                    if (distanceFromPlayer > lineOfSite)
                    {
                        stances = typeStances.passive;
                        firstTimeSeen = true;
                        CurrentPos = transform.position;
                    }
                    if (distanceFromPlayer <= shootingRange)
                    {
                        stances = typeStances.attack;
                    }
                }
                break;
                
            case typeStances.attack:
                anim.SetBool("Walking", false);
                checkIfTimeToFire();

                if (transform.localScale.x < 0 && player.transform.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                }
                else if (transform.localScale.x > 0 && player.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                if (distanceFromPlayer > shootingRange)
                {
                    stances = typeStances.follow;
                }
                break;
                


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


        if (nextFireTime > fireRate)
        {
            audioSource.PlayOneShot(EnemyShoot);
            bala = Instantiate(bulletPrefab, refManoArma.transform.position, Quaternion.identity);
            Destroy(bala, 4);
            nextFireTime = 0;
        }
    }

    public void TakeHit()
    {
        hitPoints = hitPoints - 1;
        damaged = true;

        if (hitPoints <= 0)
        { 
            gameObject.SetActive(false);
        }
    }

} 