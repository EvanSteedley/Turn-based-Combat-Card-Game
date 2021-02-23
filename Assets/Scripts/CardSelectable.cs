using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSelectable : MonoBehaviour
{
    public Renderer ren;
    public Color defaultColor;
    [SerializeField]
    CardSelection s;
    public bool selected;
    public Card card;
    public Player player;
    public GameObject original;
    public GameObject centered;
    // Start is called before the first frame update
    void Start()
    {
        s = FindObjectOfType<CardSelection>();
        ren = gameObject.GetComponent("Renderer") as Renderer;
        defaultColor = ren.material.color;
        card = GetComponent<Card>();
        player = FindObjectOfType<Player>();
        original = new GameObject();
        original.transform.localPosition = this.transform.position;
        original.transform.localRotation = this.transform.rotation;
        original.transform.parent = this.gameObject.transform.parent;
        centered = new GameObject();
        centered.transform.parent = this.gameObject.transform.parent;
        centered.transform.localPosition = new Vector3(1f, 2.2f, -3.2f);
        centered.transform.localRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        centered.transform.eulerAngles = new Vector3(-64f, original.transform.eulerAngles.y, original.transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOver()
    {
        // Debug.Log(ren.material.color);
        if (!selected && !EventSystem.current.IsPointerOverGameObject())
            ren.material.color = Color.cyan;
    }
    private void OnMouseExit()
    {
        if (!selected)
            ren.material.color = defaultColor;
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {

            selected = true;
            //Sets other selected object to unselected

            if (s.selected != null && s.selected != this.gameObject)
            {
                CardSelectable otherObject = s.Selected.GetComponentInChildren<CardSelectable>();
                //Debug.Log(otherObject.name);
                otherObject.Deselect();

                Select();
            }
            else if (s.selected != null && s.selected == this.gameObject)
            {
                Deselect();
            }
            else
            {
                Select();
            }
        }
    }

    public void Select()
    {
        s.somethingSelected = true;
        selected = true;
        s.Selected = this.gameObject;
        ren.material.color = Color.blue;
        //transform.position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        transform.position = centered.transform.position;
        transform.rotation = centered.transform.rotation;
        bool canUse = player.ManaCheckUI();
        card.Targeter.Selecting = true;
        if (canUse)
            card.HighlightTargets();
        if(!card.Targeter.exclusive)
        {
            player.SelectButton.gameObject.SetActive(true);
            player.DeselectButton.gameObject.SetActive(true);
        }
    }

    public void Deselect()
    {
        selected = false;
        s.somethingSelected = false;
        s.Selected = null;
        ren.material.color = defaultColor;
        //transform.position = transform.position + new Vector3(0.0f, -1.0f, 0.0f);
        transform.position = original.transform.position;
        transform.rotation = original.transform.rotation;
        card.RemoveHighlightTargets();
        card.ClearSelections();
        if (!card.Targeter.exclusive)
        {
            player.SelectButton.gameObject.SetActive(false);
            player.DeselectButton.gameObject.SetActive(false);
        }
    }
}
