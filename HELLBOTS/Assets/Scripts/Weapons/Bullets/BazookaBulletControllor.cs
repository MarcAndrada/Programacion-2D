using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBulletControllor : MonoBehaviour{

    public GameObject ExplosionPrefab;

    private GameObject CurrentExplosion;
    private Rigidbody2D rigidB;

// Start is called before the first frame update
void Start()
    {
        rigidB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //Hacer sonidos de Misil Volando
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
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
        Destroy(CurrentExplosion, 1f);
        Destroy(gameObject);
    }

}
