using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGreaterHealing : HealingCard
{

    // Start is called before the first frame update
    void Start()
    {
        id = 6;
        value = 35;
        mana = 3;
        name = "Greater Healing";
        description = "Heals for 35 Health.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }
}