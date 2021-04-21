using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{

    public Sprite SpriteEncendido;
    public AudioClip ChargeSound;
    public AudioClip EndSound;
    
    private SpriteRenderer sprite;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "Hellbot")
        {
            audioSource.PlayOneShot(ChargeSound);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Hellbot")
        {
            sprite.sprite = SpriteEncendido;
            audioSource.Stop();
            audioSource.PlayOneShot(EndSound);
        }
    }
}
