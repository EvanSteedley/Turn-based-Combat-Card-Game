using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    Camera MainCam;
    Scene s;
    // Start is called before the first frame update
    void Start()
    {
        MainCam = FindObjectOfType<Camera>();
        s = SceneManager.GetActiveScene();
        transform.LookAt(MainCam.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(MainCam.transform);
        if(s.name.Equals("Combat") || s.name.Equals("Sample Combat") || s.name.Substring(0, 4).Equals("Boss"))
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        else
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Vector3.up.y + 180f, 0f);
    }
}
