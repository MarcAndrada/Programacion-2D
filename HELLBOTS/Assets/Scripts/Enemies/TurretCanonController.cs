using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCanonController : MonoBehaviour
{
    public Transform Canon;
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
        Canon.eulerAngles = new Vector3(0, 0, angle -90);
    }
}
