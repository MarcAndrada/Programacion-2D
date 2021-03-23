using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretScrip : MonoBehaviour
{

    public float shootingRange;
    public float fireRate;
    public GameObject bulletPrefab;
    public GameObject Canon;
    public AudioClip EnemyShoot;

    private float nextFireTime;
    private GameObject player;
    private GameObject bala;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Hellbot");
        audioSource = GetComponent<AudioSource>();
    }


void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceFromPlayer <= shootingRange)
        {
            checkIfTimeToFire();
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Playerbullet")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Explosion")
        {
            Destroy(gameObject);
        }
 
    }

    void checkIfTimeToFire()
    {
        float delta = Time.deltaTime * 1000;
        nextFireTime += delta;
        if (nextFireTime > fireRate)
        {
            audioSource.PlayOneShot(EnemyShoot);
            bala = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Destroy(bala, 4);
            nextFireTime = 0;
        }
    }

}