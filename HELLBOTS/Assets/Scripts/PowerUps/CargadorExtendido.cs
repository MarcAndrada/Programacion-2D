using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargadorExtendido : MonoBehaviour
{
    public float CargadorExtendidoTimer;//Tiempo de duracion del powerup (SpeedBost)
    private bool CargadorExtendidoBool;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("Weapon");
    }

    // Update is called once per frame
    void Update()
    {


        if (CargadorExtendidoBool)
        {
            CargadorExtendidoTimer += Time.deltaTime;
            if (CargadorExtendidoTimer >= 10)//Tiempo de invulnerabilidad
            {
                CargadorExtendidoTimer = 0;
                CargadorExtendidoBool = false;
            }
        }
        if (Input.GetKey(KeyCode.F))
        {
            CargadorExtendidoBool = true;


        }
    }
}
