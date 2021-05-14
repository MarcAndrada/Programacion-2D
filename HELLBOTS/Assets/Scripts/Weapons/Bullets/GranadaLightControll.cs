using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadaLightControll : MonoBehaviour
{


    private float TimeToWait = 50;
    private float TimeWaited = 0;
    private bool GetBigger;
    private Vector3 MaxScale = new Vector3(1,1,1);
    private Vector3 MinScale = new Vector3(0,0,0);
    private float sizeScale = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;

        TimeWaited += delta;

        if (TimeWaited >= TimeToWait)
        {
            if (GetBigger)
            {
                transform.localScale = new Vector3(transform.localScale.x + sizeScale, transform.localScale.y + sizeScale, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x - sizeScale, transform.localScale.y - sizeScale, transform.localScale.z);
            }

            TimeWaited = 0;
        }


        if (transform.localScale.x <= MinScale.x && transform.localScale.y <= MinScale.y)
        {
            GetBigger = true;
        }
        else if(transform.localScale.x >= MaxScale.x && transform.localScale.y >= MaxScale.y)
        {
            GetBigger = false;

        }

    }
}
