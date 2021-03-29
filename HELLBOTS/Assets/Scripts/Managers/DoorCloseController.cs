using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseController : MonoBehaviour
{
    private SpriteRenderer Sprite;
    public BoxCollider2D Door;
    void Start()
    {
        
        Sprite = GetComponent<SpriteRenderer>();
        Sprite.enabled = false;
        Door.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hellbot"){
            Sprite.enabled = true;
            Door.enabled = true;
        }
    }
}
