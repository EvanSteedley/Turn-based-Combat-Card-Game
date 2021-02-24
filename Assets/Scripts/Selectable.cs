using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    Renderer ren;
    Color defaultColor;
    [SerializeField]
    Selection s;
    bool selected;
    // Start is called before the first frame update
    void Start()
    {
        s = FindObjectOfType<Selection>();
        ren = gameObject.GetComponent("Renderer") as Renderer;
        defaultColor = ren.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOver()
    {
        // Debug.Log(ren.material.color);
        if(!selected)
            ren.material.color = Color.cyan;
    }
    private void OnMouseExit()
    {
        if(!selected)
            ren.material.color = defaultColor;
    }

    private void OnMouseDown()
    {
        selected = true;
        //Sets other selected object to unselected
        if (s.somethingSelected && s.selected != this.gameObject)
        {
            Selectable otherObject = s.Selected.GetComponentInChildren<Selectable>();
            //Debug.Log(otherObject.name);
            otherObject.selected = false;
            otherObject.ren.material.color = otherObject.defaultColor;
        }
        s.somethingSelected = true;
        //s.IFX.gameObject.SetActive(true);
        //s.IFY.gameObject.SetActive(true);
        //s.IFZ.gameObject.SetActive(true);
        s.Selected = this.gameObject;
        //s.SelectionName.text = this.gameObject.name;
        ren.material.color = Color.blue;
    }
}
