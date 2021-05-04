using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyActivator : MonoBehaviour
{
    private grunt_movement GruntController;
    private newSuicideScript SuicideController;
    private turretScrip turretController;
    private tank_controller tankController;
    private flying_enemy flyerController;
    private boss_movement SkullController;

    private SpriteRenderer Sprite;
    private Animator animator;

    private enum Enemy_Type { GRUNT, SUICIDE, TURRET, TANK, FLYER, SKULL};
    private Enemy_Type CurrentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        GruntController = GetComponent<grunt_movement>();
        SuicideController = GetComponent<newSuicideScript>();
        turretController = GetComponent<turretScrip>();
        tankController = GetComponent<tank_controller>();
        flyerController = GetComponent<flying_enemy>();
        SkullController = GetComponent<boss_movement>();

        if (GruntController != null)
        {
            CurrentEnemy = Enemy_Type.GRUNT;
        }
        else if (SuicideController != null)
        {
            CurrentEnemy = Enemy_Type.SUICIDE;
        }
        else if (turretController != null)
        {
            CurrentEnemy = Enemy_Type.TURRET;
        }
        else if (tankController != null)
        {
            CurrentEnemy = Enemy_Type.TANK;
        }
        else if (flyerController != null)
        {
            CurrentEnemy = Enemy_Type.FLYER;
        }
        else if (SkullController != null)
        {
            CurrentEnemy = Enemy_Type.SKULL;
        }

        switch (CurrentEnemy)
        {
            case Enemy_Type.GRUNT:
                animator = GetComponent<Animator>();
                break;
            case Enemy_Type.SUICIDE:
                animator = GetComponent<Animator>();
                break;
            case Enemy_Type.TURRET:
                break;
            case Enemy_Type.TANK:
                break;
            case Enemy_Type.FLYER:
                break;
            case Enemy_Type.SKULL:
                break;
            default:
                break;
        }

        Sprite = GetComponent<SpriteRenderer>();


        Sprite.enabled = false;
    }

    public void ActivateEnemy(){

        switch (CurrentEnemy)
        {
            case Enemy_Type.GRUNT:
                Sprite.enabled = true;
                animator.enabled = true;
                GruntController.enabled = true;
                break;
            case Enemy_Type.SUICIDE:
                Sprite.enabled = true;
                animator.enabled = true;
                SuicideController.enabled = true;
                break;
            case Enemy_Type.TURRET:
                Sprite.enabled = true;
                turretController.enabled = true;
                break;
            case Enemy_Type.TANK:
                Sprite.enabled = true;
                tankController.enabled = true;
                break;
            case Enemy_Type.FLYER:
                Sprite.enabled = true;
                flyerController.enabled = true;
                break;
            case Enemy_Type.SKULL:
                Sprite.enabled = true;
                SkullController.enabled = true;
                break;
            default:
                break;
        }

    }


    public void DefuseEnemy(){
        switch (CurrentEnemy)
        {
            case Enemy_Type.GRUNT:
                Sprite.enabled = false;
                animator.enabled = false;
                GruntController.enabled = false;
                break;
            case Enemy_Type.SUICIDE:
                Sprite.enabled = false;
                animator.enabled = false;
                SuicideController.enabled = false;
                break;
            case Enemy_Type.TURRET:
                Sprite.enabled = false;
                turretController.enabled = false;
                break;
            case Enemy_Type.TANK:
                Sprite.enabled = false;
                tankController.enabled = false;
                break;
            case Enemy_Type.FLYER:
                Sprite.enabled = false;
                flyerController.enabled = false;
                break;
            case Enemy_Type.SKULL:
                Sprite.enabled = false;
                SkullController.enabled = false;
                break;
            default:
                break;
        }
    }
}
