using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHeavyHand : AttackCard
{
    public List<Enemy> Targets;


    // Start is called before the first frame update
    void Start()
    {
        id = 6;
        value = 50;
        mana = 2;
        name = "Heavy Hand";
        description = "Deal 50 damage to 1 target.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }
}
