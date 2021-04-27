using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCard : Card
{


    // Start is called before the first frame update
    void Start()
    {
        id = 6;
        value = 35;
        mana = 2;
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
        p.Heal(value);
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 5f);
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