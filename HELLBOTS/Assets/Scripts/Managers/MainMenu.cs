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
    private bool getDark = false;
    private float TimeToWait = 1500;
    private float TimeWaited;
    private string nextScene;
    private void Start()
    {
        AudioManagers = GameObject.Find("Managers");
        soundCont = AudioManagers.GetComponentInChildren<SoundManager>();
        MusicCont = AudioManagers.GetComponentInChildren<VolumeController>();
        SFXCont = AudioManagers.GetComponentInChildren<SFX_Controller>();

    }
    private void Update()
    {
        float delta = Time.deltaTime * 1000;

        if (getDark)
        {
            TimeWaited += delta;
        }

        if (TimeWaited >= TimeToWait)
        {
            SceneManager.LoadScene(nextScene);
            getDark = false;
            TimeWaited = 0;

        }

    }

    public void goLevelManager()
    {
        TransitionController.ChangeScene();
        getDark = true;
        nextScene = "LevelManager";
        soundCont.Restart();
    }
    public void goTutorial(){
        TransitionController.ChangeScene();
        getDark = true;
        nextScene = "Tutorial";
        soundCont.Restart();
        TransitionController.ActiveLoadIcon();

    }

    public void goMap1()
    {
        TransitionController.ChangeScene();
        getDark = true;
        nextScene = "Map1";
        soundCont.Restart();
        TransitionController.ActiveLoadIcon();

    }

    public void goMap2()
    {
        TransitionController.ChangeScene();
        getDark = true;
        nextScene = "Map2";
        soundCont.Restart();
        TransitionController.ActiveLoadIcon();

    }

    public void goMap3()
    {
        TransitionController.ChangeScene();
        getDark = true;
        nextScene = "Map3";
        soundCont.Restart();
        TransitionController.ActiveLoadIcon();
    }

    public void goOptions()
    {
        TransitionController.ChangeScene();
        getDark = true;
        nextScene = "OptionMenu";
        soundCont.Restart();
    }

    public void goMainMenu()
    {
        Time.timeScale = 1;
        TransitionController.ChangeScene();
        getDark = true;
        nextScene = "MainMenu";
        soundCont.Restart();
    }

    public void goCredits()
    {
        TransitionController.ChangeScene();
        getDark = true;
        nextScene = "WinScene";
    }
    public void ExitGame()
    {

        Application.Quit();
    }

    public void SaveValues()
    {
        soundCont.SaveValues();
        TransitionController.ActiveLoadIcon();

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
