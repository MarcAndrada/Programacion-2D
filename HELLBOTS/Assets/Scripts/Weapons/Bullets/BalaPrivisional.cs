using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaPrivisional : MonoBehaviour
{
    public float speed;

    private GameObject Crosshair;
    private GameObject Player;
    
    private Vector3 dir;
    // Start is called before the first frame update
    void Start(){
        Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Player = GameObject.FindGameObjectWithTag("Hellbot");
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
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Shield")
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        
    }
}
