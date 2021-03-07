using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMovement : MonoBehaviour
{
   // public Transform eyeDest;


    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);


    }
}