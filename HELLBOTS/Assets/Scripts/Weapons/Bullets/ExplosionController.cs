using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

    private BoxCollider2D BC2D;
    private float TimePassed;
    private float ExplosionTime = 100;
    private float TimeDespawn = 4000;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start(){
        BC2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float Delta = Time.deltaTime * 1000;
        TimePassed += Delta;

        if (TimePassed > ExplosionTime)
        {
            audioSource.enabled = true;
            BC2D.enabled = false;
        }


        if (TimePassed > TimeDespawn)
        {
            Destroy(gameObject);
        }


    }
}
