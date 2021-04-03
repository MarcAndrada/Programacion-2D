using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShootgunController : MonoBehaviour
{
    [Header("Extern References")]
    public GameObject bulletPrefab;
    public Transform SoporteArma;
    [Header("Textos")]
    public GameObject MaxBulletsT;
    public GameObject CurrentBulletsT;
    public GameObject Infinite1;
    public GameObject Infinite2;
    [Header("Audios")]
    public AudioClip soundShoot;
    public AudioClip outOfAmmoS;

    [Header("Weapon Config")]
    public float offsetBullet;
    public float fireRate;
    public int MaxAmmo;
    public int CurrentAmmo;

    private AudioSource audioSource;
    private GameObject bullet1;
    private GameObject bullet2;
    private GameObject bullet3;
    private GameObject bullet4;
    private GameObject bullet5;
    private GameObject Crosshair;
    private GameObject Player;


    
    private Quaternion AnguloRot1;
    private Quaternion AnguloRot2;
    private Quaternion AnguloRot3;
    private Quaternion AnguloRot4;
    private Quaternion AnguloRot5;
    private float TimeToShoot;
    private bool Shoot;
    // Start is called before the first frame update
    void Start()
    {
        Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Player = GameObject.FindGameObjectWithTag("Hellbot");
        audioSource = GetComponent<AudioSource>();


       

        CurrentAmmo = MaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot = HellbotInput.Shoot;
        
        AnguloRot1 = transform.rotation * Quaternion.Euler(0,0, 20);
        AnguloRot2 = transform.rotation * Quaternion.Euler(0, 0, 10);
        AnguloRot3 = transform.rotation;
        AnguloRot4 = transform.rotation * Quaternion.Euler(0, 0, -10);
        AnguloRot5 = transform.rotation * Quaternion.Euler(0, 0, -20);

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
            if (TimeToShoot > fireRate && CurrentAmmo > 0)
            {
               audioSource.PlayOneShot(soundShoot);
                // audioSource.PlayOneShot(barrelReload);
                Quaternion rot = transform.rotation;
                bullet1 =  Instantiate(bulletPrefab, pos, AnguloRot1);
                Destroy(bullet1, 2);                    
                bullet2 = Instantiate(bulletPrefab, pos, AnguloRot2);
                Destroy(bullet2, 2);                     
                bullet3 = Instantiate(bulletPrefab, pos, AnguloRot3);
                Destroy(bullet3, 2);                    
                bullet4 = Instantiate(bulletPrefab, pos, AnguloRot4);
                Destroy(bullet4, 2);                  
                bullet5 = Instantiate(bulletPrefab, pos, AnguloRot5);
                Destroy(bullet5, 2);
                TimeToShoot = 0;
                
                CurrentAmmo = CurrentAmmo - 5;
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
        }
        else if (MaxAmmo == -1)
        {
            CurrentBulletsT.SetActive(false);
            MaxBulletsT.SetActive(false);
        }
        CurrentBulletsT.GetComponent<Text>().text = CurrentAmmo.ToString();
        MaxBulletsT.GetComponent<Text>().text = MaxAmmo.ToString();

    }



    public void Reload()
    {
        CurrentAmmo = MaxAmmo;
    }
}
