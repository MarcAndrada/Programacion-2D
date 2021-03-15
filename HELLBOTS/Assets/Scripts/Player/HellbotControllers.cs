using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HellbotControllers : MonoBehaviour
{

    public BoxCollider2D Crouchbc2D;
    public BoxCollider2D Normalbc2D;
    public GameObject Heart1, Heart2, Heart3;
    public GameObject EmptyHeart1, EmptyHeart2, EmptyHeart3;
    public GameObject granadePrefab;
    public Transform GranadeLaunch;
    public GameObject DieText;

    private SpriteRenderer sprite;
    private HellbotControllers Controlls;
    private HellbotAim Aim;
    private Rigidbody2D rb2d;
    private Animator animator;

    public bool GodModeOn;
    public Vector2 jumpHeight;
    public float runSpeed;
    public int HP = 3;
    public float bostTimer;
    private bool speedBosting;

    
    private int jumpDone;
    private int jumpLimit = 2;
    private float MaxSpeed;  
    private float NormalG;
    private float WaitedTimeG;
    private float AnimDurationG = 350;

    private bool godmode;
    private bool jump;
    private bool crouch_keyD;
    private bool crouch_keyU;
    private float horizontal;
    private float vertical;
    private bool heal;
    private bool granade;
    private bool throwGranade;


    private enum DirectionV { NONE, UP, DOWN };
    private enum DirectionH { NONE, LEFT, RIGHT }
    private DirectionV GodDirectionV = DirectionV.NONE;
    private DirectionH GodDirectionH = DirectionH.NONE;
    private float speedV = 300;
    private float speedH = 3000;
    private float currentSpeedV;
    private float currentSpeedH;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Controlls = GetComponent<HellbotControllers>();
        Aim = GetComponent<HellbotAim>();
        animator = GetComponent<Animator>();

        GodModeOn = false;
        MaxSpeed = runSpeed;
        jumpDone = 0;
        Crouchbc2D.enabled = false;
        NormalG = rb2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        horizontal = HellbotInput.Horizontal;
        vertical = HellbotInput.Vertical;
        jump = HellbotInput.Jump;
        crouch_keyD = HellbotInput.CrouchDown;
        crouch_keyU = HellbotInput.CrouchUp;
        heal = HellbotInput.Heal;
        godmode = HellbotInput.GodMode;
        granade = HellbotInput.Granade;

        float delta = Time.deltaTime * 1000;

        if (speedBosting) {
            bostTimer += Time.deltaTime;
            if (bostTimer >= 10)
            {
                MaxSpeed = 3000;
                runSpeed = 3000;
                bostTimer = 0;
                speedBosting = false;
            }
        }
        if (Input.GetKey(KeyCode.F)) {
            speedBosting = true;
            MaxSpeed = 6000;
            runSpeed = 6000;
        }

        if (godmode && !GodModeOn)
        {
            GodModeOn = true;
            Crouchbc2D.enabled = false;
            Normalbc2D.enabled = false;
            rb2d.gravityScale = 0;
        }else if (godmode && GodModeOn){
            GodModeOn = false;
            Crouchbc2D.enabled = false;
            Normalbc2D.enabled = true;
            rb2d.gravityScale = NormalG;
        }

        if (!GodModeOn){

            //Salto
            if (jump && jumpDone < jumpLimit)
            {
                
                   //hacer sonido de salto
                animator.SetBool("Jumping", true);
                rb2d.drag = 0f;
                if (jumpDone >= 1)
                {
                    animator.SetBool("DoubleJumping", true);
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                }

                rb2d.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                jumpDone++;

             
                
            }
            //Crouch (Si presiono "S", el collider grande se desactiva)
            if (crouch_keyD)
            {
                animator.SetBool("Crouch", true);
                Normalbc2D.enabled = false;
                Crouchbc2D.enabled = true;
                runSpeed -= 100;
                jumpHeight -= new Vector2(0, 200);
            }
            else if (crouch_keyU)
            {
                animator.SetBool("Crouch", false);
                Normalbc2D.enabled = true;
                Crouchbc2D.enabled = false;
                runSpeed += 100;
                jumpHeight += new Vector2(0, 200);
            }

            if (granade)
            {
                throwGranade = true;
                animator.SetTrigger("GThrow");
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
            else if (HP == 2)
            {
                Heart3.SetActive(false);
                EmptyHeart3.SetActive(true);
                Heart2.SetActive(true);
                EmptyHeart2.SetActive(false);
                Heart1.SetActive(true);
                EmptyHeart1.SetActive(false);
            }
            else if (HP == 1)
            {
                Heart3.SetActive(false);
                EmptyHeart3.SetActive(true);
                Heart2.SetActive(false);
                EmptyHeart2.SetActive(true);
                Heart1.SetActive(true);
                EmptyHeart1.SetActive(false);
            }
            else if (HP <= 0)
            {
                Heart3.SetActive(false);
                EmptyHeart3.SetActive(true);
                Heart2.SetActive(false);
                EmptyHeart2.SetActive(true);
                Heart1.SetActive(false);
                EmptyHeart1.SetActive(true);
            }

            if (heal && Aim.Heal())
            {
                //Hacer sonido de comer
                
                if (HP < 3)
                {
                    Aim.ResetWeapon();
                    HP++;
                }

            }

            if (HP <= 0)
            {
                //Hacer Animacion de muerte o para empezar SetActive(False) y hacer sonido de muerte
                DieText.SetActive(true);
                Aim.Dead();
                sprite.enabled = false;
                Controlls.enabled = false;
                Aim.enabled = false;
            }else{
                sprite.enabled = true;
                Controlls.enabled = true;
                Aim.enabled = true;
            }

           
            if (throwGranade)
            {
                WaitedTimeG += delta;
                

                if (WaitedTimeG >= AnimDurationG)
                {
                    Instantiate(granadePrefab, GranadeLaunch.position, transform.rotation);
                    WaitedTimeG = 0;
                    throwGranade = false;
                }
            }

        }
        else{
            
            
            GodDirectionV = DirectionV.NONE;
            GodDirectionH = DirectionH.NONE;

            if (vertical > 0)
            {
                GodDirectionV = DirectionV.UP;
            }
            else if (vertical < 0)
            {
                GodDirectionV = DirectionV.DOWN;
            }

            if (horizontal < 0)
            {
                GodDirectionH = DirectionH.LEFT;
            }
            else if (horizontal > 0)
            {
                GodDirectionH = DirectionH.RIGHT;
            }
        }



    }
    void FixedUpdate(){

        if (!GodModeOn)
        {
            //rb2d.velocity = new Vector2(horizontal * runSpeed, 0 );
            rb2d.AddForce(Vector2.right * runSpeed * horizontal);
            if (horizontal == 1 || horizontal == -1)
            {
                //Hacer sonidos de andar
                animator.SetBool("Walking", true);
            }
            else if (horizontal == 0)
            {
                animator.SetBool("Walking", false);
            }
        }else{
            float delta = Time.fixedDeltaTime * 1000;
            currentSpeedV = 0;
            currentSpeedH = 0;

            switch (GodDirectionV)
            {
                default:
                    break;
                case DirectionV.UP:
                    currentSpeedV = speedV;

                    break;
                case DirectionV.DOWN:
                    currentSpeedV = -speedV;
                    break;
            }

            switch (GodDirectionH)
            {
                default:
                    break;
                case DirectionH.LEFT:
                    currentSpeedH = -speedH;

                    break;
                case DirectionH.RIGHT:
                    currentSpeedH = speedH;

                    break;

            }
            rb2d.velocity = new Vector2(currentSpeedH, currentSpeedV * delta);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)//Detectar si toca el suelo para reiniciar la cantidad de saltos
    {
        if (collision.collider.tag == "Floor"){
            if (jumpDone > 0)
            {
                //hacer sonido de tocar suelo
            }
            animator.SetBool("Jumping", false);
            animator.SetBool("DoubleJumping", false);

            jumpDone = 0;
            rb2d.drag = 3;
            runSpeed = MaxSpeed;
        }

        if (collision.collider.tag == "WallFloor"){
            if (jumpDone > 0)
            {
                //hacer sonido de tocar suelo
            }
            animator.SetBool("Jumping", false);
            animator.SetBool("DoubleJumping", false);
            jumpDone = 0;
            rb2d.drag = 3;
            runSpeed = MaxSpeed;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            PlayerHit();
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor"){
            animator.SetBool("Jumping", true);
            runSpeed = 200;
            rb2d.drag = 0f;
        }

        if (collision.collider.tag == "WallFloor"){
            animator.SetBool("Jumping", true);
            runSpeed = 200;
            rb2d.drag = 0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemybullet"))
        {
            PlayerHit();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Explosion")
        {
            PlayerHit();
        }

        if (collision.gameObject.tag == "Caida")
        {
            PlayerHit();
            PlayerHit();
            PlayerHit();
        }

     
    }

    public void PlayerHit(){
        //Hacer sonido de Daño
        if (!GodModeOn){
            HP--;
        }

    }

}
