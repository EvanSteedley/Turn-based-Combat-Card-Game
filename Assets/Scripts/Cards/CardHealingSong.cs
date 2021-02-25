using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHealingSong : Card
{
    public List<Player> Targets;


    // Start is called before the first frame update
    void Start()
    {
        id = 6;
        damage = 50;
        mana = 2;
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
        foreach (GameObject GO in Targeter.Selections)
        {
            Player p = GO.GetComponent<Player>();
            if (p != null)
                p.TakeDamage(damage);
        }
        RemoveHighlightTargets();
        ClearSelections();
        Destroy(this.gameObject);
    }
}
