using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class new_movement : MonoBehaviour
{
    public float speed;

    public bool MoveRight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MoveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-1, -1);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("TurnPoint"))
        {
            if (MoveRight)
            {
                MoveRight = false;
            }
            else
            {
                MoveRight = true;
            }

        }
    }
}*/

public class new_movement : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject bala;
    public GameObject bulletParent;
    private GameObject player;
    public bool MoveRight;
    
    private float nextFire;
    enum typeStances { passive, follow, attack}
    typeStances stances = typeStances.passive;
    void Start()
    {
        player = GameObject.FindWithTag("Hellbot");
    }
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (MoveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-1, -1);
        }
        switch (stances)
        {
            case typeStances.passive:
                {
                    if (MoveRight)
                    {
                        transform.Translate(2 * Time.deltaTime * speed, 0, 0);
                        transform.localScale = new Vector2(1, 1);
                    }
                    else
                    {
                        transform.Translate(-2 * Time.deltaTime * speed, 0, 0);

                    }
                    if (distanceFromPlayer < lineOfSite)
                    {
                        stances = typeStances.follow;
                    }
                    break;
                }
            case typeStances.follow:
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                    if (distanceFromPlayer > lineOfSite)
                    {
                        stances = typeStances.passive;
                    }
                    if (distanceFromPlayer <= shootingRange)
                    {
                        stances = typeStances.attack;
                    }
                    break;
                }
            case typeStances.attack:
                {
                    checkIfTimeToFire();
                    if (distanceFromPlayer > shootingRange)
                    {
                        stances = typeStances.follow;
                    }
                    break;
                }
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("TurnPoint"))
        {
            if (MoveRight)
            {
                MoveRight = false;
            }
            else
            {
                MoveRight = true;
            }

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }
    void checkIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bala, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}