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
    public GameObject PistolaHUD;
    public GameObject AmetralladoraHUD;
    public GameObject EscopetaHUD;
    public GameObject BazookaHUD;
    public GameObject SniperHUD;
    public Transform Arm;
    public AudioClip GrabWeapon;

    public enum WEAPON { PISTOLA, AMETRALLADORA, ESCOPETA, BAZOOKA, SNIPER };
    public WEAPON ActiveWeapon = WEAPON.PISTOLA;

    private bool pick;
    private string weaponName;
    private bool OnWeapon;
    private int Look;
    private bool firstTime;

    private AudioSource audioSource;
    private GameObject floorWeapon;
    private WeaponsController weapon;
    private ShootgunController ShotgunCont;


    private void Start() {
        Cursor.visible = false;
        weapon = GetComponentInChildren<WeaponsController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        pick = HellbotInput.Interact;


        if (Crosshair.transform.position.x < transform.position.x && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Look = 0;
        }

        if (Crosshair.transform.position.x > transform.position.x && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Look = 1;
        }

        if (pick && OnWeapon && weaponName == "Escopeta")
        {
            audioSource.PlayOneShot(GrabWeapon);
            ActiveWeapon = WEAPON.ESCOPETA;
            OnWeapon = false;
            Destroy(floorWeapon);
            firstTime = true;
            
                
            
           


        }
        else if (pick && OnWeapon && weaponName != "Escopeta"){
            audioSource.PlayOneShot(GrabWeapon);
            if (weaponName == "Pistola")
            {
                ActiveWeapon = WEAPON.PISTOLA;
            }
            else if (weaponName == "Ametralladora"){
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
            
            //&& Escopeta.activeInHierarchy == fals
            OnWeapon = false;
              
           

            firstTime = true;
            Destroy(floorWeapon);
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
                PistolaHUD.SetActive(true);
                AmetralladoraHUD.SetActive(false);
                BazookaHUD.SetActive(false);
                SniperHUD.SetActive(false);
                EscopetaHUD.SetActive(false);

                break;
            case WEAPON.AMETRALLADORA:
                Pistola.SetActive(false);
                Ametralladora.SetActive(true);
                Bazooka.SetActive(false);
                Sniper.SetActive(false);
                Escopeta.SetActive(false);
                PistolaHUD.SetActive(false);
                AmetralladoraHUD.SetActive(true);
                BazookaHUD.SetActive(false);
                SniperHUD.SetActive(false);
                EscopetaHUD.SetActive(false);
                weapon = GetComponentInChildren<WeaponsController>();
               
                break;
            case WEAPON.ESCOPETA:
                Pistola.SetActive(false);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(false);
                Sniper.SetActive(false);
                Escopeta.SetActive(true);
                PistolaHUD.SetActive(false);
                AmetralladoraHUD.SetActive(false);
                BazookaHUD.SetActive(false);
                SniperHUD.SetActive(false);
                EscopetaHUD.SetActive(true);
                ShotgunCont = Escopeta.GetComponentInChildren<ShootgunController>();
                break;
            case WEAPON.BAZOOKA:
                Pistola.SetActive(false);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(true);
                Sniper.SetActive(false);
                Escopeta.SetActive(false);
                PistolaHUD.SetActive(false);
                AmetralladoraHUD.SetActive(false);
                BazookaHUD.SetActive(true);
                SniperHUD.SetActive(false);
                EscopetaHUD.SetActive(false);
                weapon = GetComponentInChildren<WeaponsController>();
                break;
            case WEAPON.SNIPER:
                Pistola.SetActive(false);
                Ametralladora.SetActive(false);
                Bazooka.SetActive(false);
                Sniper.SetActive(true);
                Escopeta.SetActive(false);
                PistolaHUD.SetActive(false);
                AmetralladoraHUD.SetActive(false);
                BazookaHUD.SetActive(false);
                SniperHUD.SetActive(true);
                EscopetaHUD.SetActive(false);
                weapon = GetComponentInChildren<WeaponsController>();
                break;

        }

        if (firstTime)
        {
            if (ActiveWeapon != WEAPON.ESCOPETA)
            {
                weapon.Reload();
            }
            else
            {
                ShotgunCont.Reload();
            }
            firstTime = false;
        }

        
        Crosshair.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        Arm.position = Crosshair.transform.position;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Transform childGO;
            weaponName = collision.gameObject.name;
            OnWeapon = true;
            childGO = collision.gameObject.transform.GetChild(0);
            childGO.gameObject.SetActive(true);
            floorWeapon = collision.gameObject;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Transform childGO;
            weaponName = collision.gameObject.name;
            OnWeapon = true;
            childGO = collision.gameObject.transform.GetChild(0);
            childGO.gameObject.SetActive(true);
            floorWeapon = collision.gameObject;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Transform childGO;
            childGO = collision.gameObject.transform.GetChild(0);
            OnWeapon = false;
            childGO.gameObject.SetActive(false);

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
