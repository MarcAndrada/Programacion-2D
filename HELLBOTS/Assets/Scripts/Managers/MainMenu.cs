using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameObject AudioManagers;
    private SoundManager soundCont;
    private VolumeController MusicCont;
    private SFX_Controller SFXCont;

    private void Start()
    {
        AudioManagers = GameObject.Find("Managers");
        soundCont = AudioManagers.GetComponentInChildren<SoundManager>();
        MusicCont = AudioManagers.GetComponentInChildren<VolumeController>();
        SFXCont = AudioManagers.GetComponentInChildren<SFX_Controller>();

    }

    public void goLevelManager()
    {
        SceneManager.LoadScene("LevelManager");
        soundCont.Restart();
    }
    public void goTutorial(){

        SceneManager.LoadScene("Tutorial");
        soundCont.Restart();
    }

    public void goMap1()
    {

        SceneManager.LoadScene("Map1");
        soundCont.Restart();
    }

    public void goMap2()
    {

        SceneManager.LoadScene("Map2");
        soundCont.Restart();
    }

    public void goMap3()
    {

        SceneManager.LoadScene("Map3");
        soundCont.Restart();
    }

    public void goOptions()
    {
        SceneManager.LoadScene("OptionMenu");
        soundCont.Restart();
    }

    public void goMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        soundCont.Restart();
    }

    public void goCredits()
    {
        SceneManager.LoadScene("WinScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SaveValues()
    {
        soundCont.SaveValues();
    }

    public void SetVolumeMusic(float vol)
    {
        if (MusicCont != null)
        {
            MusicCont.SetVolume(vol);
        }
    }
    public void SetVolumeSFX(float vol)
    {
        if (SFXCont != null)
        {
            SFXCont.SetVolume(vol);
        }
    }

}
