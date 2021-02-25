using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    //Information about the cards
    public string cardName = "";
    public int id = 0;
    public int mana = 0;
    public int damage = 0;
    public int defense = 0;
    //public enum cardType {Earth,Wind,Fire,Water,Null}
    //public cardType cardElement;
    public string description;

    public int numberOfTargets = 1;
    public SelectionGO Targeter;
    public GameObject targetType;
    //Variable for type of target probably goes into a specific card, rather then the base card
    //Enemy targetType;

    //public Card(){

    //}


   
    //public Card(string CardName, int Id, int Mana, int Damage, int Defense,cardType CardElement,string Description)
    //{
    //    cardName = CardName;
    //    id = Id;
    //    mana = Mana;
    //    damage = Damage;
    //    defense = Defense;
    //    cardElement = CardElement;
    //    description = Description;


   
    //}

    //  Start is called before the first frame update
    void Start()
    {
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;

        //Important line!  If true: a target can only be selected once.  If false, the same target can be selected multiple times.
        Targeter.exclusive = false;
    }

    /*public GameObject Selected
    {
        get { return this.selected; }
        set { this.selected = value; }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void Action()
    {
        //The actual "thing" the card does, should be done here.  OR at least started here. (If lasting multiple turns)

        //Example:  Every target takes 20 damage.
        //foreach (GameObject GO in Targeter.Selections)
        //{
        //    //Do the thing
        //    Enemy e = GO.GetComponent<Enemy>();
        //    if (e != null)
        //        e.TakeDamage(20);
        //}
        RemoveHighlightTargets();
    }

    //This method searches all objects in the game, finds those that are selectable, and highlights them.
    virtual public void HighlightTargets()
    {
        //Finds all GameObjects
        GameObject[] objects = FindObjectsOfType<GameObject>();
        foreach (GameObject GO in objects)
        {
            //Gets its SelectableGO (GameObject) component, if it exists
            SelectableGO SGO = GO.GetComponent<SelectableGO>();

            //If the SGO DOES exist, then it's a legitimate target
            if(SGO != null)
            {
                //Enables the object; it is now actually selectable
                SGO.enabled = true;
                if (SGO.ren == null)
                    SGO.ren = SGO.GetComponent<Renderer>();
                SGO.ren.material.color = Color.cyan;
                //Sets the SelectableGO's Selection object this this card's Targeter.
                SGO.SGO = Targeter;
            }
        }
    }

    //This method is the opposite of the one above it.
    virtual public void RemoveHighlightTargets()
    {
        GameObject[] objects = FindObjectsOfType<GameObject>();
        foreach (GameObject GO in objects)
        {
            SelectableGO SGO = GO.GetComponent<SelectableGO>();
            if (SGO != null)
            {
                //SGO.ren.material.color = SGO.defaultColor;
                if (SGO.ren == null)
                    SGO.ren = SGO.GetComponent<Renderer>();
                SGO.ren.material.color = SGO.defaultColor;
                SGO.enabled = false;
            }
        }
    }

    //Empties the list of Selections
    virtual public void ClearSelections()
    {
        Targeter.Selections = new List<GameObject>();
    }
}
