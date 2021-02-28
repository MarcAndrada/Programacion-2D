using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellbotControllers : MonoBehaviour
{
    public BoxCollider2D Crouchbc2D;
    public BoxCollider2D Normalbc2D;


    public float runSpeed = 300f;
    public float crouchSpeed = 150f;
    bool crouch;
    private Rigidbody2D rb2d;

    public Vector2 jumpHeight;
    private int jumpLimit = 2;
    private int jumpDone;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jumpDone = 0;
        Crouchbc2D.enabled = false;
    }
   
    // Update is called once per frame
    void Update()
    {
        //Salto
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (jumpDone < jumpLimit) {
                GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
                jumpDone++;
            }
        }
        //Crouch (Si presiono "S", el collider grande se desactiva)
        if (Input.GetKeyDown(KeyCode.S))
        {
            Normalbc2D.enabled = false;
            Crouchbc2D.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            Normalbc2D.enabled = true;
            Crouchbc2D.enabled = false;
        }



        if (Input.GetKey("s")) {
            crouch = true;
        } else if (Input.GetKey("s")){
            crouch = false;
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        rb2d.AddForce(Vector2.right * runSpeed * h);
    }
    void OnCollisionEnter2D(Collision2D obj)//Detectar si toca el suelo para reiniciar la cantidad de saltos
    {
        if (obj.collider.tag == "Wall_Down")
        {
            jumpDone = 0;
        }

        
    }


}
