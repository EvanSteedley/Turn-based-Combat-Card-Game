using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWindsOfRestoration : Card
{
    public List<Player> Targets;


    // Start is called before the first frame update
    void Start()
    {
        id = 8;
        value = 55;
        mana = 4;
        name = "Winds of Restoration";
        description = "Heals for 55 Health.";
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
        p.Heal(value);
        Destroy(this.gameObject);
    }

    override public void ClearSelections()
    {
        Targeter.Selections = new List<GameObject>();
    }
}
