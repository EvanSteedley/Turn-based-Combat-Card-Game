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
        damage = -55;
        mana = 5;
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void Action()
    {
        Player p = FindObjectsOfType<Player>()[0];
        p.Heal(damage);
        Destroy(this.gameObject);
    }

    override public void ClearSelections()
    {
        Targeter.Selections = new List<GameObject>();
    }
}
