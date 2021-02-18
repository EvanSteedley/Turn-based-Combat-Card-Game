using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableGO : MonoBehaviour
{
    Renderer ren;
    Color defaultColor;
    [SerializeField]
    SelectionGO s;
    bool selected;
    // Start is called before the first frame update
    void Start()
    {
        s = FindObjectOfType<SelectionGO>();
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
        if (!selected)
            ren.material.color = Color.cyan;
    }
    private void OnMouseExit()
    {
        if (!selected)
            ren.material.color = defaultColor;
    }

    private void OnMouseDown()
    {
        selected = true;
        //Sets other selected object to unselected
        if (s.somethingSelected && s.selected != this.gameObject)
        {
            SelectableGO otherObject = s.Selected.GetComponentInChildren<SelectableGO>();
            otherObject.selected = false;
            otherObject.ren.material.color = otherObject.defaultColor;
        }
        s.somethingSelected = true;
        s.Selected = this.gameObject;
        ren.material.color = Color.blue;
    }
}
