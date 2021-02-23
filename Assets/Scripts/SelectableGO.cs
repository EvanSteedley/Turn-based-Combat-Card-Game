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
        ren = GetComponent<Renderer>();
        defaultColor = ren.material.color;
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOver()
    {
        // Debug.Log(ren.material.color);
        //if (!selected)
        //    ren.material.color = Color.cyan;
    }
    private void OnMouseExit()
    {
        //if (!selected)
        //    ren.material.color = defaultColor;
    }

    private void OnMouseDown()
    {
        timesSelected = CalcTimesSelected();
        //selected = true;
        //Sets other selected object to unselected
        if (SGO != null && enabled && !EventSystem.current.IsPointerOverGameObject())
        {
            bool alreadyInSelections = false;
            if (SGO.exclusive)
            {
                foreach (GameObject g in SGO.Selections)
                {
                    if (g == this.gameObject)
                    {
                        SGO.RemoveSelection(this.gameObject);
                        alreadyInSelections = true;
                        break;
                    }
                }
            }
            if (!alreadyInSelections)
            {
                //SelectableGO otherObject = s.Selected.GetComponentInChildren<SelectableGO>();
                //otherObject.selected = false;
                //otherObject.ren.material.color = otherObject.defaultColor;
                if (!SGO.exclusive)
                {
                    if(SGO.Selecting)
                    {
                        bool added = SGO.AddSelection(this.gameObject);
                        if (added)
                        {
                            ren.material.color = Color.blue;
                            selected = true;
                        }
                        timesSelected = CalcTimesSelected();
                    }
                    else
                    {
                        SGO.RemoveSelection(this.gameObject);
                        timesSelected = CalcTimesSelected();
                        ren.material.color = defaultColor;
                        selected = false;
                    }

                }
                else
                {
                    bool added = SGO.AddSelection(this.gameObject);
                    if (added)
                    {
                        ren.material.color = Color.blue;
                        selected = true;
                    }
                    timesSelected = CalcTimesSelected();
                }
            }
            else
            {
                if (!SGO.exclusive)
                {
                    if (SGO.Selecting)
                    {
                        bool added = SGO.AddSelection(this.gameObject);
                        if (added)
                        {
                            ren.material.color = Color.blue;
                            selected = true;
                        }
                        timesSelected = CalcTimesSelected();
                    }
                    else
                    {
                        SGO.RemoveSelection(this.gameObject);
                        timesSelected = CalcTimesSelected();
                        ren.material.color = defaultColor;
                        selected = false;
                    }

                }
                else
                {
                    SGO.RemoveSelection(this.gameObject);
                    timesSelected = CalcTimesSelected();
                    ren.material.color = defaultColor;
                    selected = false;
                }
            }
        }
        //s.somethingSelected = true;
        //s.Selected = this.gameObject;
        //ren.material.color = Color.blue;
        UpdateSelectionColor();
    }

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

    public void UpdateSelectionColor()
    {
        //Should replace this with some UI method later!
        if (timesSelected == 0)
            ren.material.color = defaultColor;
        else
        {
            ren.material.color = Color.cyan * new Color(0, 0, timesSelected);
        }
    }
}
