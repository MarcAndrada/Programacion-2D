using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellbotControllers : MonoBehaviour
{
    public BoxCollider2D Crouchbc2D;
    public BoxCollider2D Normalbc2D;
    public Vector2 jumpHeight;
    public float runSpeed;
    public int jumpDone;


    private float horizontal;
    private bool jump;
    private bool crouch_keyD;
    private bool crouch_keyU;
    private Rigidbody2D rb2d;
    private int jumpLimit = 2;
    private float MaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        MaxSpeed = runSpeed;
        rb2d = GetComponent<Rigidbody2D>();
        jumpDone = 0;
        Crouchbc2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = HellbotInput.Horizontal;
        jump = HellbotInput.Jump;
        crouch_keyD = HellbotInput.CrouchDown;
        crouch_keyU = HellbotInput.CrouchUp;

        //Salto
        if (jump)
        {
            rb2d.drag = 0f;
            if (jumpDone < jumpLimit)
            {
                rb2d.AddForce(jumpHeight, ForceMode2D.Impulse);
                jumpDone++;
                runSpeed = 200;
            }
        }
        //Crouch (Si presiono "S", el collider grande se desactiva)
        if (crouch_keyD)
        {
            Normalbc2D.enabled = false;
            Crouchbc2D.enabled = true;
            transform.localScale = new Vector3(1, 0.5f, 1);
            runSpeed -= 100;
            jumpHeight -= new Vector2(0, 200);
        }
        else if (crouch_keyU)
        {
            Normalbc2D.enabled = true;
            Crouchbc2D.enabled = false;
            transform.localScale = new Vector3(1, 1, 1);
            runSpeed += 100;
            jumpHeight += new Vector2(0, 200);
        }


    }
    void FixedUpdate()
    {
        //rb2d.velocity = new Vector2(horizontal * runSpeed, 0 );
        rb2d.AddForce(Vector2.right * runSpeed * horizontal);

    }
    void OnCollisionEnter2D(Collision2D obj)//Detectar si toca el suelo para reiniciar la cantidad de saltos
    {
        if (obj.collider.tag == "Floor")
        {
            jumpDone = 0;
            rb2d.drag = 3;
            runSpeed = MaxSpeed;
        }

        if (obj.collider.tag == "WallFloor")
        {
            jumpDone = 0;
            rb2d.drag = 3;
            runSpeed = MaxSpeed;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemybullet"))
        {
          //  Destroy(gameObject);
        }
    }

}
