using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper_Controller : MonoBehaviour
{

    public float shootingRange;
    public GameObject bala;
    public float fireRate = 1f;
    public float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    public Transform player;


    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
       
        if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }
}