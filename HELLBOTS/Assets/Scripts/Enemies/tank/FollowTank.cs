using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTank : MonoBehaviour
{

    public GameObject Tank;

    // Update is called once per frame
    void Update()
    {
        transform.position = Tank.transform.position;
    }
}
