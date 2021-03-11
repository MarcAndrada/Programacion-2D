﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HellbotRestart : MonoBehaviour
{
    private bool restart;


    // Update is called once per frame
    void Update()
    {
        restart = HellbotInput.Restart;

        if (restart) {
            //Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


}
