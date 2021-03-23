using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaPrivisional : MonoBehaviour
{
    public float speed;
    public AudioClip ScenariHit;
    public AudioClip ShieldHit;
    public AudioClip EnemyHit;

    private GameObject Crosshair;
    private GameObject Player;
    private AudioSource audiosource;
    
    private Vector3 dir;
    // Start is called before the first frame update
    void Start(){
        Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Player = GameObject.FindGameObjectWithTag("Hellbot");
        audiosource = GetComponent<AudioSource>();
        if (Crosshair.transform.position.x > Player.transform.position.x)
        {
            dir = transform.right;
        }
        else
        {
            dir = -transform.right;
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy"){
            audiosource.PlayOneShot(EnemyHit);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 8){
            audiosource.PlayOneShot(ScenariHit);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Shield"){
            audiosource.PlayOneShot(ShieldHit);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        
    }
}
