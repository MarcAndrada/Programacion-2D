using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
 
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


        Application.Quit();
    }


}
