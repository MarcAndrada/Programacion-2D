using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellbotAim : MonoBehaviour
{
    public GameObject Crosshair;
    public GameObject Pistola;
    public GameObject Ametralladora;
    public GameObject Escopeta;
    public GameObject Bazooka;
    public GameObject Sniper;
    public Transform Arm;

    public enum WEAPON { PISTOLA, AMETRALLADORA, ESCOPETA, BAZOOKA, SNIPER};
    public WEAPON ActiveWeapon = WEAPON.PISTOLA;

    private bool pick;
    private string weaponName;
    private bool OnWeapon;
    private GameObject floorWeapon;
    /*private float angle;
    private Vector3 mouse_pos;*/

    void Update()
    {
        pick = HellbotInput.Interact;
        if (Crosshair.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, transform.localScale.z);
        }

        if (Crosshair.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, transform.localScale.z);
        }
        if (pick && OnWeapon)
        {
            if (weaponName == "Ametralladora")
            {
                Pistola.SetActive(false);
                Ametralladora.SetActive(true);
                Bazooka.SetActive(false);
                Sniper.SetActive(false);
                Escopeta.SetActive(false);
            }
            else if (weaponName == "Bazooka"){
                Pistola.SetActive(false);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(true);
                Sniper.SetActive(false);
                Escopeta.SetActive(false);
            }
            else if (weaponName == "Sniper"){
                Pistola.SetActive(false);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(false);
                Sniper.SetActive(true);
                Escopeta.SetActive(false);
            }
            else if (weaponName == "Escopeta"){
                Pistola.SetActive(false);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(false);
                Sniper.SetActive(false);
                Escopeta.SetActive(true);
            }
            OnWeapon = false;
            Destroy(floorWeapon);
        }
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Cursor.visible = false;
        Crosshair.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

        Arm.position = Crosshair.transform.position;


            /* mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 aimDirection = (mouse_pos - transform.position).normalized;
            angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            Weapon.transform.eulerAngles = new Vector3(0, 0, angle);
            Debug.Log(angle);*/

            /*mouse_pos.z = 5;


            //Weapon.transform.rotation = Quaternion.Euler( new Vector3(0, 0, angle));
            //Vector3 targetDirection = Crosshair.position - Weapon.transform.position;
            //Vector3 newDirection = Vector3.RotateTowards(Weapon.transform.right, targetDirection, AimSpeed*Time.deltaTime,0.0f);

            Weapon.transform.RotateAround(transform.position, new Vector3 (0,0,dir) , AimSpeed * Time.deltaTime);
            */
        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon"){
            weaponName = collision.gameObject.name;
            OnWeapon = true;

            floorWeapon = collision.gameObject;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon"){
            OnWeapon = false;
        }

    }

}
