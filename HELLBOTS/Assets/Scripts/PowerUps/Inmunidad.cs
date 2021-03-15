using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Renderer rend;
    Color c;
    public float InvulnerabilidadTimer;//Tiempo de duracion del powerup (SpeedBost)
    private bool InvulnerableBool;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        c = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (InvulnerableBool)
        {
            InvulnerabilidadTimer += Time.deltaTime;
            if (InvulnerabilidadTimer >= 10)//Tiempo de invulnerabilidad
            {
                InvulnerabilidadTimer = 0;
                InvulnerableBool = false;
                c.a = 1f;//opacidad del player
                rend.material.color = c;
            }
        }
        if (Input.GetKey(KeyCode.F))
        {
            Physics2D.IgnoreLayerCollision(9, 10, true);
            InvulnerableBool = true;
            c.a = 0.5f;//reduce a la mitad opacidad del player
            rend.material.color = c;

        }
    }
}
