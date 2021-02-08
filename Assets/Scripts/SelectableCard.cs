using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableCard : MonoBehaviour
{
    public Renderer ren;
    public Color defaultColor;
    [SerializeField]
    SelectionCard s;
    public bool selected;
    // Start is called before the first frame update
    void Start()
    {
        s = FindObjectOfType<SelectionCard>();
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
            SelectableCard otherObject = s.Selected.GetComponentInChildren<SelectableCard>();
            //Debug.Log(otherObject.name);
            otherObject.selected = false;
            otherObject.ren.material.color = otherObject.defaultColor;
            s.Selected.transform.position = s.Selected.transform.position + new Vector3(0.0f, -0.5f, 0.0f);
        }
        s.somethingSelected = true;
        //s.Selected.transform.position = this.transform.position + new Vector3(0.0f, -0.5f, 0.0f);
        if (s.Selected != this.gameObject)
        {
            s.Selected = this.gameObject;
            s.Selected.transform.position = this.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        }
        else { s.Selected = this.gameObject; }
        ren.material.color = Color.blue;
    }
}
