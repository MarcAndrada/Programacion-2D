using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseController : MonoBehaviour
{
    
    static BoxCollider2D PublicDoor;
    private BoxCollider2D Door;
    public AudioClip DoorCloseClip;
    private AudioSource audiosource;
    static private SpriteRenderer SpritePublic;
    private SpriteRenderer CurrentSprite;
    static private bool DoorClosed;
    void Start()
    {
        PublicDoor = GetComponent<BoxCollider2D>();
        Door = GetComponent<BoxCollider2D>();
        SpritePublic = GetComponent<SpriteRenderer>();
        CurrentSprite = GetComponent<SpriteRenderer>();

        if (gameObject.tag == "Door")
        {
            CurrentSprite.enabled = false;
            Door.enabled = false;
            DoorClosed = false;
        }
        else if (gameObject.tag == "InverseDoor")
        {
            CurrentSprite.enabled = true;
            Door.enabled = true;
            DoorClosed = true;
        }
        
        audiosource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hellbot"){
            if (gameObject.name == "Inverse Door")
            {
                audiosource.PlayOneShot(DoorCloseClip);
                CurrentSprite.enabled = false;
                Door.enabled = false;
                DoorClosed = false;

                // gameObject.SetActive(false);

            } else if (!DoorClosed && gameObject.tag == "Door"){
                CurrentSprite.enabled = true;
                Door.enabled = true;
                audiosource.PlayOneShot(DoorCloseClip);
                DoorClosed = true;
            }

        }
    }


    public static void OpenDoor()
    {
        SpritePublic.enabled = false;
        PublicDoor.enabled = false;
        DoorClosed = false;

    }
}
