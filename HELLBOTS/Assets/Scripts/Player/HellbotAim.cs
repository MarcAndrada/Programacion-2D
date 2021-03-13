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
    private int Look;
    private GameObject floorWeapon;

    /*private float angle;
    private Vector3 mouse_pos;*/

    void Update()
    {
        pick = HellbotInput.Interact;
        if (Crosshair.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, transform.localScale.z);
            Look = 0;
        }

        if (Crosshair.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, transform.localScale.z);
            Look = 1;
        }
       
        if (pick && OnWeapon){
            if (weaponName == "Pistola"){
                ActiveWeapon = WEAPON.PISTOLA;
            }else if (weaponName == "Ametralladora"){
                ActiveWeapon = WEAPON.AMETRALLADORA;
            }else if (weaponName == "Bazooka"){
                ActiveWeapon = WEAPON.BAZOOKA;
            }else if (weaponName == "Sniper"){
                ActiveWeapon = WEAPON.SNIPER;
            }else if (weaponName == "Escopeta"){
                ActiveWeapon = WEAPON.ESCOPETA;
            }
            OnWeapon = false;
            Destroy(floorWeapon);
        }
        

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Cursor.visible = false;
        Crosshair.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

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

        switch (ActiveWeapon)
        {
            default:
                break;
            case WEAPON.PISTOLA:
                Pistola.SetActive(true);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(false);
                Sniper.SetActive(false);
                Escopeta.SetActive(false);
                break;
            case WEAPON.AMETRALLADORA:
                Pistola.SetActive(false);
                Ametralladora.SetActive(true);
                Bazooka.SetActive(false);
                Sniper.SetActive(false);
                Escopeta.SetActive(false);
                break;
            case WEAPON.ESCOPETA:
                Pistola.SetActive(false);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(false);
                Sniper.SetActive(false);
                Escopeta.SetActive(true);
                break;
            case WEAPON.BAZOOKA:
                Pistola.SetActive(false);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(true);
                Sniper.SetActive(false);
                Escopeta.SetActive(false);
                break;
            case WEAPON.SNIPER:
                Pistola.SetActive(false);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(false);
                Sniper.SetActive(true);
                Escopeta.SetActive(false);
                break;
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Weapon"){
            weaponName = collision.gameObject.name;
            OnWeapon = true;

            floorWeapon = collision.gameObject;

        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.tag == "Weapon"){
            OnWeapon = false;
        }

    }

    public void Dead() { 
            Pistola.SetActive(false);
            Ametralladora.SetActive(false);
            Bazooka.SetActive(false);
            Sniper.SetActive(false);
            Escopeta.SetActive(false);

    }

    public bool Heal()
    {
        if (ActiveWeapon != WEAPON.PISTOLA){
            return true;
        }else{
            return false;
        }
    }

    public void ResetWeapon(){
        ActiveWeapon = WEAPON.PISTOLA;
    }

    public int LookingAt()
    {
        return Look;
    }
}
