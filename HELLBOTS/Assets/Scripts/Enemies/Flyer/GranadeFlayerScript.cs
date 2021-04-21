using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeFlayerScript : MonoBehaviour
{

    public GameObject ExplosionPrefab;

    private Rigidbody2D rigidB;
    private GameObject CurrentExplosion;
    // Start is called before the first frame update
    void Start()
    {
        rigidB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
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
        rigidB.velocity = new Vector2(0, 0);
        CurrentExplosion = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
