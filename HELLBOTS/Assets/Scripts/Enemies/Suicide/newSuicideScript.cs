using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class newSuicideScript : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public bool MoveRight;
    public float speed;
    public float lineOfSite;
    public GameObject alert;
    public AudioClip AlertSound;
    public float WaitTime;
    public float maxBorder;

    private GameObject CurrentExplosion;
    private GameObject player;
    private SpriteRenderer sprite;
    private AudioSource audioSource;
    private bool firstTimeSeen = true;
    private float WaitedTime = 0f;
    private Vector2 CurrentPos;
    private Rigidbody2D rb2d;
    private Animator anim;

    enum typeStances { passive, follow, attack }
    typeStances stances = typeStances.passive;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Hellbot");
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        CurrentPos = transform.position;

    }
    void Update()
    {
        if (player != null)
        {
            if (transform.position.x <= player.transform.position.x + 25 && transform.position.x >= player.transform.position.x - 25)
            {
                Explosion();
            }
        }
        
    }
    private void FixedUpdate()
    {
        float distanceFromPlayer = 9999999999;

        if (player != null)
        {
            distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        }
        float delta = Time.deltaTime * 1000;

        switch (stances)
        {
            case typeStances.passive:

                if (MoveRight)
                {
                    rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                    //transform.Translate(2 * Time.deltaTime * speed, 0, 0);
                    //rb2d.AddForce(Vector2.right * speed * 1);
                    sprite.flipX = true;
                }
                else
                {
                    rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                    //transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
                    //rb2d.AddForce(Vector2.right * speed * -1);
                    sprite.flipX = false;
                }

                if (transform.position.x > CurrentPos.x + maxBorder && MoveRight){
                    MoveRight = false;
                }
                if (transform.position.x < CurrentPos.x - maxBorder && !MoveRight){
                    MoveRight = true;
                }
                
                if (distanceFromPlayer < lineOfSite)
                {
                    stances = typeStances.follow;
                }
                break;


            case typeStances.follow:
                if (firstTimeSeen)
                {
                    //hacer sonido
                    if (WaitedTime == 0)
                    {
                        audioSource.PlayOneShot(AlertSound);
                    }

                    //empezar a contar
                    WaitedTime += delta;

                    //set active alerta
                    alert.SetActive(true);

                    if (WaitedTime > WaitTime)
                    {
                        //cuando el contador este tal poner firsttimeseen a fasle
                        firstTimeSeen = false;
                        //set active false alerta
                        alert.SetActive(false);
                        WaitedTime = 0;
                    }


                }
                else
                {

                    if (player.transform.position.x > transform.position.x)
                    {
                        MoveRight = true;
                    }
                    else if (player.transform.position.x < transform.position.x)
                    {
                        MoveRight = false;
                    }
                    if (MoveRight)
                    {
                        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                        //transform.Translate(2 * Time.deltaTime * speed, 0, 0);
                        //rb2d.AddForce(Vector2.right * speed * 1);
                        sprite.flipX = true;
                    }
                    else
                    {
                        rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                        //transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
                        //rb2d.AddForce(Vector2.right * speed * -1);
                        sprite.flipX = false;
                    }

                    if (distanceFromPlayer > lineOfSite)
                    {
                        stances = typeStances.passive;
                        CurrentPos = transform.position;
                        firstTimeSeen = true;

                    }
                }

                break;

        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Playerbullet"){
            Explosion();
        }
        if (collider.gameObject.tag == "Explosion"){
            gameObject.SetActive(false);
        }
        if (collider.gameObject.tag == "SniperBullet")
        {
            Explosion();
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

      
       
        if (collider.gameObject.layer == 8)
        {
            
            if (MoveRight)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right, 20);
                if (checkRaycastWithScenario(hits)) { MoveRight = false; }
               
            }
            else
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector2.right, 20);
                if (checkRaycastWithScenario(hits)) { MoveRight = true; }
                

            }
        }
       
    }

    private bool checkRaycastWithScenario(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void Explosion()
    {
        //Hacer sonido de explosion
        CurrentExplosion = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}