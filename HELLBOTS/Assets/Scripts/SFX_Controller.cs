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
    private AudioSource ExplosionS;
    private AudioSource EnemiesS;
    private float Volume = 0.1f;

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

      

    }

    public void SetVolume(float vol)
    {
        Volume = vol;
    }
}
