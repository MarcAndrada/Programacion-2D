using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private GameObject Crosshair;
    private GameObject Player;
    public float offsetBullet;
    private bool Shoot;


    // Start is called before the first frame update
    void Start()
    {
        Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Player = GameObject.FindGameObjectWithTag("Hellbot");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot = HellbotInput.Shoot;
        /* Debug.Log(SoporteArma.rotation.z);
         AnguloRotacion = SoporteArma.rotation.z * -100;
         //Debug.Log(AnguloRotacion);*/

        float delta = Time.deltaTime * 1000;
        Vector3 pos;
        if (Crosshair.transform.position.x > Player.transform.position.x)
        {
            pos = transform.right * offsetBullet + transform.position;
        }
        else
        {
            pos = Vector3.left * offsetBullet + transform.position;
        }
    }
}

