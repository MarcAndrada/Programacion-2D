using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss_movement : MonoBehaviour
{
    public float HandSpeed;
    public float shootingRange;
    public float fireRate = 1f;
    public GameObject BossbulletPrefab;
    public GameObject EnemybulletPrefab;
    public float hitPoints;
    public GameObject LEye;
    public GameObject REye;
    public GameObject RHand;
    public GameObject LHand;
    public AudioClip LaserShoot;
    public GameObject portal;
    public AudioClip laught;
    public Slider HP_Bar;

    private SpriteRenderer sprite;
    private SpriteRenderer LHandSprite;
    private SpriteRenderer RHandSprite;
    private Rigidbody2D LHandRB2D;
    private Rigidbody2D RHandRB2D;
    private GameObject bala1;
    private GameObject bala2;
    private float nextFireTime;
    private GameObject player;
    private bool Lasers = true;
    private AudioSource audiosource;
    private bool damaged = false;
    private float TimeSinceDmg;
    private float changeSprite = 150;
    private float HandOnTheFloorTime = 1500;
    private float TimePassedHandOnFloor;
    private float HandAbovePlayer = 2000;
    private float TimeAbovePlayer;
    private bool HitFloor;
    private float LaughtTime = 5000;
    private float LaughtTimePassed;

    void Start()
    {
        player = GameObject.FindWithTag("Hellbot");
        sprite = GetComponent<SpriteRenderer>();
        LHandSprite = LHand.GetComponent<SpriteRenderer>();
        RHandSprite = RHand.GetComponent<SpriteRenderer>();
        LHandRB2D = LHand.GetComponent<Rigidbody2D>();
        RHandRB2D = RHand.GetComponent<Rigidbody2D>();
        audiosource = GetComponent<AudioSource>();
        nextFireTime = 0;

    }
    void Update()
    {
        float delta = Time.deltaTime * 1000;
        LaughtTimePassed += delta;

        if (LaughtTimePassed >= LaughtTime)
        {
            audiosource.PlayOneShot(laught);
            LaughtTimePassed = 0;
        }
        if (player.transform.position.y < transform.position.y)
        {
            Lasers = false;
                

        }
        else
        {
            Lasers = true;
        }



        if (damaged)
        {
            TimeSinceDmg += delta;
            sprite.color = Color.red;
            LHandSprite.color = Color.red;
            RHandSprite.color = Color.red;
            if (TimeSinceDmg > changeSprite)
            {
                sprite.color = Color.white;
                LHandSprite.color = Color.white;
                RHandSprite.color = Color.white;
                damaged = false;
                TimeSinceDmg = 0;
            }




        }


    }


    private void FixedUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceFromPlayer <= shootingRange)
        {
            checkIfTimeToFire();
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

        if (collider.gameObject.layer == 8)
        {
            if (collider.gameObject.tag != "WallFloor")
            {
                HitFloor = false;
            }

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (collision.gameObject.tag != "WallFloor")
            {
                HitFloor = false;
            }

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }
    void checkIfTimeToFire()
    {

        float delta = Time.deltaTime * 1000;
        
        if (Lasers)
        {
            nextFireTime += delta;
            if (nextFireTime > fireRate)
            {
                audiosource.PlayOneShot(LaserShoot);
                bala1 = Instantiate(EnemybulletPrefab, LEye.transform.position, Quaternion.identity);
                Destroy(bala1, 5);

                audiosource.PlayOneShot(LaserShoot);
                bala2 = Instantiate(EnemybulletPrefab, REye.transform.position, Quaternion.identity);
                Destroy(bala2, 5);

                nextFireTime = 0;
            }
        }
        else{
            if (player.transform.position.x > transform.position.x)
            {
                TimePassedHandOnFloor += delta;
                if (HandOnTheFloorTime < TimePassedHandOnFloor)
                {
                    TimeAbovePlayer += delta;
                    if (player.transform.position.y + 400 >= RHand.transform.position.y && player.transform.position.y + 400 >= RHand.transform.position.y && TimeAbovePlayer < HandAbovePlayer - 500)
                    {
                        RHand.transform.position = Vector3.MoveTowards(RHand.transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 400, transform.position.z), HandSpeed * Time.deltaTime);
                        LHand.transform.position = Vector3.MoveTowards(LHand.transform.position, new Vector3(LHand.transform.position.x, player.transform.position.y + 400, transform.position.z), HandSpeed * Time.deltaTime);
                    }
                    if (TimeAbovePlayer > HandAbovePlayer - 500)
                    {
                        LHandRB2D.velocity = new Vector2(0,200);
                        RHandRB2D.velocity = new Vector2(0,200);
                    }
                    if (TimeAbovePlayer > HandAbovePlayer)
                    {
                        LHandRB2D.velocity = new Vector2(0, 0);
                        RHandRB2D.velocity = new Vector2(0, 0);
                        HitFloor = true;
                    }
                }

                if (HitFloor)
                {
                    TimePassedHandOnFloor = 0;
                    TimeAbovePlayer = 0;
                    
                }

            }
            else
            {
                TimePassedHandOnFloor += delta;
                if (HandOnTheFloorTime < TimePassedHandOnFloor)
                {
                    TimeAbovePlayer += delta;
                    if (player.transform.position.y + 400 >= RHand.transform.position.y && player.transform.position.y + 400 >= RHand.transform.position.y && TimeAbovePlayer < HandAbovePlayer - 500)
                    {
                        RHand.transform.position = Vector3.MoveTowards(RHand.transform.position, new Vector3(RHand.transform.position.x, player.transform.position.y + 400, transform.position.z), HandSpeed * Time.deltaTime);
                        LHand.transform.position = Vector3.MoveTowards(LHand.transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 400, transform.position.z), HandSpeed * Time.deltaTime);
                    }
                   
                    if (TimeAbovePlayer > HandAbovePlayer - 500)
                    {
                        LHandRB2D.velocity = new Vector2(0, 200);
                        RHandRB2D.velocity = new Vector2(0, 200);
                    }
                    if (TimeAbovePlayer > HandAbovePlayer)
                    {
                        LHandRB2D.velocity = new Vector2(0, 0);
                        RHandRB2D.velocity = new Vector2(0, 0);
                        HitFloor = true;
                    }
                }

            }


            if (HitFloor)
            {
                TimePassedHandOnFloor = 0;
                TimeAbovePlayer = 0;
                LHand.transform.position = Vector3.MoveTowards(LHand.transform.position, new Vector3(LHand.transform.position.x, player.transform.position.y - 100, transform.position.z), HandSpeed * Time.deltaTime);
                RHand.transform.position = Vector3.MoveTowards(RHand.transform.position, new Vector3(RHand.transform.position.x, player.transform.position.y - 100, transform.position.z), HandSpeed * Time.deltaTime);
                if (RHand.transform.position.y <= player.transform.position.y - 100 && LHand.transform.position.y <= player.transform.position.y - 100)
                {
                    HitFloor = false;
                }
              
            }

        }
            
        
    }
    public void TakeHit()
    {
        hitPoints = hitPoints - 1;
        damaged = true;
        SetSliderValue();
        if (hitPoints <= 0)
        {
            Instantiate(portal, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void SetSliderValue()
    {
        HP_Bar.value = hitPoints;
    }

}
