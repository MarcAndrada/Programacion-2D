using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    public enum Direction { NONE, LEFT, RIGHT }
    public float speed;
    public Direction basicEnemyDirection = Direction.NONE;
    public GameObject bala;
    public float fireRate;
    public float nextFire;
    public float limiteWalkLeft;
    public float limiteWalkRight;
    public float walkSpeed = 40f;
    int direction = 1;

    private int firstDir;
    private float currentSpeed;
    private Rigidbody2D rigidB;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1f;
        nextFire = Time.time;
        //firstDir = Random.Range(1, 2)
        /*if (firstDir == 1)
        {
            basicEnemyDirection = Direction.LEFT;
        }
        else if (firstDir == 2)
        {
            basicEnemyDirection = Direction.RIGHT;
        }*/
        //Los límites para la patrulla
        rigidB = GetComponent<Rigidbody2D>();
        limiteWalkLeft = transform.position.x - GetComponent<CircleCollider2D>().radius;
        limiteWalkRight = transform.position.x + GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame


    void FixedUpdate()
    {
        /*float delta = Time.fixedDeltaTime * 1000;
        switch (basicEnemyDirection)
        {
            default:
                break;
            case Direction.LEFT:
                currentSpeed = -speed;

                break;
            case Direction.RIGHT:
                currentSpeed = speed;

                break;

        }

        rigidB.velocity = new Vector2(currentSpeed, -rigidB.gravityScale * delta);*/
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.tag == "Wall")
        {
            if (basicEnemyDirection == Direction.RIGHT)
            {
                basicEnemyDirection = Direction.LEFT;
            }
            else if (basicEnemyDirection == Direction.LEFT)
            {
                basicEnemyDirection = Direction.RIGHT;
            }
        }*/

        /*if (collision.gameObject.tag == "Hellbot")
        {
            collision.gameObject.SetActive(false);
        }*/
       
    }
   

    // Update is called once per frame
    void Update()
    {
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
        transform.localScale = new Vector3(0.46f * direction, 0.46f, 0.46f);
       
        checkIfTimeToFire();
    
    }

    void checkIfTimeToFire()
    {
        /*if (Time.time > nextFire)
        {
            Instantiate(bala, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Playerbullet")
        {
            Destroy(gameObject);
        }
    }
}