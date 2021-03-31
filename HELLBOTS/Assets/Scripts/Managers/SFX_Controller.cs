using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Controller : MonoBehaviour
{
    public AudioSource player;
    public AudioSource Pistola;
    public AudioSource Ametralladora;
    public AudioSource Escopeta;
    public AudioSource Bazooka;
    public AudioSource Sniper;


    private GameObject[] Explosion;
    private GameObject[] Enemies;
    private GameObject[] PlayerBullets;
    private GameObject[] EnemyBullets;
    private AudioSource PBull;
    private AudioSource EBull;
    private AudioSource ExplosionS;
    private AudioSource EnemiesS;
    private GameObject Door;
    private AudioSource DoorS;
    private float Volume = 0.1f;

    private void Start()
    {
         Door = GameObject.FindGameObjectWithTag("Door");
         DoorS = Door.GetComponent<AudioSource>();
    }
    void Update()
    {
        player.volume = Volume;
        Pistola.volume = Volume;
        Ametralladora.volume = Volume;
        Escopeta.volume = Volume;
        Bazooka.volume = Volume;
        Sniper.volume = Volume;
        

        Explosion = GameObject.FindGameObjectsWithTag("Explosion");
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        PlayerBullets = GameObject.FindGameObjectsWithTag("Playerbullet");
        EnemyBullets = GameObject.FindGameObjectsWithTag("Enemybullet");
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

        DoorS.volume = Volume;

    }

    public void SetVolume(float vol)
    {
        Volume = vol;
    }
}
