using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTankController : MonoBehaviour
{
    public Transform Head;
    private GameObject player;
    private float angle;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Hellbot");
    }
    // Update is called once per frame
    void Update()
    {

        Vector3 aimDirection = (player.transform.position - transform.position).normalized;
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Head.eulerAngles = new Vector3(0, 0, angle + 180);
    }
}
