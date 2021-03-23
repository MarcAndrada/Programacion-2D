using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boss_movement : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    public GameObject BossbulletPrefab;
    public GameObject EnemybulletPrefab;
    public bool MoveRight;
    public float hitPoints;
    public GameObject Mouth;
    public GameObject LEye;
    public GameObject REye;
    public AudioClip LaserShoot;
    public AudioClip MisileShoot;
    public float maxBorder;

    private SpriteRenderer sprite;
    private float nextFireTime;
    private GameObject bala1;
    private GameObject bala2;
    private GameObject bala3;
    private GameObject player;
    private bool Lasers = true;
    private float ChangeBulletType = 5000;
    private float TimePasedForChange = 0;
    private Animator animator;
    private AudioSource audiosource;
    private Vector3 CurrentPos;


    enum typeStances { passive, follow, attack }
    typeStances stances = typeStances.passive;
    void Start()
    {
        player = GameObject.FindWithTag("Hellbot");
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        nextFireTime = 0;
        CurrentPos = transform.position;
    }
    void Update()
    {
        float delta = Time.deltaTime * 1000;
        TimePasedForChange+= delta;

        if (ChangeBulletType < TimePasedForChange)
        {
            if (Lasers)
            {
                Lasers = false;
            }
            else
            {
                Lasers = true;
            }

            TimePasedForChange = 0;
        }


    }
    private void FixedUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);


        switch (stances)
        {
            case typeStances.passive:
                
                if (MoveRight)
                {
                    transform.Translate(2 * Time.deltaTime * speed, 0, 0);

                }
                else
                {
                    transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
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
                else
                {
                    stances = typeStances.passive;
                }
                break;

        
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
            sprite.flipX = true;
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            sprite.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
            if (collider.gameObject.tag == "Playerbullet")
            {
                TakeHit();
            }

            if (collider.gameObject.tag == "Explosion")
            {
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
            if (Lasers)
            {
                animator.SetBool("OpenMouth",true);
                audiosource.PlayOneShot(MisileShoot);
                bala1 = Instantiate(BossbulletPrefab, Mouth.transform.position, Quaternion.identity);
                Destroy(bala1, 3);
                nextFireTime = 0;
            }else{
                animator.SetBool("OpenMouth", false);
                audiosource.PlayOneShot(LaserShoot);
                bala2 = Instantiate(EnemybulletPrefab, LEye.transform.position, Quaternion.identity);
                Destroy(bala2, 3);
                audiosource.PlayOneShot(LaserShoot);
                bala3 = Instantiate(EnemybulletPrefab, REye.transform.position, Quaternion.identity);
                Destroy(bala3, 3);
                nextFireTime = 0;

            }
            
        }
    }
    public void TakeHit()
    {
        hitPoints = hitPoints - 1;
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
