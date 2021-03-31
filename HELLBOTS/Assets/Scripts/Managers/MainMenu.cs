using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private SoundManager sound;


    private void Start()
    {
        sound = GetComponent<SoundManager>();
    }
    public void goGame(){
        SceneManager.LoadScene("Tutorial");
    }

    public void goOptions()
    {
        SceneManager.LoadScene("OptionMenu");
    }

    public void goMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void ExitGame()
    {

        float music;
        float SFX;

        music = sound.GetMusicVol();
        SFX = sound.GetSFXVol();

        BinaryWriter writer = new BinaryWriter(File.Open("sound.sav", FileMode.Create));
        writer.Write(music);
        writer.Write(SFX);

        Application.Quit();
    }


}
