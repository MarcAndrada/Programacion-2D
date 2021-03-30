using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tank_controller : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public bool MoveRight;
    public float hitPoints;
    public GameObject alert;
    public AudioClip AlertSound;
    public float WaitTime;
    public GameObject shoot;
    public AudioClip EnemyShoot;
    public float maxBorder;
    public GameObject Parent;

    private GameObject bala;
    private GameObject player;
    private Vector2 CurrentPos;
    private AudioSource audioSource;
    private float nextFireTime;
    private bool firstTimeSeen = true;
    private float WaitedTime = 0f;


    enum typeStances { passive, follow, attack }
    typeStances stances = typeStances.passive;
    void Start()
    {
        player = GameObject.FindWithTag("Hellbot");
        audioSource = GetComponent<AudioSource>();
        nextFireTime = 0;
        CurrentPos = transform.position;


    }

    private void FixedUpdate()
    {
       

        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        float delta = Time.deltaTime * 1000;
        
        if (hitPoints <= 0)
        {
            Parent.SetActive(false);
        }

        switch (stances)
        {
            case typeStances.passive:

                if (MoveRight)
                {
                    Parent.transform.Translate(2 * Time.deltaTime * speed, 0, 0);
                    
                }
                else
                {
                    Parent.transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
                }

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
                else
                {

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
                        Parent.transform.Translate(2 * Time.deltaTime * speed, 0, 0);
                    }
                    else
                    {
                        Parent.transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
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

        

    }

    void OnTriggerEnter2D(Collider2D collider){
       
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
            audioSource.PlayOneShot(EnemyShoot);
            bala = Instantiate(bulletPrefab, shoot.transform.position, Quaternion.identity);
            Destroy(bala, 2);
            nextFireTime = 0;
        }
    }
    public void TakeHit()
    {
        hitPoints--;
        //hacer sonidito
    }


}
