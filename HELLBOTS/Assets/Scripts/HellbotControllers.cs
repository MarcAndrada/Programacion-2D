using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HellbotControllers : MonoBehaviour
{
    public BoxCollider2D Crouchbc2D;
    public BoxCollider2D Normalbc2D;
    public Vector2 jumpHeight;
    public float runSpeed;
    public GameObject Heart1, Heart2, Heart3;
    public GameObject EmptyHeart1, EmptyHeart2, EmptyHeart3;
    public int HP = 3;


    private int jumpDone;
    private Rigidbody2D rb2d;
    private int jumpLimit = 2;
    private float MaxSpeed;
    private Vector3 StarterPos;
    private SpriteRenderer sprite;
    private HellbotControllers Controlls;
    private HellbotAim Aim;

    private bool jump;
    private bool crouch_keyD;
    private bool crouch_keyU;
    private float horizontal;
    private bool heal;
    // Start is called before the first frame update
    void Start()
    {
        MaxSpeed = runSpeed;
        rb2d = GetComponent<Rigidbody2D>();
        jumpDone = 0;
        Crouchbc2D.enabled = false;
        sprite = GetComponent<SpriteRenderer>();
        Controlls = GetComponent<HellbotControllers>();
        Aim = GetComponent<HellbotAim>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = HellbotInput.Horizontal;
        jump = HellbotInput.Jump;
        crouch_keyD = HellbotInput.CrouchDown;
        crouch_keyU = HellbotInput.CrouchUp;
        heal = HellbotInput.Heal;

        

        //Salto
        if (jump)
        {
            rb2d.drag = 0f;
            if (jumpDone < jumpLimit)
            {
                if (jumpDone >= 1)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                }
                
                rb2d.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
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

        if (HP == 3)
        {
            Heart3.SetActive(true);
            EmptyHeart3.SetActive(false);
            Heart2.SetActive(true);
            EmptyHeart2.SetActive(false);
            Heart1.SetActive(true);
            EmptyHeart1.SetActive(false);
        }
        else if (HP == 2){
            Heart3.SetActive(false);
            EmptyHeart3.SetActive(true);
            Heart2.SetActive(true);
            EmptyHeart2.SetActive(false);
            Heart1.SetActive(true);
            EmptyHeart1.SetActive(false);
        }
        else if (HP == 1){
            Heart3.SetActive(false);
            EmptyHeart3.SetActive(true);
            Heart2.SetActive(false);
            EmptyHeart2.SetActive(true);
            Heart1.SetActive(true);
            EmptyHeart1.SetActive(false);
        }
        else if (HP == 0){
            Heart3.SetActive(false);
            EmptyHeart3.SetActive(true);
            Heart2.SetActive(false);
            EmptyHeart2.SetActive(true);
            Heart1.SetActive(false);
            EmptyHeart1.SetActive(true);
        }

        if (heal && Aim.Heal()){
            Aim.ResetWeapon();
            if (HP < 3)
            {
                HP++;
            }
            
        }

        if (HP <= 0) {
            //Hacer Animacion de muerte o para empezar SetActive(False)
            Aim.Dead();
            sprite.enabled = false;
            Controlls.enabled = false;
            Aim.enabled = false;
        }else{
            sprite.enabled = true;
            Controlls.enabled = true;
            Aim.enabled = true;
        }



    }
    void FixedUpdate()
    {
        //rb2d.velocity = new Vector2(horizontal * runSpeed, 0 );
        rb2d.AddForce(Vector2.right * runSpeed * horizontal);

    }
    void OnCollisionEnter2D(Collision2D collision)//Detectar si toca el suelo para reiniciar la cantidad de saltos
    {
        if (collision.collider.tag == "Floor")
        {
            jumpDone = 0;
            rb2d.drag = 3;
            runSpeed = MaxSpeed;
        }

        if (collision.collider.tag == "WallFloor")
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
            PlayerHit();
            Destroy(collision.gameObject);
        }
    }

    public void PlayerHit(){  HP--; }

}
