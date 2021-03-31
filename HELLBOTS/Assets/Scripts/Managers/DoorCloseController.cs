using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseController : MonoBehaviour
{
    
    public BoxCollider2D Door;
    public AudioClip DoorCloseClip;

    private AudioSource audiosource;
    private SpriteRenderer Sprite;
    private bool DoorClosed = false;
    void Start()
    {
        
        Sprite = GetComponent<SpriteRenderer>();
        Sprite.enabled = false;
        Door.enabled = false;
        audiosource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hellbot"){
            if (!DoorClosed)
            {
                Sprite.enabled = true;
                Door.enabled = true;
                audiosource.PlayOneShot(DoorCloseClip);
                DoorClosed = true;
            }
            
        }
    }
}
