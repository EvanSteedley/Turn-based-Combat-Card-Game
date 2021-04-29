using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageNumber : MonoBehaviour
{
    Camera cam;
    Scene s;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        s = SceneManager.GetActiveScene();
        transform.LookAt(cam.transform);
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform);
        if (s.name.Equals("Combat") || s.name.Equals("Sample Combat") || s.name.Substring(0, 4).Equals("Boss"))
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        else
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Vector3.up.y + 180f, 0f);
        gameObject.transform.position += new Vector3(0, 1.5f * Time.deltaTime, 0f);
    }
}
