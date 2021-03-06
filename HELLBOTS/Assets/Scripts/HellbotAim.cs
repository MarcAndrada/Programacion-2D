using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellbotAim : MonoBehaviour
{
    public GameObject Crosshair;
    private Vector3 mouse_pos;
    public Transform Weapon;
    private float angle;


    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Cursor.visible = false;
        Crosshair.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mouse_pos - transform.position).normalized;
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Weapon.transform.eulerAngles = new Vector3(0, 0, angle);
        Debug.Log(angle);

        /*mouse_pos.z = 5;
        
        
        //Weapon.transform.rotation = Quaternion.Euler( new Vector3(0, 0, angle));
        //Vector3 targetDirection = Crosshair.position - Weapon.transform.position;
        //Vector3 newDirection = Vector3.RotateTowards(Weapon.transform.right, targetDirection, AimSpeed*Time.deltaTime,0.0f);
       
        Weapon.transform.RotateAround(transform.position, new Vector3 (0,0,dir) , AimSpeed * Time.deltaTime);
        */
    }
}
