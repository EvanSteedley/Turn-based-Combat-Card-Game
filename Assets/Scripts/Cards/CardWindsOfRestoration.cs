using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWindsOfRestoration : HealingCard
{


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
}
