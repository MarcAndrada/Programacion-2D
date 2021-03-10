﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBasic : MonoBehaviour
{
    private SpriteRenderer sprite;
    private BasicEnemyController Controller;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        Controller = GetComponent<BasicEnemyController>();
        Controller.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            sprite.enabled = true;
            Controller.enabled = true;
        }
    }
}