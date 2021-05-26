using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Vector3 TargetPos;
    public float speed;

    private Vector3 StarterPos;
    private bool goingUp = false;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StarterPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!goingUp)
        {
            if (transform.position.y > StarterPos.y)
            {
                rb2d.velocity = new Vector2(0,-speed);
            }
            else
            {
                rb2d.velocity = new Vector2(0,0);

            }
        }
        else
        {
            if (transform.position.y < TargetPos.y)
            {
                rb2d.velocity = new Vector2(0,speed);
            }
            else
            {
                rb2d.velocity = new Vector2(0,0);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hellbot")
        {
            goingUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hellbot"){
            goingUp = false;
        }
    }
}
