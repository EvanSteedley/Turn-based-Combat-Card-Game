using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuffCard : Card
{
    public List<Player> Targets;
    public int turnsToLast = 3; //for 3 turns (not including current turn)

    // Start is called before the first frame update
    void Start()
    {
        id = 7;
        mana = 2;
        value = 25; //will increase max hp by 25
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
        HealthBuff hb = p.gameObject.AddComponent<HealthBuff>();
        hb.UpdateValues(value, turnsToLast);
        hb.ApplyEffect();
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
