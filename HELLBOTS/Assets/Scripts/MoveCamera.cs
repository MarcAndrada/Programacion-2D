using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public GameObject Player;
    public Vector2 minCamPos;
    public Vector2 maxCamPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = Player.transform.position.x;
        float posY = Player.transform.position.y;

        transform.position = 
            new Vector3(
                Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
                Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
                transform.position.z) ;
    }
}
