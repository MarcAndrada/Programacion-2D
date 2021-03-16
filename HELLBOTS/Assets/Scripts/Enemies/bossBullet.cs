using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    GameObject target;
    public float speed;

    private GameObject CurrentExplosion;

    private Rigidbody2D rigidB;

    void Start()
    {
        rigidB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Hellbot");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        rigidB.velocity = new Vector2(moveDir.x, moveDir.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hellbot")
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
        Destroy(CurrentExplosion, 0.2f);
        Destroy(gameObject);
    }

    
}
