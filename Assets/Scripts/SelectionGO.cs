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
    public bool exclusive = true;

    public Player player;
    public bool Selecting = true;

    //public GameObject Selected
    //{
    //    get { return this.selected; }
    //    set { this.selected = value; }
    //}

    public bool AddSelection(GameObject o)
    {
        if (Selections.Count >= numberOfSelections)
            return false;
        Selections.Add(o);
        return true;
    }

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
        player = FindObjectOfType<Player>();
        Selections = new List<GameObject>();
        //player.SelectButton = GameObject.Find("Select").GetComponent<Button>();
        //player.DeselectButton = GameObject.Find("Deselect").GetComponent<Button>();
        //player.SelectButton.enabled = false;
        //player.DeselectButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectClicked()
    {
        Selecting = true;
    }

    public void DeselectClicked()
    {
        Selecting = false;
    }

}
