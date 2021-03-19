using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void goGame(){
        SceneManager.LoadScene("Map1");
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
