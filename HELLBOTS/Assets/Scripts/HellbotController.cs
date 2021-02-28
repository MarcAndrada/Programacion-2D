using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellbotController : MonoBehaviour
{


    public Transform Crosshair;

    // Start is called before the first frame update
    void Start()
    {
        //ocultar el cursor, cambiar de sitio mas adelante
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Crosshair.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }
}
