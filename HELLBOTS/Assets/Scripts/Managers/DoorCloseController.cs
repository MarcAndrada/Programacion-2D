using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseController : MonoBehaviour
{
    
    static BoxCollider2D Door;
    public AudioClip DoorCloseClip;

    private AudioSource audiosource;
    static private SpriteRenderer Sprite;
    static private bool DoorClosed = false;
    void Start()
    {
        Door = GetComponent<BoxCollider2D>();
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


    public static void OpenDoor()
    {
        Sprite.enabled = false;
        Door.enabled = false;
        DoorClosed = false;
    }
}
