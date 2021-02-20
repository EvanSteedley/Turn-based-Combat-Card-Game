using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableGO : MonoBehaviour
{
    public Renderer ren;
    public Color defaultColor;
    
    public SelectionGO s;
    public bool selected;
    // Start is called before the first frame update
    void Start()
    {
        s = FindObjectOfType<SelectionGO>();
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
        //selected = true;
        //Sets other selected object to unselected
        bool alreadyInSelections = false;
        foreach (GameObject g in s.Selections)
        {
            if (g == this.gameObject)
            {
                s.RemoveSelection(this.gameObject);
                alreadyInSelections = true;
                break;
            }
        }
        if (!alreadyInSelections)
        {
            //SelectableGO otherObject = s.Selected.GetComponentInChildren<SelectableGO>();
            //otherObject.selected = false;
            //otherObject.ren.material.color = otherObject.defaultColor;
            bool added = s.AddSelection(this.gameObject);
            if(added)
            {
                ren.material.color = Color.blue;
                selected = true;
            }
        }
        else
        {
            s.RemoveSelection(this.gameObject);
            ren.material.color = defaultColor;
            selected = false;
        }
        //s.somethingSelected = true;
        //s.Selected = this.gameObject;
        //ren.material.color = Color.blue;
    }
}
