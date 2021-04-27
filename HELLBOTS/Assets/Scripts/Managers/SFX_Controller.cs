using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Controller : MonoBehaviour
{
    private GameObject player;
    private AudioSource playerS;
    private GameObject[] Explosion;
    private GameObject[] Enemies;
    private GameObject[] PlayerBullets;
    private GameObject[] EnemyBullets;
    private GameObject[] Weapons;
    private GameObject[] CheckPoints;
    private AudioSource PBull;
    private AudioSource EBull;
    private AudioSource ExplosionS;
    private AudioSource EnemiesS;
    private AudioSource WeaponsS;
    private AudioSource CheckPointsS;
    private GameObject Door;
    private AudioSource DoorS;
    private SoundManager sound;
    private float Volume = 0.1f;

    private void Start()
    {
        sound = GetComponent<SoundManager>();
        player = GameObject.FindGameObjectWithTag("Hellbot");
        if (player != null)
        {
            playerS = player.GetComponent<AudioSource>();
        }
        Door = GameObject.FindGameObjectWithTag("Door");
        if (Door != null)
        {
            DoorS = Door.GetComponent<AudioSource>();
        }

        Volume = sound.GetSFXVol();

    }
    void Update()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        CheckPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        Explosion = GameObject.FindGameObjectsWithTag("Explosion");
        PlayerBullets = GameObject.FindGameObjectsWithTag("Playerbullet");
        EnemyBullets = GameObject.FindGameObjectsWithTag("Enemybullet");
        Weapons = GameObject.FindGameObjectsWithTag("ActiveWeapon");
        for (int i = 0; i < Explosion.Length; i++)
        {
            ExplosionS = Explosion[i].GetComponent<AudioSource>();
            ExplosionS.volume = Volume;
        }

        for (int i = 0; i < Enemies.Length; i++)
        {
            EnemiesS = Enemies[i].GetComponent<AudioSource>();
            EnemiesS.volume = Volume;
        }

        for (int i = 0; i < PlayerBullets.Length; i++)
        {
            PBull = PlayerBullets[i].GetComponent<AudioSource>();
            PBull.volume = Volume;
        }
        for (int i = 0; i < EnemyBullets.Length; i++)
        {
            EBull = EnemyBullets[i].GetComponent<AudioSource>();
            EBull.volume = Volume;
        }
        for (int i = 0; i < Weapons.Length; i++)
        {
            WeaponsS = Weapons[i].GetComponent<AudioSource>();
            WeaponsS.volume = Volume;
        }
        for (int i = 0; i < CheckPoints.Length; i++)
        {
            CheckPointsS = CheckPoints[i].GetComponent<AudioSource>();
            CheckPointsS.volume = Volume;
        }
        if (player != null)
        {
            playerS.volume = Volume;
        }




        if (Door != null)
        {
            DoorS.volume = Volume;
        }


    }

    public void SetVolume(float vol)
    {
        if (sound != null && sound.SliderUpdate())
        {
            Volume = vol;
        }

    }
}
