using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_bullet : MonoBehaviour
{

    public AudioClip ScenariHit;
    public float speed;

    private GameObject target;
    private Rigidbody2D bulletRB;
    private float randAngleY;
    private float randAngleX;
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Hellbot");
        randAngleY = Random.Range(-130, 130);
        randAngleX = Random.Range(-130, 130);
        Vector2 moveDir = (new Vector3 (target.transform.position.x + randAngleX, target.transform.position.y + randAngleY, target.transform.position.z) - transform.position).normalized * speed;
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
            if (other.gameObject.tag != "WallFloor")
            {
                Destroy(gameObject);
            }
            
        }
    }

    

    
}
