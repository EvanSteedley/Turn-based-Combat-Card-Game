using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public List<GameObject> AllItems = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject PullRandom()
    {
        GameObject itemToAdd = null;
        if (AllItems.Count > 0)
        {
            int index = Random.Range(0, AllItems.Count);
            itemToAdd = AllItems[index];
            AllItems.RemoveAt(index);
        }
        return itemToAdd;
    }
}
