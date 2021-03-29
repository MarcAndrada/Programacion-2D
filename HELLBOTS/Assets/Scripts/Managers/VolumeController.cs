using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    private AudioSource audiosouce;
    private float musicVolume;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        audiosouce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audiosouce.volume = musicVolume;
        
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
