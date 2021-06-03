using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    public float TimeToGenerateWeapons;
    
    [Header("Weapons")]
    public GameObject Shotgun;
    public GameObject MachineGun;
    public GameObject Sniper;


    [Header("Positions")]
    public Vector3 Pos1;
    public Vector3 Pos2;
    public Vector3 Pos3;

    private float TimePassed = 0;
    private enum CurrentWeapons { SNIPER, SHOOTGUN, MACHINEHGUN, NONE}
    private CurrentWeapons WeaponGenerated;
    private GameObject NewWeapon;

    private void Start()
    {
        TimeToGenerateWeapons = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;
        TimePassed += delta;

        if (TimePassed >= TimeToGenerateWeapons)
        {
            int WeaponRand = Random.Range(0,3);
            switch (WeaponRand) {
                case 0:
                    WeaponGenerated = CurrentWeapons.SNIPER;
                    break;
                case 1:
                    WeaponGenerated = CurrentWeapons.SHOOTGUN;

                    break;

                case 2:
                    WeaponGenerated = CurrentWeapons.MACHINEHGUN;

                    break;


                    break;

            }

            switch (WeaponGenerated)
            {
                case CurrentWeapons.SNIPER:
                    NewWeapon = Instantiate(Sniper, Pos1, Quaternion.identity);
                    NewWeapon.name = "Sniper";

                    break;
                case CurrentWeapons.SHOOTGUN:
                    NewWeapon = Instantiate(Shotgun, Pos3, Quaternion.identity);
                    NewWeapon.name = "Escopeta";

                    break;
                case CurrentWeapons.MACHINEHGUN:
                    NewWeapon = Instantiate(MachineGun, Pos2, Quaternion.identity);
                    NewWeapon.name = "Ametralladora";
                    break;
                default:
                    break;
            }

            TimePassed = 0;

        }
        
    }
}
