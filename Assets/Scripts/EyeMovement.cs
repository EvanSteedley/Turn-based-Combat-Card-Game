using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMovement: MonoBehaviour
{
    Transform eyeDest;

    
    void Update()
        {

        Vector3 pos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(20);
            Vector3 invertedMousePos = new Vector3(-pos.x * (float)0.2, -pos.y * (float)0.2, pos.z);
            eyeDest.transform.LookAt(invertedMousePos);
        }
    }
