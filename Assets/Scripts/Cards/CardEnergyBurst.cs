using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEnergyBurst : Card
{
    public List<Player> Targets;

    // Start is called before the first frame update
    void Start()
    {
        id = 4;
        mana = 4;
        value = 2; //will icrease max mana by 2
        name = "Energy Burst";
        description = "Increases Max Mana by 2.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void Action()
    {
        Player p = FindObjectsOfType<Player>()[0];
        p.BuffMana(value);
        Destroy(this.gameObject);
    }

    override public void ClearSelections()
    {
        Targeter.Selections = new List<GameObject>();
    }
}
