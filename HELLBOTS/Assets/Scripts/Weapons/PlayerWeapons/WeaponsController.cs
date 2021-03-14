using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform SoporteArma;
    public GameObject MaxBulletsT;
    public GameObject CurrentBulletsT;
    public GameObject Infinite1;
    public GameObject Infinite2;

    public float offsetBullet;
    public float fireRate;
    public int MaxAmmo;

    private GameObject bullet;
    private GameObject Crosshair;
    private GameObject Player;

    private int CurrentAmmo;
    private float TimeToShoot;
    private bool Shoot;

    // Start is called before the first frame update
    void Start()
    {
        Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Player = GameObject.FindGameObjectWithTag("Hellbot");

        CurrentAmmo = MaxAmmo;

        MaxBulletsT.GetComponent<Text>().text = MaxAmmo.ToString();

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


        TimeToShoot += delta;
        if (Shoot)
        {
            if (TimeToShoot > fireRate && CurrentAmmo > 0 || TimeToShoot > fireRate && gameObject.name == "Pistola")
            {
                //Hacer sonido de dispar
                //bullet = Instantiate(bulletPrefab, pos, SoporteArma.rotation);
                bullet = Instantiate(bulletPrefab, pos, transform.rotation);
                Destroy(bullet, 3);
                TimeToShoot = 0;
                CurrentAmmo--;
                
            }
            else if (CurrentAmmo <= 0)
            {
                //Hacer Soniditos de Sin municion
            }

        }
        if (MaxAmmo >= 0)
        {
            CurrentBulletsT.SetActive(true);
            MaxBulletsT.SetActive(true);
            Infinite1.SetActive(false);
            Infinite2.SetActive(false);
        }else if (MaxAmmo == -1){
            CurrentBulletsT.SetActive(false);
            MaxBulletsT.SetActive(false);

            Infinite1.SetActive(true);
            Infinite2.SetActive(true);


        }
        CurrentBulletsT.GetComponent<Text>().text = CurrentAmmo.ToString();

    }


    public void SetMaxAmmo()
    {
        CurrentAmmo = MaxAmmo;
    }
}

    
