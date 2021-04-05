using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeglectDamageCard : Card
{
    
    public List<Player> Targets;
    public Player p;

    // Start is called before the first frame update
    void Start()
    {
        id = 11;
        mana = 3;
        value = 999;
        name = "Neglect Damage";
        description = "Ignore damage this turn.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        p = FindObjectOfType<Player>();
        SetInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void Action()
    {
        //p = FindObjectOfType<Player>();
        Debug.Log("Armor up.  Defense += " + value);
        p.BuffDefense(value);
        Destroy(this.gameObject);
    }

    override public void ClearSelections()
    {
        Targeter.Selections = new List<GameObject>();
    }
}
