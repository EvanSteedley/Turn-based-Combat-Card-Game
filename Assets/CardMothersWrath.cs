using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMothersWrath : AttackCard
{


    // Start is called before the first frame update
    void Start()
    {
        id = 23;
        value = 20;
        mana = 1;
        name = "Mother's Wrath";
        description = "Mother is using her glock to deal 25 damage to anyone who crosses her.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }
}

