using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHealingSong : HealingCard
{


    // Start is called before the first frame update
    void Start()
    {
        id = 3;
        value = 15;
        mana = 1;
        name = "Healing Song";
        description = "Heals for 15 Health.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }
}
