using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_bullet : MonoBehaviour
{

    public AudioClip ScenariHit;
    private GameObject target;
    public float speed;
    private Rigidbody2D bulletRB;
    private AudioSource audiosource;
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Hellbot");
        audiosource = GetComponent<AudioSource>();
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hellbot"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == 8)
        {
            audiosource.PlayOneShot(ScenariHit);
            Destroy(gameObject);
        }
    }

    

    
}
