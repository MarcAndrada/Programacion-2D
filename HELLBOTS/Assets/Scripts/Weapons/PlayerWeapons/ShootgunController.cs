using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShootgunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject MaxBulletsT;
    public GameObject CurrentBulletsT;
    public GameObject Infinite1;
    public GameObject Infinite2;

    public float offsetBullet;
    public float fireRate;
    public int CurrentAmmo;


    private GameObject bullet1;
    private GameObject bullet2;
    private GameObject bullet3;
    private GameObject bullet4;
    private GameObject bullet5;
    private GameObject Crosshair;
    private GameObject Player;

    private int MaxAmmo;
    private float AnguloRot1;
    private float AnguloRot2;
    private float AnguloRot3;
    private float AnguloRot4;
    private float AnguloRot5;
    private float TimeToShoot;
    private bool Shoot;
    // Start is called before the first frame update
    void Start()
    {
        MaxAmmo = CurrentAmmo;
        Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Player = GameObject.FindGameObjectWithTag("Hellbot");
        

        MaxBulletsT.GetComponent<Text>().text = MaxAmmo.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        Shoot = HellbotInput.Shoot;

        AnguloRot1 = transform.rotation.z + 0.2f;
        AnguloRot2 = transform.rotation.z + 0.1f;
        AnguloRot3 = transform.rotation.z;
        AnguloRot4 = transform.rotation.z - 0.1f;
        AnguloRot5 = transform.rotation.z - 0.2f;

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
                bullet1 = Instantiate(bulletPrefab, pos, Quaternion.EulerAngles(transform.rotation.x, transform.rotation.y, -AnguloRot1));
                Destroy(bullet1, 3);                    
                bullet2 = Instantiate(bulletPrefab, pos, Quaternion.EulerAngles(transform.rotation.x, transform.rotation.y, -AnguloRot2));
                Destroy(bullet2, 3);                     
                bullet3 = Instantiate(bulletPrefab, pos, Quaternion.EulerAngles(transform.rotation.x, transform.rotation.y, -AnguloRot3));
                Destroy(bullet3, 3);                    
                bullet4 = Instantiate(bulletPrefab, pos, Quaternion.EulerAngles(transform.rotation.x, transform.rotation.y, -AnguloRot4));
                Destroy(bullet4, 3);                  
                bullet5 = Instantiate(bulletPrefab, pos, Quaternion.EulerAngles(transform.rotation.x, transform.rotation.y, -AnguloRot5));
                Destroy(bullet5, 3);
                TimeToShoot = 0;

                CurrentAmmo = CurrentAmmo - 5;
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

    }


    public void SetMaxAmmo()
    {
        CurrentAmmo = MaxAmmo;
    }


}
