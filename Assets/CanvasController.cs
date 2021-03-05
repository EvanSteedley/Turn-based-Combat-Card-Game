using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    Camera MainCam;
    // Start is called before the first frame update
    void Start()
    {
        MainCam = FindObjectOfType<Camera>();
        transform.LookAt(MainCam.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(MainCam.transform);
    }
}
