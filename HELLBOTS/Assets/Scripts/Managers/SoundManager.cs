 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{

    private GameObject Music_GO;
    private GameObject SFX_GO;
    private Slider MusicSlider;
    private Slider SFXSlider;
    private SFX_Controller SFX_Cont;
    private VolumeController Music_Cont;
    private float Music = 0.1f;
    private float SFX = 0.1f;
    private bool FirstTime = true;
    private float WaitedTime;
    private float WaitTime = 10;
    private int TimesWaited = 0;

    // Start is called before the first frame update
    void Start(){
        BinaryReader reader;
        if (File.Exists("sound.sav"))
        {
            reader = new BinaryReader(File.Open("sound.sav", FileMode.Open));
            Music = reader.ReadSingle();
            SFX = reader.ReadSingle();
            reader.Close();
        }
        else { return; }
        Music_GO = GameObject.FindGameObjectWithTag("Music_Slider");
        SFX_GO = GameObject.FindGameObjectWithTag("SFX_Slider");
        Music_Cont = GetComponentInChildren<VolumeController>();
        SFX_Cont = GetComponent<SFX_Controller>();

        if (Music_GO != null)
        {
            MusicSlider = Music_GO.GetComponent<Slider>();
            MusicSlider.value = Music;
        }else{
            Music_Cont.SetVolume(Music);
        }
        if (SFX_GO != null)
        {
            SFXSlider = SFX_GO.GetComponent<Slider>();
            SFXSlider.value = SFX;
        }
        else
        {
            SFX_Cont.SetVolume(SFX);
        }
       


    }

    private void Update(){
        float delta = Time.deltaTime * 1000;
        if (FirstTime && TimesWaited < 10)
        {
            WaitedTime += delta;

            if (WaitedTime > WaitTime)
            {
                Music_GO = GameObject.FindGameObjectWithTag("Music_Slider");
                SFX_GO = GameObject.FindGameObjectWithTag("SFX_Slider");
                Music_Cont = GetComponentInChildren<VolumeController>();
                SFX_Cont = GetComponent<SFX_Controller>();

                if (Music_GO != null)
                {
                    MusicSlider = Music_GO.GetComponent<Slider>();
                    MusicSlider.value = Music;
                }
                else
                {
                    Music_Cont.SetVolume(Music);
                }
                if (SFX_GO != null)
                {
                    SFXSlider = SFX_GO.GetComponent<Slider>();
                    SFXSlider.value = SFX;
                }
                else
                {
                    SFX_Cont.SetVolume(SFX);
                }

                if (Music_GO != null)
                {
                    FirstTime = false;
                }
                WaitedTime = 0;
                WaitTime += 50;
                TimesWaited++;
            }
        }

        if (!FirstTime || TimesWaited >= 10)
        {
            if (Music_GO != null)
            {
                if (MusicSlider != null)
                {

                    Music = MusicSlider.value;
                }
                else
                {
                    Reload();
                }
            }
            if (SFX_GO != null)
            {
                if (SFXSlider != null)
                {
                    SFX = SFXSlider.value;


                }
                else
                {
                    Reload();
                }
                

            }
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


    public void SaveValues()
    {
        BinaryWriter writer = new BinaryWriter(File.Open("sound.sav", FileMode.Create));
        writer.Write(Music);
        writer.Write(SFX);
        writer.Close();
    }

    public void Restart()
    {
        FirstTime = true;
        TimesWaited = 0;
    }

    public bool SliderUpdate()
    {
        if (Music_GO != null && SFX_GO != null )
        {
            return true;
        }
        return false;
    }

    public void Reload()
    {
        Music_GO = GameObject.FindGameObjectWithTag("Music_Slider");
        SFX_GO = GameObject.FindGameObjectWithTag("SFX_Slider");
        Music_Cont = GetComponentInChildren<VolumeController>();
        SFX_Cont = GetComponent<SFX_Controller>();

        if (Music_GO != null)
        {
            MusicSlider = Music_GO.GetComponent<Slider>();
            MusicSlider.value = Music;
        }
        else
        {
            Music_Cont.SetVolume(Music);
        }
        if (SFX_GO != null)
        {
            SFXSlider = SFX_GO.GetComponent<Slider>();
            SFXSlider.value = SFX;
        }
        else
        {
            SFX_Cont.SetVolume(SFX);
        }

        if (Music_GO != null)
        {
            FirstTime = false;
        }
    }
}

