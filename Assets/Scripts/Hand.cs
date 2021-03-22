using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        Player p = FindObjectsOfType<Player>()[0];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw(GameObject c)
    {
       // c.Transform.setParent();// (this.Transform.parent, false);
    }
}
