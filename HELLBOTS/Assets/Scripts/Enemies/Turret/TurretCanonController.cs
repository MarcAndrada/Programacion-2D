using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCanonController : MonoBehaviour
{
    public Transform Canon;
    public Transform Turret;
    private GameObject player;
    private float angle;

    private void Start(){
        player = GameObject.FindGameObjectWithTag("Hellbot");
    }
    // Update is called once per frame
    void Update()
    {


        if (Turret.localRotation.z == 0)
        {
            if (player.transform.position.y < transform.position.y)
            {
                Vector3 aimDirection = (player.transform.position - transform.position).normalized;
                angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                Canon.eulerAngles = new Vector3(0, 0, angle + 90);
            }
            else
            {
                angle = 0;
                Canon.eulerAngles = new Vector3(0, 0, angle);

            }
        }
        else if (Turret.localRotation.z > 0 && Turret.transform.localRotation.z < 1)
        {
            if (player.transform.position.x > transform.position.x)
            {
                Vector3 aimDirection = (player.transform.position - transform.position).normalized;
                angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                Canon.eulerAngles = new Vector3(0, 0, angle + 90);
            }
            else
            {
                angle = 0;
                Canon.eulerAngles = new Vector3(0, 0, angle + 90);

            }
        }
        else if (Turret.localRotation.z < 0)
        {
            if (player.transform.position.x < transform.position.x)
            {
                Vector3 aimDirection = (player.transform.position - transform.position).normalized;
                angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                Canon.eulerAngles = new Vector3(0, 0, angle + 90);
            }
            else
            {
                angle = 0;
                Canon.eulerAngles = new Vector3(0, 0, angle - 90);

            }
        }
        else if (Turret.localRotation.z == 1)
        {
            if (player.transform.position.y > transform.position.y)
            {
                Vector3 aimDirection = (player.transform.position - transform.position).normalized;
                angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                Canon.eulerAngles = new Vector3(0, 0, angle + 90);
            }
            else
            {
                angle = 0;
                Canon.eulerAngles = new Vector3(0, 0, angle - 90);

            }


        }
    }
}
