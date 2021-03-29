using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{

    public Slider MusicSlider;
    public Slider SFXSlider;

    private SFX_Controller SFX_Cont;
    private VolumeController Music_Cont;
    private float Music = 0.1f;
    private float SFX = 0.1f;


    private void Awake() {
        BinaryReader reader;
        if (File.Exists("sound.sav"))
        {
            reader = new BinaryReader(File.Open("save.sav", FileMode.Open));
        }
        else { return;  }

        Music = reader.ReadInt32();
        SFX = reader.ReadInt32();
        reader.Close();


    }

    // Start is called before the first frame update
    void Start(){

        MusicSlider = MusicSlider.GetComponent<Slider>();
        SFXSlider = SFXSlider.GetComponent<Slider>();
        Music_Cont = GetComponent<VolumeController>();
        SFX_Cont = GetComponent<SFX_Controller>();

        if (MusicSlider != null){
            MusicSlider.value = Music;

        }else{
            Music_Cont.SetVolume(Music);
        }

        if (SFXSlider != null)
        {
            SFXSlider.value = SFX;

        }else{
            SFX_Cont.SetVolume(SFX);
        }

    }

    private void Update(){

        if (MusicSlider != null)
        {
            Music = MusicSlider.value; 

        }
        if (SFXSlider != null){
            SFX = SFXSlider.value;

        }
        
    }

    // Update is called once per frame
    public float GetMusicVol()
    {
         return Music;
    }
    public float GetSFXVol()
    {
       return SFX;
    }
}
