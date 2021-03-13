using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public GameObject Player;
    public Vector2 minCamPos;
    public Vector2 maxCamPos;


    private bool GodModeOn;
    private bool godmode;

    // Start is called before the first frame update
    void Start()
    {
        GodModeOn = false;
    }

    private void Update()
    {
        godmode = HellbotInput.GodMode;

        if (godmode && !GodModeOn)
        {
            GodModeOn = true;
        }else if (godmode && GodModeOn){
            GodModeOn = false;
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {

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
}
