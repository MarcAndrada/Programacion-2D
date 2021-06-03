using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaBossFinal : MonoBehaviour
{
    Transform player;
    public float speed;
    public GameObject ExplosionPrefab;

    private GameObject CurrentExplosion;
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
        if (collision.gameObject.tag == "Playerbullet")
        {
            Explosion();
        }
        if (collision.gameObject.layer == 8)
        {
            Explosion();
        }
    }
    public void Explosion()
    {
        //Hacer sonido de explosion
        rigidB.velocity = new Vector2(0, 0);
        CurrentExplosion = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
