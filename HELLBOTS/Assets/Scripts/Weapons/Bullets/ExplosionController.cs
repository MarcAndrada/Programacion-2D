using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

    private BoxCollider2D BC2D;
    private float TimePassed;
    private float ExplosionTime = 100;
    // Start is called before the first frame update
    void Start(){
        BC2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float Delta = Time.deltaTime * 1000;
        TimePassed += Delta;

        if (TimePassed > ExplosionTime)
        {
            BC2D.enabled = false;
        }

    }
}
