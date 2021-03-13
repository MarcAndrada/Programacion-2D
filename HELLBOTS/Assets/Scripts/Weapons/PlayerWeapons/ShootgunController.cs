using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootgunController : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float offsetBullet;
    public float fireRate;

    private GameObject bullet1;
    private GameObject bullet2;
    private GameObject bullet3;
    private GameObject bullet4;
    private GameObject bullet5;
    private GameObject Crosshair;
    private GameObject Player;

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
        Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Player = GameObject.FindGameObjectWithTag("Hellbot");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot = HellbotInput.Shoot;

        AnguloRot1 = transform.rotation.z - 40;
        AnguloRot2 = transform.rotation.z - 20;
        AnguloRot3 = transform.rotation.z;
        AnguloRot4 = transform.rotation.z + 20;
        AnguloRot5 = transform.rotation.z + 40;

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
            if (TimeToShoot > fireRate)
            {
                bullet1 = Instantiate(bulletPrefab, pos, Quaternion.Euler(transform.rotation.x, transform.rotation.y, AnguloRot1));
                Destroy(bullet1, 3);                    
                bullet2 = Instantiate(bulletPrefab, pos, Quaternion.Euler(transform.rotation.x, transform.rotation.y, AnguloRot2));
                Destroy(bullet2, 3);                     
                bullet3 = Instantiate(bulletPrefab, pos, Quaternion.Euler(transform.rotation.x, transform.rotation.y, AnguloRot3));
                Destroy(bullet3, 3);                    
                bullet4 = Instantiate(bulletPrefab, pos, Quaternion.Euler(transform.rotation.x, transform.rotation.y, AnguloRot4));
                Destroy(bullet4, 3);                  
                bullet5 = Instantiate(bulletPrefab, pos, Quaternion.Euler(transform.rotation.x, transform.rotation.y, AnguloRot5));
                Destroy(bullet5, 3);
                TimeToShoot = 0;
            }
        }
    }

   
}
