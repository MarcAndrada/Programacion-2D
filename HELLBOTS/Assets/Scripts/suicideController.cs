using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class suicideController : MonoBehaviour
{
    public enum Direction { NONE, LEFT, RIGHT }
    public float speed;
    public Direction suicideDirection = Direction.NONE;

    private int firstDir;
    private float currentSpeed;
    private Rigidbody2D rigidB;
    private bool Explosion;
    private float destroyTimer;
    private float explosionDuration = 400f;

    // Start is called before the first frame update
    void Start()
    {
        firstDir =  Random.Range(1, 2);
        rigidB = GetComponent<Rigidbody2D>();
        Explosion = false;
        if (firstDir == 1){
            suicideDirection = Direction.LEFT;
        }
        else if (firstDir == 2)
        {
            suicideDirection = Direction.RIGHT;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;
        if (Explosion)
        {
            destroyTimer += delta;
            if (destroyTimer > explosionDuration) {

                gameObject.SetActive(false);
                

            }

        }
    }

    void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
        switch (suicideDirection)
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

        rigidB.velocity = new Vector2(currentSpeed, -rigidB.gravityScale * delta);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Hellbot")
        {
            rigidB.velocity = new Vector2(0, 0);
            speed = 0;
            transform.localScale = new Vector3(3,3,1);
            Explosion = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall"){
            if (suicideDirection == Direction.RIGHT)
            {
                suicideDirection = Direction.LEFT;
            }
            else if (suicideDirection == Direction.LEFT)
            {
                suicideDirection = Direction.RIGHT;
            }
        }

        if (collision.gameObject.tag == "Hellbot")
        {
            collision.gameObject.SetActive(false);
        }
        
    }
}