using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HellbotControllers : MonoBehaviour
{

    public BoxCollider2D Crouchbc2D;
    public BoxCollider2D Normalbc2D;
    public GameObject granadePrefab;
    public Transform GranadeLaunch;
    public GameObject DieText;
    [Header("Corazones Llenos")]
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    [Header("Corazones Mitad")]
    public GameObject M_Heart1;
    public GameObject M_Heart2;
    public GameObject M_Heart3;
    [Header("Corazones Vacios")]
    public GameObject EmptyHeart1;
    public GameObject EmptyHeart2;
    public GameObject EmptyHeart3;
    
    [Header("Audios")]
    public AudioClip WalkSound;
    public AudioClip JumpSound;
    public AudioClip pHit;
    public AudioClip eat;
    public AudioClip lowHP;



    private AudioSource audioSource;
    private SpriteRenderer sprite;
    private HellbotControllers Controlls;
    private HellbotAim Aim;
    private Rigidbody2D rb2d;
    private Animator animator;

    [Header("Config Player")]
    public bool GodModeOn;
    public Vector2 jumpHeight;
    public float runSpeed;
    public int HP = 3;
    public float granadeCD;
    public bool crouch = false;

    public int jumpDone;
    private int jumpLimit = 2;
    private float CurrentRunSpeed;  
    private float NormalG;
    private float WaitedTimeG;
    private float AnimDurationG = 800;
    private float TimePassedGCD;
    private bool godmode;
    private bool jump;
    private bool crouch_keyD;
    private bool crouch_keyU;
    private float horizontal;
    private float vertical;
    private bool heal;
    private bool granade;
    private bool throwGranade;
    private float footstep;
    private float footstepRithm = 375;
    private bool onFloor;
    private Vector3 lastpos;
    private Vector3 CheckpointPos;



    private enum DirectionV { NONE, UP, DOWN };
    private enum DirectionH { NONE, LEFT, RIGHT }
    private DirectionV GodDirectionV = DirectionV.NONE;
    private DirectionH GodDirectionH = DirectionH.NONE;
    private float speedV = 300;
    private float speedH = 3000;
    private float currentSpeedV;
    private float currentSpeedH;
    private float lowHPDuration = 1000f;
    private float TimePassed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Controlls = GetComponent<HellbotControllers>();
        Aim = GetComponent<HellbotAim>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        CheckpointPos = transform.position;

        GodModeOn = false;
        CurrentRunSpeed = runSpeed;
        jumpDone = 0;
        TimePassed = 0;
        Crouchbc2D.enabled = false;
        NormalG = rb2d.gravityScale;
        TimePassedGCD = granadeCD;
    }

    // Update is called once per frame
    void Update(){
        
        horizontal = HellbotInput.Horizontal;
        vertical = HellbotInput.Vertical;
        jump = HellbotInput.Jump;
        crouch_keyD = HellbotInput.CrouchDown;
        crouch_keyU = HellbotInput.CrouchUp;
        heal = HellbotInput.Heal;
        godmode = HellbotInput.GodMode;
        granade = HellbotInput.Granade;

        float delta = Time.deltaTime * 1000;




        if (godmode && !GodModeOn)
        {
            GodModeOn = true;
            Crouchbc2D.enabled = false;
            Normalbc2D.enabled = false;
            rb2d.gravityScale = 0;
        }
        else if (godmode && GodModeOn){
            GodModeOn = false;
            Crouchbc2D.enabled = false;
            Normalbc2D.enabled = true;
            rb2d.gravityScale = NormalG;
        }

        if (!GodModeOn){

            if (crouch)
            {
                if (onFloor)
                {
                    runSpeed = 2000;    
                }
                
            }
            //Salto
            if (jump && jumpDone < jumpLimit)
            {
                
                if (jumpDone >= 1)
                {
                    jumpHeight.y += 50;
                    if (horizontal == 0)
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, 100);

                    }
                    else if (horizontal == 1)
                    {
                        if (rb2d.velocity.x > 0)
                        {
                            rb2d.velocity = new Vector2(rb2d.velocity.x + 75, 0);
                        }
                        else
                        {
                            rb2d.velocity = new Vector2(rb2d.velocity.x + 600, 0);
                        }
                        


                    }
                    else
                    {
                        if (rb2d.velocity.x < 0)
                        {
                            rb2d.velocity = new Vector2(rb2d.velocity.x - 75 , 0);
                        }
                        else
                        {
                            rb2d.velocity = new Vector2(rb2d.velocity.x - 600, 0);
                        }
                    }
                }
                if (crouch)
                {
                    jumpDone = 3;
                }
                //hacer sonido de salto
                jumpDone++;
                audioSource.PlayOneShot(JumpSound);
                animator.SetBool("Jumping", true);
                rb2d.drag = 0f;
                

                rb2d.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                if (jumpDone == 2)
                {
                   jumpHeight.y -= 50;
                }

            }
            //Crouch (Si presiono "S", el collider grande se desactiva)
            if (crouch_keyD)
            {
                
                animator.SetBool("Crouch", true);
                Normalbc2D.enabled = false;
                Crouchbc2D.enabled = true;
                if (jumpDone < 1 && jumpHeight.y < 1000)
                {
                    jumpHeight += new Vector2(0, 700);
                    
                }
                if (onFloor)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, -150);
                }
                crouch = true;
            }
            else if (crouch_keyU)
            {

                if (onFloor)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 400);
                }
                animator.SetBool("Crouch", false);
                Normalbc2D.enabled = true;
                Crouchbc2D.enabled = false;
                if (jumpHeight.y > 1000)
                {
                    jumpHeight -= new Vector2(0, 700);
                    
                }
                
                crouch = false;
            }


            if (HP == 6){
                Heart3.SetActive(true);
                M_Heart3.SetActive(false);
                EmptyHeart3.SetActive(false);
                Heart2.SetActive(true);
                M_Heart2.SetActive(false);
                EmptyHeart2.SetActive(false);
                Heart1.SetActive(true);
                M_Heart1.SetActive(false);
                EmptyHeart1.SetActive(false);
            }
            else if (HP == 5){
                Heart3.SetActive(false);
                M_Heart3.SetActive(true);
                EmptyHeart3.SetActive(false);
                Heart2.SetActive(true);
                M_Heart2.SetActive(false);
                EmptyHeart2.SetActive(false);
                Heart1.SetActive(true);
                M_Heart1.SetActive(false);
                EmptyHeart1.SetActive(false);
            }
            else if (HP == 4){
                Heart3.SetActive(false);
                M_Heart3.SetActive(false);
                EmptyHeart3.SetActive(true);
                Heart2.SetActive(true);
                M_Heart2.SetActive(false);
                EmptyHeart2.SetActive(false);
                Heart1.SetActive(true);
                M_Heart1.SetActive(false);
                EmptyHeart1.SetActive(false);
            }
            else if (HP == 3){
                Heart3.SetActive(false);
                M_Heart3.SetActive(false);
                EmptyHeart3.SetActive(true);
                Heart2.SetActive(false);
                M_Heart2.SetActive(true);
                EmptyHeart2.SetActive(false);
                Heart1.SetActive(true);
                M_Heart1.SetActive(false);
                EmptyHeart1.SetActive(false);
            }
            else if (HP == 2){
                Heart3.SetActive(false);
                M_Heart3.SetActive(false);
                EmptyHeart3.SetActive(true);
                Heart2.SetActive(false);
                M_Heart2.SetActive(false);
                EmptyHeart2.SetActive(true);
                Heart1.SetActive(true);
                M_Heart1.SetActive(false);
                EmptyHeart1.SetActive(false);
                
            }
            else if (HP == 1){
                Heart3.SetActive(false);
                M_Heart3.SetActive(false);
                EmptyHeart3.SetActive(true);
                Heart2.SetActive(false);
                M_Heart2.SetActive(false);
                EmptyHeart2.SetActive(true);
                Heart1.SetActive(false);
                M_Heart1.SetActive(true);
                EmptyHeart1.SetActive(false);
            }
            else if (HP <= 0){
                Heart3.SetActive(false);
                M_Heart3.SetActive(false);
                EmptyHeart3.SetActive(true);
                Heart2.SetActive(false);
                M_Heart2.SetActive(false);
                EmptyHeart2.SetActive(true);
                Heart1.SetActive(false);
                M_Heart1.SetActive(false);
                EmptyHeart1.SetActive(true);
            }

            TimePassed += delta;

            if(HP < 3 && lowHPDuration < TimePassed)
            {
                audioSource.PlayOneShot(lowHP);
                TimePassed = 0;
            }
            if (heal && Aim.Heal())
            {
                //Hacer sonido de comer
                audioSource.PlayOneShot(eat);
                Aim.ResetWeapon();
                if (HP < 6)
                {
                    HP++;
                    HP++;
                }
                if (HP > 6)
                {
                    HP = 6;
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
                Cursor.visible = true;
            }
            else if(HP >=1 && Time.timeScale == 1)
            {
                sprite.enabled = true;
                Controlls.enabled = true;
                Aim.enabled = true;
                Cursor.visible = false;
            }

            TimePassedGCD += delta;

            if (granade && TimePassedGCD > granadeCD)
            {
                throwGranade = true;
                animator.SetTrigger("GThrow");
                
            }


            if (throwGranade)
            {
                WaitedTimeG += delta;

                if (WaitedTimeG >= AnimDurationG && TimePassedGCD > granadeCD)
                {
                    Instantiate(granadePrefab, GranadeLaunch.position, transform.rotation);
                    WaitedTimeG = 0;
                    throwGranade = false;
                    TimePassedGCD = 0;
                }
            }


            if (horizontal == 1 && onFloor || horizontal == -1 && onFloor) {
                footstep += delta;
                if (footstep > footstepRithm)
                {
                    audioSource.PlayOneShot(WalkSound);
                    footstep = 0;
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
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "WallFloor" || collision.gameObject.tag == "Weapon" || collision.gameObject.tag == "Ramp" || collision.gameObject.tag == "CheckPoint")
        {
            if (jumpDone > 0)
            {
                //hacer sonido de tocar suelo
            }
            animator.SetBool("Jumping", false);

            jumpDone = 0;
            rb2d.drag = 3;
            runSpeed = CurrentRunSpeed;
            onFloor = true;
        }


       
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "WallFloor" || collision.gameObject.tag == "Weapon" || collision.gameObject.tag == "Ramp" || collision.gameObject.tag == "CheckPoint")
        {
            
            animator.SetBool("Jumping", false);         
            rb2d.drag = 3;
            if (collision.gameObject.tag == "Ramp" || collision.gameObject.tag == "CheckPoint")
            {
                runSpeed = CurrentRunSpeed + 400;
                if (collision.gameObject.tag == "CheckPoint")
                {
                   runSpeed = CurrentRunSpeed + 1500;
                }
            }
            else
            {
                runSpeed = CurrentRunSpeed;
            }
            
            onFloor = true;
        }

        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "WallFloor" || collision.gameObject.tag == "Weapon" || collision.gameObject.tag == "Ramp" || collision.gameObject.tag == "CheckPoint")
        {
            if (collision.gameObject.tag != "CheckPoint")
            {
                animator.SetBool("Jumping", true);
            }
            runSpeed = CurrentRunSpeed - 2600;
            rb2d.drag = 0f;
            onFloor = false;
        }

        if (collision.gameObject.tag == "Floor")
        {
            //coger posicion para checkpont
            lastpos = transform.position;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemybullet"))
        {
            PlayerHit();
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(pHit);
        }

        if (collision.gameObject.tag == "Explosion")
        {
            PlayerHit();
            audioSource.PlayOneShot(pHit);
        }

        if (collision.gameObject.tag == "Caida")
        {
            PlayerHit();
            audioSource.PlayOneShot(pHit);
            ReturnLastJump();
        }

        if (collision.gameObject.tag == "CheckPoint")
        {
            CheckpointPos = transform.position;
        }

    }

    public void PlayerHit(){
        //Hacer sonido de Daño
        if (!GodModeOn){
            HP--;
        }

    }


    public void ReturnLastJump()
    {
        //volver a la posicion del checkpoint
        transform.position = lastpos;
    }

    public void returnLastCheckPoint()
    {
        transform.position = CheckpointPos;
        HP = 4;
        DieText.SetActive(false);
        sprite.enabled = true;
        Controlls.enabled = true;
        Aim.enabled = true;
        Cursor.visible = false;
    }

}
