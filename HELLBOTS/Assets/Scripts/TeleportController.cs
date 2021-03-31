using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportController : MonoBehaviour
{
    private string NextScene;
    private string CurrentScene;
    private void Start()
    {
        CurrentScene = SceneManager.GetActiveScene().name;
        if ( CurrentScene == "Tutorial")
        {
            NextScene = "Map1";
        }
        if (CurrentScene == "Map1")
        {
            NextScene = "Map2";
        }
        if (CurrentScene == "Map2")
        {
            NextScene = "WinScene";
        }

    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hellbot")
        {
            SceneManager.LoadScene(NextScene);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hellbot")
        {
            SceneManager.LoadScene(NextScene);

        }
    }
}
