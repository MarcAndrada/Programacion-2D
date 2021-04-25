﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCamera : MonoBehaviour
{

    public GameObject Player;
    public Vector2 minCamPos;
    public Vector2 maxCamPos;
    public GameObject optionMenu;
    public float camSpeed;
    public bool LookingUp = false;

    private bool GodModeOn;
    private bool godmode;
    private bool Menu;
    private SoundManager sound;

    // Start is called before the first frame update
    void Start()
    {
        GodModeOn = false;
        optionMenu.SetActive(false);
        sound = GetComponentInChildren<SoundManager>();
        Time.timeScale = 1;
    }

    private void Update()
    {
        float delta = Time.deltaTime * 1000;
        godmode = HellbotInput.GodMode;
        Menu = HellbotInput.Menu;

        if (Menu)
        {
          
            if (Time.timeScale == 1)
            {
                PauseGame();
            }
            else if (Time.timeScale == 0){
                // si la velocidad es 0
                ResumeGame();
            }
        }



        if (godmode && !GodModeOn)
        {
            GodModeOn = true;
        }else if (godmode && GodModeOn){
            GodModeOn = false;
        }

    }
    // Update is called once per frame
    void FixedUpdate(){

        if (!GodModeOn)
        {

            float posX;
            float posY;

            if (!LookingUp)
            {
                posX = Player.transform.position.x + 275;
                posY = Player.transform.position.y;
            }
            else
            {
                posX = Player.transform.position.x;
                posY = Player.transform.position.y + 275;
            }
            
           
            

            transform.position = Vector3.MoveTowards(
                transform.position, 
                new Vector3(Mathf.Clamp(posX, minCamPos.x, maxCamPos.x), Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),transform.position.z),
                camSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
        }
       
    }


    public void SetMaxCamX(float MaxX)
    {
        maxCamPos.x = MaxX;
    }
    public void SetMaxCamY(float MaxY)
    {
        maxCamPos.y = MaxY;
    }
    public void SetMinCamX(float MinX)
    {
        minCamPos.x = MinX;
    }
    public void SetMinCamY(float MinY)
    {
        minCamPos.y = MinY;
    }
    public void SetLookUp(bool whereLook)
    {
        LookingUp = whereLook;
    }

    public void PauseGame() {
        Cursor.visible = true;
        Time.timeScale = 0;
        optionMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        optionMenu.SetActive(false);// que la velocidad del juego regrese a 1
        Cursor.visible = false;
        sound.SaveValues();
    }

    public void returnToGame()
    {
        Menu = true;
    }
}



