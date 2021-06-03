using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaBossFinal : MonoBehaviour
{
    Transform player;
    public float speed;

    private Rigidbody2D rigidB;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Hellbot").transform;
        rigidB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        
        if (collision.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
            
        }
        if (collision.gameObject.tag == "Hellbot")
        {
            Destroy(this.gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Playerbullet" || collision.gameObject.tag == "SniperBullet")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

        }

    }
    
}
