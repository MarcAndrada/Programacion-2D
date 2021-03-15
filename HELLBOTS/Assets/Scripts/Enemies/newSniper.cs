using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class Sniper_Controller : MonoBehaviour
{

    public GameObject bala;
    public float fireRate;
    public float nextFire;
    enum typeStances { idle, fire };
    float enterattackDistance = 1000f;

    typeStances stances = typeStances.idle;

    float distancePlayer;
    public Transform player;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        switch (stances)
        {
            case typeStances.idle:
                if (distancePlayer > enterattackDistance)
                {
                    stances = typeStances.fire;
                }
                break;
            case typeStances.fire:
                checkIfTimeToFire();
                if (distancePlayer < enterattackDistance)

                {
                    stances = typeStances.idle;
                }
                break;
        }

    }

    void checkIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bala, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Playerbullet")
        {
            Destroy(gameObject);
        }
    }


}*/