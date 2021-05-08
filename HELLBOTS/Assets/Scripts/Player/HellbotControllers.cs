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
    public GameObject BossHPBar;
    public GameObject GranadeAnim;
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
    [Header("Particulas")]
    public GameObject Particulas;
    public GameObject HealParticles;
    public GameObject BlueHealParticles;
    public Transform barraHP;
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
    private BoxCollider2D box2d;
    private Image BombOpacityCont;


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
    private bool damaged = false;
    private float TimeSinceDmg;
    private float immortalTime = 1000;
    private float changeSprite = 150;
    private float alpha = 0;
    private float timetoPassAlpha = 100;
    private float timePassedAlpha = 0;
    private bool InvertedGravity = false;
    private bool GravityIsInverted;
    private string CurrentScene;


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
    private float TimeToWaitForHeal = 450;
    private float TimeHealWaited = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Controlls = GetComponent<HellbotControllers>();
        Aim = GetComponent<HellbotAim>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        BombOpacityCont = GranadeAnim.GetComponent<Image>();

        CurrentScene = SceneManager.GetActiveScene().name;

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




        if (godmode && !GodModeOn)
        {
            GodModeOn = true;
            Crouchbc2D.enabled = false;
            Normalbc2D.enabled = true;
            rb2d.gravityScale = 0;
        }
        else if (godmode && GodModeOn)
        {
            GodModeOn = false;
            Crouchbc2D.enabled = false;
            Normalbc2D.enabled = true;
            if (!InvertedGravity)
            {
                rb2d.gravityScale = NormalG;
            }
            else
            {
                rb2d.gravityScale = -NormalG;
            }
        }

        if (!GodModeOn)
        {
            
            if (!InvertedGravity)
            {


                if (crouch)
                {
                    if (onFloor)
                    {
                        runSpeed = 2000;
                    }
                    box2d = Crouchbc2D;
                }
                else
                {
                    box2d = Normalbc2D;
                }
                //Salto
                if (jump && jumpDone < jumpLimit)
                {
                    if (!onFloor)
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    }
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
                                rb2d.velocity = new Vector2(rb2d.velocity.x - 75, 0);
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
                    bool GoDown;
                    if (onFloor)
                    {
                        GoDown = true;
                    }
                    else
                    {
                        GoDown = false;
                    }
                    animator.SetBool("Crouch", true);
                    Normalbc2D.enabled = false;
                    Crouchbc2D.enabled = true;
                    if (jumpDone < 1 && jumpHeight.y < 1000)
                    {
                        jumpHeight += new Vector2(0, 700);

                    }
                    if (GoDown)
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, -950);
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

            }
            else
            {
                if (crouch)
                {
                    if (onFloor)
                    {
                        runSpeed = 2000;
                    }
                    box2d = Crouchbc2D;
                }
                else
                {
                    box2d = Normalbc2D;
                }
                //Salto
                if (jump && jumpDone < jumpLimit)
                {

                    if (jumpDone >= 1)
                    {
                        jumpHeight.y -= 50;
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
                                rb2d.velocity = new Vector2(rb2d.velocity.x - 75, 0);
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


                    rb2d.AddForce(Vector2.up * -jumpHeight, ForceMode2D.Impulse);
                    if (jumpDone == 2)
                    {
                        jumpHeight.y += 50;
                    }

                }
                //Crouch (Si presiono "S", el collider grande se desactiva)
                if (crouch_keyD)
                {
                    bool GoDown;
                    if (onFloor)
                    {
                        GoDown = true;
                    }
                    else
                    {
                        GoDown = false;
                    }
                    animator.SetBool("Crouch", true);
                    Normalbc2D.enabled = false;
                    Crouchbc2D.enabled = true;
                    if (jumpDone < 1 && jumpHeight.y < 1000)
                    {
                        jumpHeight += new Vector2(0, 700);

                    }
                    if (GoDown)
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, 1000);
                    }
                    crouch = true;
                }
                else if (crouch_keyU)
                {

                    if (onFloor)
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, -400);
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
            }

            if (HP == 6)
            {
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
            else if (HP == 5)
            {
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
            else if (HP == 4)
            {
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
            else if (HP == 3)
            {
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
            else if (HP == 2)
            {
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
            else if (HP == 1)
            {
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
            else if (HP <= 0)
            {
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

            if (HP < 3 && lowHPDuration < TimePassed)
            {
                audioSource.PlayOneShot(lowHP);
                TimePassed = 0;
            }
            if (heal && Aim.Heal())
            {
                //Hacer sonido de comer
                audioSource.PlayOneShot(eat);
                Aim.ResetWeapon();
                healing();
                healing();

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
            else if (HP >= 1 && Time.timeScale == 1)
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
                if (WaitedTimeG == 0)
                {
                    alpha = 1;
                }
            }

            timePassedAlpha += delta;
            if (timePassedAlpha > timetoPassAlpha)
            {
                alpha -= 0.01f;
                timePassedAlpha = 0;

            }

            BombOpacityCont.color = new Color(BombOpacityCont.color.r, BombOpacityCont.color.g, BombOpacityCont.color.b, alpha);

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


            if (horizontal == 1 && onFloor || horizontal == -1 && onFloor)
            {
                footstep += delta;
                if (footstep > footstepRithm)
                {
                    audioSource.PlayOneShot(WalkSound);
                    footstep = 0;
                }

            }

            if (damaged)
            {
                TimeSinceDmg += delta;
                sprite.color = Color.red;
                if (TimeSinceDmg > changeSprite)
                {
                    sprite.color = Color.white;
                }

                if (TimeSinceDmg > immortalTime)
                {
                    damaged = false;
                    TimeSinceDmg = 0;
                }
            }


        }
        else
        {


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
    void FixedUpdate()
    {

        if (!GodModeOn)
        {
            //rb2d.velocity = new Vector2(horizontal * runSpeed, 0 );
            rb2d.AddForce(Vector2.right * runSpeed * horizontal);

            if (horizontal == 1 || horizontal == -1)
            {
                //Hacer sonidos de andar
                animator.SetBool("Walking", true);
                if (!onFloor)
                {
                    if (horizontal == -1 && rb2d.velocity.x > 400)
                    {
                        rb2d.velocity = new Vector2(400, rb2d.velocity.y);
                    }
                    else if (horizontal == 1 && rb2d.velocity.x < -400)
                    {
                        rb2d.velocity = new Vector2(-400, rb2d.velocity.y);
                    }

                    if (horizontal == 1)
                    {
                        if (rb2d.velocity.x < 600)
                        {
                            runSpeed = CurrentRunSpeed - 1000;
                        }
                        else
                        {
                            runSpeed = CurrentRunSpeed - 2600;

                        }
                    }
                    else if (horizontal == -1)
                    {
                        if (rb2d.velocity.x > -600)
                        {
                            runSpeed = CurrentRunSpeed - 1000;

                        }
                        else
                        {
                            runSpeed = CurrentRunSpeed - 2600;
                        }
                    }
                }


            }
            else if (horizontal == 0)
            {
                animator.SetBool("Walking", false);
            }

        }
        else
        {
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

    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 8 || collision.gameObject.tag == "Weapon")
        {
            if (!onFloor)
            {

                int RayRange = 10;

                bool col1 = false;
                bool col2 = false;
                bool col3 = false;
                float center_x = (box2d.bounds.min.x + box2d.bounds.max.x) / 2;


                if (!InvertedGravity)
                {
                    Vector2 centerPosition = new Vector2(center_x, box2d.bounds.min.y);
                    Vector2 leftPosition = new Vector2(box2d.bounds.min.x, box2d.bounds.min.y);
                    Vector2 RightPosition = new Vector2(box2d.bounds.max.x, box2d.bounds.min.y);
                    RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, -Vector2.up, RayRange);
                    if (checkRaycastWithScenario(hits)) { col1 = true; }

                    hits = Physics2D.RaycastAll(leftPosition, -Vector2.up, RayRange);
                    if (checkRaycastWithScenario(hits)) { col2 = true; }

                    hits = Physics2D.RaycastAll(RightPosition, -Vector2.up, RayRange);
                    if (checkRaycastWithScenario(hits)) { col3 = true; }
                }
                else
                {
                    Vector2 centerPosition = new Vector2(center_x, box2d.bounds.max.y);
                    Vector2 leftPosition = new Vector2(box2d.bounds.min.x, box2d.bounds.max.y);
                    Vector2 RightPosition = new Vector2(box2d.bounds.max.x, box2d.bounds.max.y);
                    RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, Vector2.up, RayRange);
                    if (checkRaycastWithScenario(hits)) { col1 = true; }

                    hits = Physics2D.RaycastAll(leftPosition, Vector2.up, RayRange);
                    if (checkRaycastWithScenario(hits)) { col2 = true; }

                    hits = Physics2D.RaycastAll(RightPosition, Vector2.up, RayRange);
                    if (checkRaycastWithScenario(hits)) { col3 = true; }
                }


                if (col1 || col2 || col3)
                {

                    animator.SetBool("Jumping", false);
                    rb2d.drag = 3;
                    jumpDone = 0;

                    if (collision.gameObject.tag == "Ramp")
                    {
                        runSpeed = CurrentRunSpeed + 500;

                    }
                    else
                    {
                        runSpeed = CurrentRunSpeed;
                    }

                    if (collision.gameObject.tag == "Floor")
                    {
                        //coger posicion para checkpont
                        if (CurrentScene != "Map3")
                        {
                            lastpos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

                        }
                        else
                        {
                            lastpos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                        }
                        GravityIsInverted = InvertedGravity;
                    }


                    onFloor = true;

                }
            }

        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            if (onFloor)
            {
                if (CurrentScene != "Map3")
                {
                    lastpos = transform.position;

                }
                else
                {
                    lastpos = transform.position;
                }
                GravityIsInverted = InvertedGravity;
            }
            //coger posicion para checkpont
            
        }

        if (collision.gameObject.layer == 8 || collision.gameObject.tag == "Weapon")
        {
            if (collision.gameObject.tag != "CheckPoint")
            {
                animator.SetBool("Jumping", true);
            }
            rb2d.drag = 0f;
            onFloor = false;
        }

       



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemybullet")
        {
            PlayerHit();
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.tag == "Explosion")
        {
            PlayerHit();
        }

        if (collision.gameObject.tag == "SkeletonHand")
        {
            PlayerHit();
        }

        if (collision.gameObject.tag == "Caida" && !godmode)
        {
            PlayerHit();
            ReturnLastJump();
        }

        if (collision.gameObject.tag == "CheckPoint")
        {
            CheckpointPos = transform.position;
        }

        if (collision.gameObject.tag == "BossActivator")
        {
            BossHPBar.SetActive(true);
        }

        if (collision.gameObject.tag == "GravityUp")
        {
            GravityUp();
        }

        if (collision.gameObject.tag == "GravityDown")
        {
            GravityDown();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            float delta = Time.deltaTime * 1000;


            TimeHealWaited += delta;

            if (TimeHealWaited > TimeToWaitForHeal && HP < 4)
            {
                healing();
                TimeHealWaited = 0;

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            TimeHealWaited = 0;
        }
    }

    public void PlayerHit()
    {
        //Hacer sonido de Daño
        if (!GodModeOn && !damaged)
        {
            damaged = true;
            HP--;
            Instantiate(Particulas, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(pHit);
        }

    }


    public void ReturnLastJump()
    {
        int RayRange = 500;

        bool col1 = false;
        if (!InvertedGravity)
        {
            Vector2 centerPosition = new Vector2(lastpos.x, lastpos.y);
           
            RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, -Vector2.up, RayRange);
            if (checkRaycastWithScenarioFloor(hits)) { col1 = true; }
            
        }
        else
        {
            Vector2 centerPosition = new Vector2(lastpos.x, lastpos.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, Vector2.up, RayRange);
            if (checkRaycastWithScenarioFloor(hits)) { col1 = true; }

        }

        if (!col1)
        {
            if (CurrentScene != "Map3")
            {
                if (transform.position.x >= lastpos.x)
                {
                    lastpos = new Vector3(lastpos.x - 200, lastpos.y, lastpos.z);

                }else{
                    lastpos = new Vector3(lastpos.x + 200, lastpos.y, lastpos.z);
                }
            }
            else
            {
                if (transform.position.x <= lastpos.x)
                {
                    lastpos = new Vector3(lastpos.x + 200, lastpos.y, lastpos.z);

                }
                else
                {
                    lastpos = new Vector3(lastpos.x - 200, lastpos.y, lastpos.z);
                }

            }
        }
        //volver a la posicion del checkpoint
        if (GravityIsInverted)
        {
            GravityUp();
        }
        else
        {
            GravityDown();
        }

        transform.position = lastpos;
        rb2d.velocity = new Vector2(0, 0);
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
        DoorCloseController.OpenDoor();
        if (BossHPBar != null)
        {
            BossHPBar.SetActive(false);
        }

    }


    private bool checkRaycastWithScenario(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 8 || hit.collider.gameObject.tag == "Weapon")
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool checkRaycastWithScenarioFloor(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Floor")
                {
                    return true;
                }
            }
        }
        return false;
    }


    private void healing()
    {
        if (HP < 4)
        {
            Instantiate(HealParticles, new Vector3(barraHP.position.x + 100, barraHP.position.y, barraHP.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(BlueHealParticles, new Vector3(barraHP.position.x + 100, barraHP.position.y, barraHP.position.z), Quaternion.identity);

        }
        if (HP < 6)
        {
            HP++;
        }
        if (HP > 6)
        {
            HP = 6;
        }
    }

    private void GravityDown()
    {
        rb2d.gravityScale = NormalG;
        if (transform.localScale.y < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        }
        InvertedGravity = false;
    }


    private void GravityUp()
    {
        rb2d.gravityScale = -NormalG;
        if (transform.localScale.y > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        }
        InvertedGravity = true;
    }


}
