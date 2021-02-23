using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionGO : MonoBehaviour
{
    public List<GameObject> Selections;
    public bool somethingSelected = false;
    public int numberOfSelections = 1;


    //public GameObject Selected
    //{
    //    get { return this.selected; }
    //    set { this.selected = value; }
    //}
    //Add the GameObject to list. 
    public bool AddSelection(GameObject o)
    {
        if (Selections.Count >= numberOfSelections)
            return false;
        Selections.Add(o);
        return true;
    }
    //Remove the GameObject if it is in the list. 
    public bool RemoveSelection(GameObject o)
    {
        bool inSelections = false;
        foreach (GameObject g in Selections)
        {
            if(g == o)
            {
                Selections.Remove(o);
                inSelections = true;
                break;
            }
        }
        if (inSelections)
            return true;
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Selections = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
