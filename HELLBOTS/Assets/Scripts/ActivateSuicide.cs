using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSuicide : MonoBehaviour
{
   
    private SpriteRenderer sprite;
    private suicideController Controller;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        Controller = GetComponent<suicideController>();
        Controller.enabled = false;
    }

    // Update is called once per frame
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            sprite.enabled = true;
            Controller.enabled = true;
        }
    }
}
