using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSuicide : MonoBehaviour
{
   
    private suicideController Controller;
    // Start is called before the first frame update
    void Start()
    {

        Controller = GetComponent<suicideController>();
        Controller.enabled = false;
    }

    // Update is called once per frame
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Activator")
        {
            Controller.enabled = true;
        }
    }
}
