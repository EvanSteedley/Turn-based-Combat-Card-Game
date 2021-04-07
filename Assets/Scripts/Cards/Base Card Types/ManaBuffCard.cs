using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBuffCard : Card
{

    // Start is called before the first frame update
    void Start()
    {
        id = 4;
        mana = 2;
        value = 2; //will icrease max mana by 2
        name = "";
        description = "";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }

    override public void Action()
    {
        Player p = FindObjectOfType<Player>();
        p.BuffMana(value);
        Destroy(this.gameObject);
    }

    override public void HighlightTargets()
    {

    }

    override public void RemoveHighlightTargets()
    {

    }

    override public void ClearSelections()
    {
        Targeter.Selections = new List<GameObject>();
    }
}
