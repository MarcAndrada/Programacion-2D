using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCamera : MonoBehaviour
{

    public GameObject Player;
    public Vector2 minCamPos;
    public Vector2 maxCamPos;
    public float UPMinYPos;
    public float DownMinYPos;
    public GameObject optionMenu;

    private bool GodModeOn;
    private bool godmode;
    private bool Menu;


    // Start is called before the first frame update
    void Start()
    {
        GodModeOn = false;
        optionMenu.SetActive(false);
    }

    private void Update()
    {
        godmode = HellbotInput.GodMode;

        Menu = HellbotInput.Menu;

        if (Menu)
        {
          
            if (Time.timeScale == 1)
            {    //si la velocidad es 1
                Cursor.visible = true;
                Time.timeScale = 0;
                optionMenu.SetActive(true);
                
                //que la velocidad del juego sea 0
            }
            else if (Time.timeScale == 0)
            {   // si la velocidad es 0
                Time.timeScale = 1;
                optionMenu.SetActive(false);// que la velocidad del juego regrese a 1
                Cursor.visible = false;
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
            float posX = Player.transform.position.x;
            float posY = Player.transform.position.y;

            transform.position =
                new Vector3(
                    Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
                    Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
                    transform.position.z);
        }
        else
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "CamUp"){
            GoUP();
        }
    }


    private void GoUP()
    {
        minCamPos.y = UPMinYPos;
    }


    public void returnToGame()
    {
        Menu = true;
    }
}



