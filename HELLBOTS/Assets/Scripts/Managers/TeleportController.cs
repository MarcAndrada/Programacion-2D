using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportController : MonoBehaviour
{
    private string NextScene;
    private string CurrentScene;
    private float WaitTime = 75;
    private float WaitedTime;
    private int rot = 13;


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
            NextScene = "Map3";
        }
        if (CurrentScene == "Map3")
        {
            NextScene = "WinScene";
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 65);
    }

    private void Update()
    {

        if (HellbotInput.GoLevel1)
        {
            SceneManager.LoadScene("Map1");
        }

        if (HellbotInput.GoLevel2)
        {
            SceneManager.LoadScene("Map2");
        }

        if (HellbotInput.GoLevel3)
        {
            SceneManager.LoadScene("Map3");
        }

        float delta = Time.deltaTime * 1000;
        WaitedTime += delta;
        if (WaitTime < WaitedTime)
        {
            transform.rotation = Quaternion.Euler(Random.Range(-rot, rot), Random.Range(-rot, rot), Random.Range(-1,1));
            WaitedTime = 0;
        }

        if (gameObject.name == "PortalInicio")
        {
            if (transform.localScale.x >= 0.2f )
            {
                transform.localScale = new Vector3(transform.localScale.x - 0.01f, transform.localScale.y, 1);

            }

            if (transform.localScale.y >= 0.2f)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.01f, 1);
            }
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
