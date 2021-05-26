using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolumeController : MonoBehaviour
{
    public AudioClip MainMenu;
    public AudioClip Level1;
    public AudioClip Level2;
    public AudioClip Level3;
    public AudioClip MusicBoss;

    private AudioSource audiosouce;
    private float musicVolume;
    private Scene ActiveScene;
    private SoundManager sound;
    private bool boss = false;
    // Start is called before the first frame update
    void Start()
    {
        audiosouce = GetComponent<AudioSource>();
        sound = GetComponentInParent<SoundManager>();
        musicVolume = sound.GetMusicVol();
    }

    // Update is called once per frame
    void Update()
    {
        ActiveScene = SceneManager.GetActiveScene();

        audiosouce.volume = musicVolume;

        if (!audiosouce.isPlaying)
        {
            audiosouce.clip = null;
        }

        if (boss)
        {
            if (audiosouce.clip != MusicBoss)
            {
                audiosouce.Stop();
                audiosouce.PlayOneShot(MusicBoss);
                audiosouce.clip = MusicBoss;
            }
        }
        else
        {

            if (ActiveScene.name == "Map1" || ActiveScene.name == "Tutorial")
            {

                if (audiosouce.clip != Level1)
                {
                    audiosouce.Stop();
                    audiosouce.PlayOneShot(Level1);
                    audiosouce.clip = Level1;
                }
                /*audiosouce.clip = Level1;
                audiosouce.playOnAwake = Level1;*/

            }
            else if (ActiveScene.name == "Map2")
            {
                if (audiosouce.clip != Level2)
                {
                    audiosouce.Stop();
                    audiosouce.PlayOneShot(Level2);
                    audiosouce.clip = Level2;
                }
                //audiosouce.clip = Level2;
            }
            else if (ActiveScene.name == "Map3")
            {

                if (audiosouce.clip != Level3)
                {
                    audiosouce.Stop();
                    audiosouce.PlayOneShot(Level3);
                    audiosouce.clip = Level3;
                    //audiosouce.clip = Level3;
                }

            }
            else if (ActiveScene.name == "Map4")
            {
                audiosouce.Stop();
            }
            else
            {

                if (audiosouce.clip != MainMenu)
                {
                    audiosouce.Stop();
                    audiosouce.PlayOneShot(MainMenu);
                    audiosouce.clip = MainMenu;
                }

                //audiosouce.clip = MainMenu;
                //audiosouce.playOnAwake = MainMenu;
            }
        }
       

    }
    

        

    

    public void SetVolume(float vol)
    {
        if (sound != null && sound.SliderUpdate())
        {
            musicVolume = vol;
        }
        
    }

    public void ActiveBossMusic()
    {
        boss = true;
    }
    public void DesactiveBossMusic()
    {
        boss = false;
    }

}
