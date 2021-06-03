using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FinalBossController : MonoBehaviour
{

    public float idleMoveSpeed;
    public float attackMoveSpeed;
    public float attackPlayerSpeed;
    public float groundCheckRadius;
    public float shootingRange;
    public float fireRate = 1f;
    public float hitPoints;

    private float nextFireTime;

    public GameObject projectile;
    public GameObject projectileParent;
    public GameObject projectileParent2;
    public GameObject projectileParent3;
    public GameObject portal;


    public Slider HP_Bar;


    public Vector2 idleMoveDirection;
    public Vector2 attackMoveDirection;

    private Vector2 playerPosition;

    public Transform player;
    public Transform groundCheckUp;
    public Transform groundCheckDown;
    public Transform checkWall;

    public LayerMask groundLayer;

    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingWall;
    private bool goingUp = true;
    private bool facingLeft = true;
    private bool damaged = false;

    private float changeSprite = 150;
    private float TimeSinceDmg;

    private Color SkullDamagedColor = new Color(0.51f, 0.35f, 0.32f);
    private Color SkullColor = new Color(0.75f, 0.75f, 0.75f);

    private SpriteRenderer sprite;



    private Rigidbody2D enemyRB;

    void Start()
    {
        
        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();
        enemyRB = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        float delta = Time.deltaTime * 1000;
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(checkWall.position, groundCheckRadius, groundLayer);
        //IdleState();
        AttackUpDown();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackPlayer();
        }
        //FlipTowardsPlayer();
        /*if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(projectile, projectileParent.transform.position, Quaternion.identity);
            Instantiate(projectile, projectileParent2.transform.position, Quaternion.identity);
            Instantiate(projectile, projectileParent3.transform.position, Quaternion.identity);

            nextFireTime = Time.time + fireRate;
        }*/
        if (damaged)
        {
            TimeSinceDmg += delta;
            sprite.color = SkullDamagedColor;
            if (TimeSinceDmg > changeSprite)
            {
                sprite.color = SkullColor;
                damaged = false;
                TimeSinceDmg = 0;
            }

        }
    }

    public void IdleState()
    {
        if (isTouchingUp && goingUp)
        {
            ChangeDirection();
        }else if (isTouchingDown && !goingUp) {
            ChangeDirection();
        }
        
        if (isTouchingWall)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

            if (facingLeft)
            {
                Flip();
                
            }
            else if (!facingLeft)
            {
                Flip();
                
            }
        }
        
        enemyRB.velocity = idleMoveSpeed * idleMoveDirection;
    }

    public void AttackUpDown()
    {
        if (isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }

        if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
                Shoot();
            }
            else if (!facingLeft)
            {
                Flip();
                Shoot();
            }
        }

        enemyRB.velocity = attackMoveSpeed * attackMoveDirection;
    }

    public void AttackPlayer()
    {
        playerPosition = player.position - transform.position;

        playerPosition.Normalize();

        enemyRB.velocity = playerPosition * attackPlayerSpeed;
    }

    private void FlipTowardsPlayer()
    {
        float playerDirection = player.position.x - transform.position.x;

        if (playerDirection > 0 && facingLeft)
        {
            Flip();
            
        }
        else if (playerDirection < 0 && !facingLeft)
        {
            Flip();
        }
    }

    private void ChangeDirection()
    {
        goingUp = !goingUp;
        idleMoveDirection.y *= -1;
        attackMoveDirection.y *= -1;
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        idleMoveDirection.x *= -1;
        attackMoveDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheckUp.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckDown.position, groundCheckRadius);
        Gizmos.DrawWireSphere(checkWall.position, groundCheckRadius);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
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

    public void Shoot()
    {
        Instantiate(projectile, projectileParent.transform.position, Quaternion.identity);
        Instantiate(projectile, projectileParent2.transform.position, Quaternion.identity);
        Instantiate(projectile, projectileParent3.transform.position, Quaternion.identity);
        nextFireTime = Time.time + fireRate;


    }

    
}
