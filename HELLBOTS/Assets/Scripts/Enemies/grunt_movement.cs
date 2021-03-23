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

    private float nextFireTime;
    private GameObject bala;
    private GameObject player;
    private Vector2 CurrentPos;
    private SpriteRenderer sprite;
    private AudioSource audioSource;
    private bool firstTimeSeen = true;
    private float WaitedTime = 0f;
   
    private enum typeStances { passive, follow, attack }
    private typeStances stances = typeStances.passive;

    Animator anim;


    void Start() {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Hellbot");
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        nextFireTime = 0;
        CurrentPos = transform.position;
        
    }

    private void FixedUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        float delta = Time.deltaTime * 1000;



        switch (stances){
            case typeStances.passive:
                if (MoveRight)
                {
                    transform.Translate(2 * Time.deltaTime * speed, 0, 0);
                    sprite.flipX = true;
                }
                else{
                    transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
                    sprite.flipX = false;
                }

                if (transform.position.x > CurrentPos.x + maxBorder && MoveRight){
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
                        transform.Translate(2 * Time.deltaTime * speed, 0, 0);
                        sprite.flipX = true;
                    }
                    else
                    {
                        transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
                        sprite.flipX = false;
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
                
                checkIfTimeToFire();
                if (distanceFromPlayer > shootingRange)
                {
                    stances = typeStances.follow;
                }
                break;
                


        }
        if (MoveRight = true)
        {
            anim.SetTrigger("caminar");
        }
        if (MoveRight = false)
        {
            anim.SetTrigger("caminar");
        }

        refManoArma.position = player.transform.position;
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
            audioSource.PlayOneShot(EnemyShoot);
            bala = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Destroy(bala, 4);
            nextFireTime = 0;
        }
    }
} 