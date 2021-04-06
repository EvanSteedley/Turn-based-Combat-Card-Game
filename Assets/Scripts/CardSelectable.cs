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
    //public GameObject original;
    public Vector3 originalP;
    public Vector3 originalR;
    //public GameObject centered;
    public Vector3 centeredP;
    public Vector3 centeredR;
    // Start is called before the first frame update
    void Start()
    {
        s = FindObjectOfType<CardSelection>();
        ren = gameObject.GetComponent("Renderer") as Renderer;
        defaultColor = ren.material.color;
        card = GetComponent<Card>();
        player = FindObjectOfType<Player>();

        //Stores original position of the current card 
        //original = new GameObject();
        //original.transform.localPosition = this.transform.position;
        //original.transform.localRotation = this.transform.rotation;
        //original.transform.parent = this.gameObject.transform.parent;
        originalP = this.transform.localPosition;
        originalR = this.transform.eulerAngles;

        //Stores the position Centered on the screen
        //centered = new GameObject();
        //centered.transform.parent = this.gameObject.transform.parent;
        //centered.transform.localPosition = new Vector3(7f, 2f, 0f);
        //centered.transform.localRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        centeredP = new Vector3(7f, 2f, 0f);
        centeredR = new Vector3(-64f, 0f, -90f);
        //New values after moving camera/player
        centeredP = new Vector3(6f, 1f, 0.25f);
        centeredR = new Vector3(-30f, 0f, -90f);
        //The x-value of this Euler Angle was determined by manually moving the card in-scene
        //It was also the only angle that needed changing; the others are the same as the original.
        //centered.transform.eulerAngles = new Vector3(-64f, original.transform.eulerAngles.y, original.transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOver()
    {
        //Checks if the mouse isn't on a GUI element, and if the card isn't already selected.
        if (!selected && !EventSystem.current.IsPointerOverGameObject())
            ren.material.color = Color.cyan;
        //If the mouse IS on a GUI element, then set the color back to default.
        if (EventSystem.current.IsPointerOverGameObject())
            ren.material.color = defaultColor;
    }
    private void OnMouseExit()
    {
        //When the mouse moves off of the object; if it's not selected, then the color is set back to normal.
        if (!selected)
            ren.material.color = defaultColor;
    }


    //Called when the object is clicked.
    private void OnMouseDown()
    {
        //If the mouse is not over the GUI            OR      The GUI object the mouse is on is part of a canvas on the card:
        if (!EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            //If there is already a selection & it's NOT this card:
            if (s.selected != null && s.selected != this.gameObject)
            {
                //Deselect the other card
                CardSelectable otherObject = s.Selected.GetComponentInChildren<CardSelectable>();
                otherObject.Deselect();

                originalP = this.transform.localPosition;
                originalR = this.transform.eulerAngles;
                //Select this card
                Select();
            }
            //If there is a selection and it IS this card:
            else if (s.selected != null && s.selected == this.gameObject)
            {
                Deselect();
            }
            //If NO selection at all:
            else
            {
                originalP = this.transform.localPosition;
                originalR = this.transform.eulerAngles;
                Select();
            }
        }
    }

    public void Select()
    {
        //If this script is enabled:
        if (enabled)
        {
            ren.material.color = defaultColor;
            s.somethingSelected = true;
            selected = true;
            //Set the stored Selection to this card
            s.Selected = this.gameObject;
            //ren.material.color = Color.blue;
            //transform.position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);

            //Center this card on the screen
            //transform.position = centered.transform.position;
            //transform.rotation = centered.transform.rotation;
            transform.localPosition = centeredP;
            transform.eulerAngles = centeredR;

            //Update the PlayCard button's Mana Cost (Yellow in the top-left corner of the button)
            bool canUse = player.ManaCheckUI();
            //Selects targets by default, if non-exclusive.
            card.Targeter.Selecting = true;

            //If the Player has enough Mana to use this card, highlight its possible targets.
            if (canUse)
                card.HighlightTargets();
            //If non-exclusive, enable the Select and Deselect buttons.
            if (!card.Targeter.exclusive)
            {
                player.SelectButton.gameObject.SetActive(true);
                player.DeselectButton.gameObject.SetActive(true);
            }
        }
    }

    public void Deselect()
    {
        player.PlayCardButton.interactable = false;
        selected = false;
        //Since this effects the CardSelection's variable, Deselect() should always be called
        //BEFORE the Select() method on another card.  Otherwise, there may be Null reference exceptions.
        s.somethingSelected = false;
        s.Selected = null;

        //Reset the color of this card
        ren.material.color = defaultColor;
        //transform.position = transform.position + new Vector3(0.0f, -1.0f, 0.0f);

        //Reset the position to its original position
        //transform.position = original.transform.position;
        //transform.rotation = original.transform.rotation;
        transform.localPosition = originalP;
        transform.eulerAngles = originalR;

        //Un-highlight all the card's targets
        card.RemoveHighlightTargets();
        //Empty the card's list of targets; might cause Null errors if not
        card.ClearSelections();

        //If non-exclusive, disable the Select/Deselect buttons again
        if (!card.Targeter.exclusive)
        {
            player.SelectButton.gameObject.SetActive(false);
            player.DeselectButton.gameObject.SetActive(false);
        }
    }
}
