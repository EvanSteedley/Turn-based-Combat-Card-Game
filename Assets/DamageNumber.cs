using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        transform.LookAt(cam.transform);
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform);
        gameObject.transform.position += new Vector3(0, 1.5f * Time.deltaTime, 0f);
    }
}
