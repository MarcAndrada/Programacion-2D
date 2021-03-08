using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper_Controller : MonoBehaviour {

    public GameObject bala;
    public float fireRate;
    public float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        checkIfTimeToFire();
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
