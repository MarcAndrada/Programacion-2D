using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaPrivisional : MonoBehaviour
{
    public float speed;
    public AudioClip ScenariHit;
    public AudioClip ShieldHit;
    public AudioClip EnemyHit;
    public GameObject EnemyParticles;
    public GameObject ShieldParticles;

    private GameObject Crosshair;
    private GameObject Player;
    private AudioSource audiosource;
    private SpriteRenderer Sprite;
    private BoxCollider2D bc2D;
    private GameObject HittedSurface;
    private bool Hit = false;
    private float TimeToWait = 0;
    private float SoundTime = 300;
    
    private Vector3 dir;
    // Start is called before the first frame update
    void Start(){
        Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Player = GameObject.FindGameObjectWithTag("Hellbot");
        audiosource = GetComponent<AudioSource>();
        Sprite = GetComponent<SpriteRenderer>();
        bc2D = GetComponent<BoxCollider2D>();


        if (Crosshair.transform.position.x > Player.transform.position.x)
        {
            dir = transform.right;
        }
        else
        {
            dir = -transform.right;
            Sprite.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;
        transform.position += dir * speed * Time.deltaTime;

        if (HittedSurface != null && Hit && HittedSurface.tag != "WallFloor")
        {
            TimeToWait += delta;
            if (TimeToWait > SoundTime){
                
                Destroy(gameObject);
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && gameObject.tag != "Untagged"){
            Hit = true;
            if (EnemyHit != null)
            {
                audiosource.PlayOneShot(EnemyHit);
            }
            Sprite.enabled = false;
            bc2D.enabled = false;
            Instantiate(EnemyParticles, transform.position, Quaternion.identity);

        }

        if (collision.gameObject.layer == 8){
            Hit = true;
            HittedSurface = collision.gameObject;
            if (collision.gameObject.tag != "WallFloor" && collision.gameObject.tag != "Door" && collision.gameObject.tag != "InverseDoor")
            {
                gameObject.tag = "Untagged";
                Sprite.enabled = false;
                bc2D.enabled = false;
                if (ScenariHit != null)
                {
                    audiosource.PlayOneShot(ScenariHit);
                }
            }

            
            
            

        }

        if (collision.gameObject.tag == "Shield"){
            Hit = true;
            if (ShieldHit != null)
            {
                audiosource.PlayOneShot(ShieldHit);
            }
            speed = 0;
            Instantiate(ShieldParticles, transform.position, Quaternion.identity);
            gameObject.tag = "Untagged";
            Sprite.enabled = false;
            bc2D.enabled = false;
        }

    }
}
