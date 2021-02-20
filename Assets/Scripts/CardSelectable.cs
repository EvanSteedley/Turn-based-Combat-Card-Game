using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectable : MonoBehaviour
{
    public Renderer ren;
    public Color defaultColor;
    [SerializeField]
    CardSelection s;
    public bool selected;
    public Card card;
    // Start is called before the first frame update
    void Start()
    {
        s = FindObjectOfType<CardSelection>();
        ren = gameObject.GetComponent("Renderer") as Renderer;
        defaultColor = ren.material.color;
        card = GetComponent<Card>();
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
            CardSelectable otherObject = s.Selected.GetComponentInChildren<CardSelectable>();
            //Debug.Log(otherObject.name);
            otherObject.selected = false;
            otherObject.ren.material.color = otherObject.defaultColor;
            s.Selected.transform.position = s.Selected.transform.position + new Vector3(0.0f, -1.0f, 0.0f);
            s.Selected = this.gameObject;
            s.somethingSelected = true;
            ren.material.color = Color.blue; 
            transform.position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            card.HighlightTargets();
        }
        else if (s.somethingSelected && s.selected == this.gameObject)
        {
            selected = false;
            s.somethingSelected = false;
            s.Selected = null;
            ren.material.color = defaultColor;
            transform.position = transform.position + new Vector3(0.0f, -1.0f, 0.0f);
            card.RemoveHighlightTargets();
        }
        else
        {
            s.somethingSelected = true;

            //if (s.Selected != this.gameObject)
            //{
            //    s.Selected = this.gameObject;
            //    s.Selected.transform.position = this.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            //}
            //else 
            
           s.Selected = this.gameObject;
           ren.material.color = Color.blue;
           transform.position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
           card.HighlightTargets();
        }
    }
}
