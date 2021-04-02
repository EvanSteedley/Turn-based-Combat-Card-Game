using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableGO : MonoBehaviour
{
    public Renderer ren;
    public Color defaultColor;
    
    public SelectionGO SGO;
    public bool selected;
    public int timesSelected;
    // Start is called before the first frame update
    void Start()
    {
        SGO = FindObjectOfType<SelectionGO>();
        ren = GetComponentInChildren<Renderer>();
        //Stores the default color before the object is highlighted/selected.
        defaultColor = ren.material.color;
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOver()
    {
        //if (!selected)
        //  ren.material.color = Color.cyan;
    }
    private void OnMouseExit()
    {
        //if (!selected)
        //  ren.material.color = defaultColor;
    }

    private void OnMouseDown()
    {
        //If there is a card selected & this script is enabled & the mouse is not over a GUI object
        //Then the clicked object is selectable
        if (SGO != null && enabled && !EventSystem.current.IsPointerOverGameObject())
        {
            bool alreadyInSelections = false;
            //If exclusive, then the object can only be selected once!
            if (SGO.exclusive)
            {
                foreach (GameObject g in SGO.Selections)
                {
                    if (g == this.gameObject)
                    {
                        //SGO.RemoveSelection(this.gameObject);

                        //If g == this, then it is already selected.
                        alreadyInSelections = true;
                        break;
                    }
                }
            }
            //If the object is not already selected (or if the selection is non-exclusive; alreadyInSelections defaults to false for this case)
            if (!alreadyInSelections)
            {
                if (!SGO.exclusive)
                {
                    //If non-exclusive; then whether the object is selected is based on the Selecting variable
                    //SGO.Selecting is changed based on the buttons Select and Deselect being clicked in the UI.
                    if(SGO.Selecting)
                    {
                        Select();
                    }
                    else
                    {
                        Deselect();
                    }

                }
                //If not in selections, and exclusive, then the object is selected.
                else
                {
                    Select();
                }
            }
            //If the object IS already selected, then it must be exclusive:
            //And if it's exclusive and already selected, then clicking again will deselect the object.
            else
            {
                    Deselect();
            }
        }
        UpdateSelectionColor();
    }

    //Counts how many times the current object has been selected in the current Card's selections
    public int CalcTimesSelected()
    {
        int count = 0;
        foreach (GameObject GO in SGO.Selections)
        {
            if (GO == this.gameObject)
                count++;
        }
        return count;
    }

    //Updates the Color of the object based on how many times it's been selected
    public void UpdateSelectionColor()
    {
        timesSelected = CalcTimesSelected();
        //Should replace this with some UI method later!
        if (timesSelected == 0)
            ren.material.color = defaultColor;
        else
        {
            ren.material.color = Color.cyan * new Color(0, 0, timesSelected);
        }
    }


    //If the list of Selections is not already full, add the object to the list.
    public void Select()
    {
        bool added = SGO.AddSelection(this.gameObject);
        if (added)
        {
            ren.material.color = Color.blue;
            selected = true;
        }
    }

    //Remove the object from the list of Selections.
    public void Deselect() 
    {
        SGO.RemoveSelection(this.gameObject);
        ren.material.color = defaultColor;
        selected = false;
    }
}
