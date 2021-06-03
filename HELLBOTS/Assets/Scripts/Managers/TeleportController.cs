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
    private bool getDark = false;
    private float TimeToWait = 1500;
    private float TimeWaited;


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
            NextScene = "Map4";

        }
        if (CurrentScene == "Map4")
        {
            NextScene = "WinScene";

        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 65);
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

            SceneManager.LoadScene(NextScene);
            getDark = false;
            TimeWaited = 0;

        }

        if (HellbotInput.GoLevel1)
        {
            TransitionController.ChangeScene();
            TransitionController.ActiveLoadIcon();
            getDark = true;
            NextScene = "Map1";
        }

        if (HellbotInput.GoLevel2)
        {
            TransitionController.ChangeScene();
            getDark = true;
            NextScene = "Map2";
            TransitionController.ActiveLoadIcon();
        }

        if (HellbotInput.GoLevel3)
        {
            TransitionController.ChangeScene();
            getDark = true;
            NextScene = "Map3";
            TransitionController.ActiveLoadIcon();
        }

        if (HellbotInput.GoLevel4)
        {
            TransitionController.ChangeScene();
            getDark = true;
            NextScene = "Map4";
            TransitionController.ActiveLoadIcon();
        }
        WaitedTime += delta;
        if (WaitTime < WaitedTime)
        {
            transform.rotation = Quaternion.Euler(Random.Range(-rot, rot), Random.Range(-rot, rot), Random.Range(-1,1));
            WaitedTime = 0;
        }

        if (gameObject.name == "PortalInicio")
        {
            if (transform.localScale.x >= 0 )
            {
                transform.localScale = new Vector3(transform.localScale.x - 0.005f, transform.localScale.y, 1);

            }

            if (transform.localScale.y >= 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.005f, 1);
            }

            if (transform.localScale.x <= 0 && transform.localScale.y <= 0 )
            {
                Destroy(gameObject);
            }
        }



    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
      


        if (collision.gameObject.tag == "Hellbot")
        {

            TransitionController.ChangeScene();
            getDark = true;
            TransitionController.ActiveLoadIcon();

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
