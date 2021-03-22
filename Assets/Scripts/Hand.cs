using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    GameObject CurrentHand;
    

    // Start is called before the first frame update
    void Start()
    {
        Player p = FindObjectsOfType<Player>()[0];
        CurrentHand = GameObject.Find("Hand");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw(GameObject c)
    {
        c.transform.parent = CurrentHand.transform;
    }
}
