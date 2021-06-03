using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellbotInput : MonoBehaviour
{
    public static float Vertical{
        get { return Input.GetAxisRaw("Vertical"); }
    }
    /* Para llamarlo simplemente es crear una variable e igualarlo al valor que devuelven las funciones
     *  private float vertical;
     *  vertical = HellbotInput.Vertical;(esto en el update)
     *  y cuando se quiera usar la variable se utilizara solo "veritcal"
     */
    public static float Horizontal
    {
        get { return Input.GetAxisRaw("Horizontal"); }
    }


    public static bool CrouchDown
    {
        get { return Input.GetKeyDown(KeyCode.S); }
    }

    public static bool CrouchUp
    {
        get { return Input.GetKeyUp(KeyCode.S); }
    }
    /*Aqui seria igual que en el de vertical solo que la variable ha de ser un booleano
     *  private bool crouch
     *  crouch = HellbotInput.Crouch
     */
    public static bool Interact
    {
        get { return Input.GetKeyDown(KeyCode.E); }
    }

    public static bool Heal
    {
        get { return Input.GetKeyDown(KeyCode.Q); }
    }

    public static bool Jump
    {
        get { return Input.GetKeyDown(KeyCode.Space); }
    }

    public static bool Granade
    {
        get { return Input.GetKeyDown(KeyCode.G); }
        
    }

    //esto es el click izquierdo
    public static bool Shoot
    {
        get { return Input.GetMouseButton(0); }
        
    }

    public static bool GodMode
    {
        get { return Input.GetKeyDown(KeyCode.V);  }
    }

    public static bool Menu
    {
        get { return Input.GetKeyDown(KeyCode.Escape);  }
    }

    public static bool GoLevel1
    {
        get { return Input.GetKeyDown(KeyCode.Alpha1); }
    }

    public static bool GoLevel2
    {
        get { return Input.GetKeyDown(KeyCode.Alpha2); }
    }

    public static bool GoLevel3
    {
        get { return Input.GetKeyDown(KeyCode.Alpha3); }
    }

    public static bool GoLevel4
    {
        get { return Input.GetKeyDown(KeyCode.Alpha4); }
    }
}
