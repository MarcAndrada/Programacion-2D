using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform SoporteArma;
    private float AnguloRotacion;

    public float offsetBullet;
    public float fireRate;

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
            if (TimeToShoot > fireRate)
            {
                //bullet = Instantiate(bulletPrefab, pos, SoporteArma.rotation);
                bullet = Instantiate(bulletPrefab, pos, transform.rotation);
                Destroy(bullet, 3);
                TimeToShoot = 0;
            }

        }
    }
}
