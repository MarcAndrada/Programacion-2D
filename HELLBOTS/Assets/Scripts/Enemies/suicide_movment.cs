using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class suicide_movement : MonoBehaviour
{
    private Rigidbody2D rigidB;
    public GameObject ExplosionPrefab;
    private GameObject CurrentExplosion;

    public float speed;
    public float lineOfSite;
    
    private GameObject player;
    public bool MoveRight;
    private Vector2 StarterPos;

    enum typeStances { passive, follow, attack }
    typeStances stances = typeStances.passive;
    void Start()
    {
        player = GameObject.FindWithTag("Hellbot");
        
        StarterPos = transform.position;
    }
    void Update()
    {
        float delta = Time.deltaTime * 1000;

    }
    private void FixedUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);


        switch (stances)
        {
            case typeStances.passive:
                {
                    if (distanceFromPlayer < lineOfSite)
                    {
                        stances = typeStances.follow;
                    }
                    break;
                }
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
                        transform.position = StarterPos;
                    }
                    
                    break;
                }
            


        }

        if (MoveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-1, -1);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("TurnPoint"))
        {
            if (stances == typeStances.passive)
            {
                if (MoveRight)
                {
                    MoveRight = false;
                }
                else
                {
                    MoveRight = true;
                }
            }
            if (collider.gameObject.tag == "Playerbullet")
            {
                Explosion();
            }
            if (collider.gameObject.tag == "Explosion")
            {
                Explosion();
            }


        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        

    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Hellbot")
        {
            Explosion();
        }


    }

    
    public void Explosion()
    {
        //Hacer sonido de explosion
        rigidB.velocity = new Vector2(0, 0);
        CurrentExplosion = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Destroy(CurrentExplosion, 0.2f);
        Destroy(gameObject);
    }
}
