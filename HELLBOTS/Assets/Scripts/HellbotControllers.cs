using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellbotControllers : MonoBehaviour
{
    public BoxCollider2D Crouchbc2D;
    public BoxCollider2D Normalbc2D;

    private float horizontal;
    private bool jump;
    private bool crouch_keyD;
    private bool crouch_keyU;

    public float runSpeed;

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
        horizontal = HellbotInput.Horizontal;
        jump = HellbotInput.Jump;
        crouch_keyD = HellbotInput.CrouchDown;
        crouch_keyU = HellbotInput.CrouchUp;

        //Salto
        if (jump) {
            if (jumpDone < jumpLimit) {
                GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
                jumpDone++;
            }
        }
        //Crouch (Si presiono "S", el collider grande se desactiva)
        if (crouch_keyD){
            Normalbc2D.enabled = false;
            Crouchbc2D.enabled = true;
            transform.localScale = new Vector3(1, 0.5f, 1);
            runSpeed -= 100;
            jumpHeight -= new Vector2 (0,200);
        }else if (crouch_keyU){
            Normalbc2D.enabled = true;
            Crouchbc2D.enabled = false;
            transform.localScale = new Vector3(1, 1, 1);
            runSpeed += 100;
            jumpHeight += new Vector2(0, 200);
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
        rb2d.AddForce(Vector2.right * runSpeed * horizontal);
    }
    void OnCollisionEnter2D(Collision2D obj)//Detectar si toca el suelo para reiniciar la cantidad de saltos
    {
        if (obj.collider.tag == "Wall")
        {
            jumpDone = 0;
        }

        
    }


}
