using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLvl3 : MonoBehaviour
{
    public float shootingRange;
    public float nextAttackTime = 1f;
    public float hitPoints;
    public GameObject portal;
    public Slider HP_Bar;
    public Transform EyeTracker;
    public float LaserSpeed;
    

    [Header("UpLaser")]
    public GameObject UpLaser;
    public Vector3 UPDown;
    public Vector3 UPMid;
    [Header("DownLaser")]
    public GameObject DownLaser;
    public Vector3 DOWNUp;
    public Vector3 DOWNMid;
    [Header("RightLaser")]
    public GameObject RightLaser;
    public Vector3 RIGHTLeft;
    public Vector3 RIGHTMid;
    [Header("LeftLaser")]
    public GameObject LeftLaser;
    public Vector3 LEFTRight;
    public Vector3 LEFTMid;
           

    private SpriteRenderer sprite;
    private GameObject player;
    private AudioSource audiosource;
    private bool damaged = false;
    private float TimeSinceDmg;
    private float changeSprite = 150;
    private Color SkullDamagedColor = new Color(0.51f, 0.35f, 0.32f);
    private enum NextAttackPos { NONE, LEFT, RIGHT, MIDDLE, UP, DOWN, LEFTUP, RIGHTUP ,LEFTDOWN, RIGHTDOWN };
    private NextAttackPos CurretAttack = NextAttackPos.NONE;
    private Vector3 StarterUpLaserPos;
    private Vector3 StarterDownLaserPos;
    private Vector3 StarterRightLaserPos;
    private Vector3 StarterLeftLaserPos;
    private float LaserActivatorTime = 1500;
    private float LaserActivatorWaited = 0;



    void Start()
    {
        player = GameObject.FindWithTag("Hellbot");
        sprite = GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
        StarterUpLaserPos = UpLaser.transform.position;
        StarterDownLaserPos = DownLaser.transform.position;
        StarterRightLaserPos = RightLaser.transform.position;
        StarterLeftLaserPos = LeftLaser.transform.position;

        DesactivateLasers();

    }
    void Update()
    {
        float delta = Time.deltaTime * 1000;
        EyeTracker.position = player.transform.position;
        if (damaged)
        {
            TimeSinceDmg += delta;
            sprite.color = SkullDamagedColor;
            if (TimeSinceDmg > changeSprite)
            {
                sprite.color = Color.white;
                damaged = false;
                TimeSinceDmg = 0;
            }


        }


        EyeTracker.position = player.transform.position;
    }


    private void FixedUpdate()
    {
        float delta = Time.deltaTime * 1000;

        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceFromPlayer <= shootingRange)
        {
            LaserActivatorWaited += delta;
            if (LaserActivatorTime <= LaserActivatorWaited)
            {
                ActivateLasers();

            }
            checkIfTimeToFire();

        }
        else
        {
            CurretAttack = NextAttackPos.NONE;
            LaserActivatorWaited = 0;
            ResetLaserPos();

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

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }
    void checkIfTimeToFire()
    {
        float delta = Time.deltaTime * 1000;

        if (UpLaser.transform.position == StarterUpLaserPos &&
            DownLaser.transform.position == StarterDownLaserPos &&
            RightLaser.transform.position == StarterRightLaserPos &&
            LeftLaser.transform.position == StarterLeftLaserPos ){

            int NextAttack = Random.Range(1,10);

            switch (NextAttack)
            {
                case 1:
                    CurretAttack = NextAttackPos.LEFT;
                    break;
                case 2:
                    CurretAttack = NextAttackPos.RIGHT;
                    break;
                case 3:
                    CurretAttack = NextAttackPos.MIDDLE;
                    break;
                case 4:
                    CurretAttack = NextAttackPos.UP;
                    break;
                case 5:
                    CurretAttack = NextAttackPos.DOWN;
                    break;
                case 6:
                    CurretAttack = NextAttackPos.LEFTUP;
                    break;
                case 7:
                    CurretAttack = NextAttackPos.RIGHTUP;
                    break;
                case 8:
                    CurretAttack = NextAttackPos.LEFTDOWN;
                    break;
                case 9:
                    CurretAttack = NextAttackPos.RIGHTDOWN;
                    break;
                default:
                    break;
            }
        }
        if (LaserActivatorTime <= LaserActivatorWaited)
        {

        
            switch (CurretAttack)
            {
                case NextAttackPos.NONE:

                    UpLaser.transform.position = Vector3.MoveTowards(UpLaser.transform.position, StarterUpLaserPos, LaserSpeed);
                    DownLaser.transform.position = Vector3.MoveTowards(DownLaser.transform.position, StarterDownLaserPos, LaserSpeed);
                    RightLaser.transform.position = Vector3.MoveTowards(RightLaser.transform.position, StarterRightLaserPos, LaserSpeed);
                    LeftLaser.transform.position = Vector3.MoveTowards(LeftLaser.transform.position, StarterLeftLaserPos, LaserSpeed);

                    break;
                case NextAttackPos.LEFT:
                    RightLaser.transform.position = Vector3.MoveTowards(RightLaser.transform.position, RIGHTLeft, LaserSpeed);
                    if (RightLaser.transform.position.x == RIGHTLeft.x)
                    {
                       CurretAttack = NextAttackPos.NONE;
                    }
                    break;
                case NextAttackPos.RIGHT:
                    LeftLaser.transform.position = Vector3.MoveTowards(LeftLaser.transform.position, LEFTRight, LaserSpeed);
                    if (LeftLaser.transform.position.x == LEFTRight.x)
                    {
                        CurretAttack = NextAttackPos.NONE;
                    }
                    break;
                case NextAttackPos.MIDDLE:

                    UpLaser.transform.position = Vector3.MoveTowards(UpLaser.transform.position, UPMid, LaserSpeed);

                    DownLaser.transform.position = Vector3.MoveTowards(DownLaser.transform.position, DOWNMid, LaserSpeed);


                    RightLaser.transform.position = Vector3.MoveTowards(RightLaser.transform.position, RIGHTMid, LaserSpeed);

                    LeftLaser.transform.position = Vector3.MoveTowards(LeftLaser.transform.position, LEFTMid, LaserSpeed);
                    
                    
                    if (UpLaser.transform.position.y == UPMid.y && DownLaser.transform.position.y == DOWNMid.y && RightLaser.transform.position.x == RIGHTMid.x && LeftLaser.transform.position.x == LEFTMid.x)
                    {
                        CurretAttack = NextAttackPos.NONE;
                    }

                    break;
                case NextAttackPos.UP:
                    DownLaser.transform.position = Vector3.MoveTowards(DownLaser.transform.position, DOWNUp, LaserSpeed);

                    if (DownLaser.transform.position.y == DOWNUp.y)
                    {
                        CurretAttack = NextAttackPos.NONE;
                    }

                    break;
                case NextAttackPos.DOWN:
                    UpLaser.transform.position = Vector3.MoveTowards(UpLaser.transform.position, UPDown, LaserSpeed);

                    if (UpLaser.transform.position.y == UPDown.y)
                    {
                        CurretAttack = NextAttackPos.NONE;
                    }
                    break;
                case NextAttackPos.LEFTUP:
                    DownLaser.transform.position = Vector3.MoveTowards(DownLaser.transform.position, DOWNUp, LaserSpeed);
                    RightLaser.transform.position = Vector3.MoveTowards(RightLaser.transform.position, RIGHTLeft, LaserSpeed);
                    
                    if (DownLaser.transform.position.y == DOWNUp.y && RightLaser.transform.position.x == RIGHTLeft.x)
                    {
                        CurretAttack = NextAttackPos.NONE;
                    }

                    break;
                case NextAttackPos.RIGHTUP:
                    DownLaser.transform.position = Vector3.MoveTowards(DownLaser.transform.position, DOWNUp, LaserSpeed);
                    LeftLaser.transform.position = Vector3.MoveTowards(LeftLaser.transform.position, LEFTRight, LaserSpeed);
                    
                    if (DownLaser.transform.position.y == DOWNUp.y && LeftLaser.transform.position.x == LEFTRight.x)
                    {
                        CurretAttack = NextAttackPos.NONE;
                    }
                    break;
                case NextAttackPos.LEFTDOWN:
                    UpLaser.transform.position = Vector3.MoveTowards(UpLaser.transform.position, UPDown, LaserSpeed);
                    RightLaser.transform.position = Vector3.MoveTowards(RightLaser.transform.position, RIGHTLeft, LaserSpeed);

                    if (UpLaser.transform.position.y == UPDown.y && RightLaser.transform.position.x == RIGHTLeft.x)
                    {
                        CurretAttack = NextAttackPos.NONE;
                    }

                    break;
                case NextAttackPos.RIGHTDOWN:
                    UpLaser.transform.position = Vector3.MoveTowards(UpLaser.transform.position, UPDown, LaserSpeed);
                    LeftLaser.transform.position = Vector3.MoveTowards(LeftLaser.transform.position, LEFTRight, LaserSpeed);

                    if (UpLaser.transform.position.y == UPDown.y && LeftLaser.transform.position.x == LEFTRight.x)
                    {
                        CurretAttack = NextAttackPos.NONE;
                    }


                    break;
                default:
                    break;
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


    private void ResetLaserPos()
    {
        UpLaser.transform.position = StarterUpLaserPos;
        DownLaser.transform.position = StarterDownLaserPos;
        RightLaser.transform.position = StarterRightLaserPos;
        LeftLaser.transform.position = StarterLeftLaserPos;

    }
    public void ActivateLasers()
    {
        UpLaser.SetActive(true);
        DownLaser.SetActive(true);
        RightLaser.SetActive(true);
        LeftLaser.SetActive(true);
    }

    public void DesactivateLasers()
    {
        UpLaser.SetActive(false);
        DownLaser.SetActive(false);
        RightLaser.SetActive(false);
        LeftLaser.SetActive(false);
    }
}