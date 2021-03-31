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
    public AudioClip soundShoot;
    public AudioClip outOfAmmoS;


    public float offsetBullet;
    public float fireRate;
    public int MaxAmmo;
    public int CurrentAmmo;

    private AudioSource audioSource;
    private GameObject bullet;
    private GameObject Crosshair;
    private GameObject Player;

    private float TimeToShoot;
    private bool Shoot;
 

    // Start is called before the first frame update
    void Start()
    {
        Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Player = GameObject.FindGameObjectWithTag("Hellbot");
        audioSource = GetComponent<AudioSource>();

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
            pos = -transform.right * offsetBullet + transform.position;
        }


        TimeToShoot += delta;
        if (Shoot)
        {
            if (TimeToShoot > fireRate && CurrentAmmo > 0 || TimeToShoot > fireRate && gameObject.name == "Pistola")
            {
                //Hacer sonido de dispar
                audioSource.PlayOneShot(soundShoot);
                //bullet = Instantiate(bulletPrefab, pos, SoporteArma.rotation);
                bullet = Instantiate(bulletPrefab, pos, transform.rotation);
                Destroy(bullet, 1.6f);
                TimeToShoot = 0;
                CurrentAmmo--;

            }
            else if (CurrentAmmo == 0 && TimeToShoot > fireRate)
            {
                //Hacer Soniditos de Sin municion
                audioSource.PlayOneShot(outOfAmmoS);
                TimeToShoot = 0;

            }

        }
        if (MaxAmmo >= 0)
        {
            CurrentBulletsT.SetActive(true);
            MaxBulletsT.SetActive(true);
            Infinite1.SetActive(false);
            Infinite2.SetActive(false);
        } else if (MaxAmmo == -1) {
            CurrentBulletsT.SetActive(false);
            MaxBulletsT.SetActive(false);

            Infinite1.SetActive(true);
            Infinite2.SetActive(true);


        }
        CurrentBulletsT.GetComponent<Text>().text = CurrentAmmo.ToString();

    }


}

    
