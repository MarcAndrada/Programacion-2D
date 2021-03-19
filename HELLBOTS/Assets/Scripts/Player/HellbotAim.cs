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
    public AudioClip GrabWeapon;

    public enum WEAPON { PISTOLA, AMETRALLADORA, ESCOPETA, BAZOOKA, SNIPER };
    public WEAPON ActiveWeapon = WEAPON.PISTOLA;

    private bool pick;
    private string weaponName;
    private bool OnWeapon;
    private int Look;


    private AudioSource audioSource;
    private GameObject floorWeapon;
    private WeaponsController weapon;
    private ShootgunController Shotgun;


    private void Start() {
        Cursor.visible = false;
        weapon = GetComponentInChildren<WeaponsController>();
        audioSource = GetComponent<AudioSource>();
    }

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

        if (pick && OnWeapon && weaponName != "Escopeta"){
            audioSource.PlayOneShot(GrabWeapon);
            if (weaponName == "Pistola")
            {
                ActiveWeapon = WEAPON.PISTOLA;
            }
            else if (weaponName == "Ametralladora")
            {
                ActiveWeapon = WEAPON.AMETRALLADORA;
            }
            else if (weaponName == "Bazooka")
            {
                ActiveWeapon = WEAPON.BAZOOKA;
            }
            else if (weaponName == "Sniper")
            {
                ActiveWeapon = WEAPON.SNIPER;
            }
            weapon.CurrentAmmo = weapon.MaxAmmo;


            OnWeapon = false;
            Destroy(floorWeapon);
        }else if (pick && OnWeapon && weaponName == "Escopeta"){
            audioSource.PlayOneShot(GrabWeapon);
            ActiveWeapon = WEAPON.ESCOPETA;
            OnWeapon = false;
            Destroy(floorWeapon);

           /* if (ActiveWeapon == WEAPON.ESCOPETA)
            {
                Shotgun = GetComponentInChildren<ShootgunController>();
                Shotgun.CurrentAmmo = Shotgun.MaxAmmo;
            }*/
        }


    }
    // Update is called once per frame
    void FixedUpdate()
    {

        
        Arm.position = Crosshair.transform.position;
        Crosshair.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

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
                weapon = GetComponentInChildren<WeaponsController>();
               
                break;
            case WEAPON.ESCOPETA:
                Pistola.SetActive(false);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(false);
                Sniper.SetActive(false);
                Escopeta.SetActive(true);
                weapon = GetComponentInChildren<WeaponsController>();
                break;
            case WEAPON.BAZOOKA:
                Pistola.SetActive(false);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(true);
                Sniper.SetActive(false);
                Escopeta.SetActive(false);
                weapon = GetComponentInChildren<WeaponsController>();
                break;
            case WEAPON.SNIPER:
                Pistola.SetActive(false);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(false);
                Sniper.SetActive(true);
                Escopeta.SetActive(false);
                weapon = GetComponentInChildren<WeaponsController>();
                break;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            weaponName = collision.gameObject.name;
            OnWeapon = true;

            floorWeapon = collision.gameObject;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            OnWeapon = false;
        }

    }

    public void Dead()
    {
        Pistola.SetActive(false);
        Ametralladora.SetActive(false);
        Bazooka.SetActive(false);
        Sniper.SetActive(false);
        Escopeta.SetActive(false);

    }

    public bool Heal()
    {
        if (ActiveWeapon != WEAPON.PISTOLA)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetWeapon()
    {
        ActiveWeapon = WEAPON.PISTOLA;

    }

    public int LookingAt()
    {
        return Look;
    }

}
