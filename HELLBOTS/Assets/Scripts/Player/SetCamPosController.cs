using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamPosController : MonoBehaviour
{
    public float NewMaxY = 0;
    public float NewMaxX = 0;
    public float NewMinY = 0;
    public float NewMinX = 0;
    public bool LookUp = false;

    private GameObject Cam;
    private MoveCamera CamController;

    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("MainCamera");
        CamController = Cam.GetComponent<MoveCamera>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hellbot")
        {
            if (NewMaxY != 0){
                CamController.SetMaxCamY(NewMaxY);
            }
            
            if (NewMaxX != 0){
                CamController.SetMaxCamX(NewMaxX);
            }

            if (NewMinY != 0){
                CamController.SetMinCamY(NewMinY);
            }

            if (NewMinX != 0){
                CamController.SetMinCamX(NewMinX);
            }

            CamController.SetLookUp(LookUp);

        }
    }
}
