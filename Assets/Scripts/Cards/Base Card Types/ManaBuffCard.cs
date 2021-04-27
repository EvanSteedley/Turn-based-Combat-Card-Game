using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBuffCard : Card
{
    public int turnsToLast = 3; //Lasts for 3 turns (not including current turn)
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
        ManaBuff mb = p.gameObject.AddComponent<ManaBuff>();
        mb.UpdateValues(value, turnsToLast);
        mb.ApplyEffect();
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
