using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private boss_movement ParentContr;
    // Start is called before the first frame update
    void Start()
    {
        ParentContr = GetComponentInParent<boss_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Playerbullet")
        {
            ParentContr.TakeHit();
        }
    }
}
