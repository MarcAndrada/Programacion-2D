using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala_Controll : MonoBehaviour
{
    public float balaSpeed;
    private Transform hellbot;
    private Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        hellbot = GameObject.FindGameObjectWithTag("Hellbot").transform;
        target = new Vector2(hellbot.position.x, hellbot.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, balaSpeed);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyBala();
        }
    }

    void OntriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hellbot"))
        {
            DestroyBala();
        }
    }

    void DestroyBala()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "WallFloor")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Roof")
        {
            Destroy(gameObject);
        }
    }
}
