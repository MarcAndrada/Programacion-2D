using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeController : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    
    public float granadeSpeed;
    public float TimeToExplode;
    

    private Rigidbody2D rigidB;
    private GameObject Crosshair;
    private GameObject Player;
    private GameObject CurrentExplosion;

    private float GTime;
    
    // Start is called before the first frame update
    void Start()
    {
        Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Player = GameObject.FindGameObjectWithTag("Hellbot");
        rigidB = GetComponent<Rigidbody2D>();
        if (Crosshair.transform.position.x > Player.transform.position.x)
        {
            rigidB.AddForce(Vector3.right * granadeSpeed, ForceMode2D.Impulse);
        }
        else if (Crosshair.transform.position.x < Player.transform.position.x)
        {
            rigidB.AddForce(Vector3.left * granadeSpeed, ForceMode2D.Impulse);
        }


        rigidB.AddTorque(Random.Range(-10, 10), ForceMode2D.Impulse);
    }

    private void Update()
    {
        float delta = Time.deltaTime * 1000;
        GTime += delta;

        if (TimeToExplode <= GTime){
            Explosion();
            GTime = 0;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Explosion();
        }
        if (collision.gameObject.tag == "Shield")
        {
            Explosion();
        }

    }

    public void Explosion(){
        rigidB.velocity = new Vector2(0, 0);
        CurrentExplosion = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Destroy(CurrentExplosion, 1f);
        Destroy(gameObject);
    }


}
