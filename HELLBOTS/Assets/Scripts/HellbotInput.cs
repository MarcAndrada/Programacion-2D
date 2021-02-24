using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellbotInput : MonoBehaviour
{
    public static float Vertical{
        get { return Input.GetAxis("Vertical"); }
    }
    /* Para llamarlo simplemente es crear una variable e igualarlo al valor que devuelven las funciones
     *  private float vertical;
     *  vertical = HellbotInput.Vertical;
     *  y cuando se quiera usar la variable se utilizara solo "veritcal"
     */
    public static float Horizontal
    {
        get { return Input.GetAxis("Horizontal"); }
    }


    public static bool Crouch
    {
        get { return Input.GetKey(KeyCode.S); }
    }
    /*Aqui seria igual que en el de vertical solo que la variable ha de ser un booleano
     *  private bool crouch
     *  crouch = HellbotInput.Crouch
     */
    public static bool Interact
    {
        get { return Input.GetKey(KeyCode.E); }
    }

    public static bool Heal
    {
        get { return Input.GetKey(KeyCode.Q); }
    }

    public static bool Jump
    {
        get { return Input.GetKey(KeyCode.Space); }
    }

    public static bool Melee
    {
        get { return Input.GetMouseButton(1); }
        //esto es el click izquierdo
    }

    public static bool Shoot
    {
        get { return Input.GetMouseButton(0); }
        //esto es el click derecho
    }

    public static bool PowerUp
    {
        get { return Input.GetKey(KeyCode.F); }
    }

}
