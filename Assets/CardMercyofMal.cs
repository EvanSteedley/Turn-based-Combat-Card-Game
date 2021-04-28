using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercyofMal : AttackCard
{


    // Start is called before the first frame update
    void Start()
    {
        id = 21;
        value = 30;
        mana = 2;
        name = "Mercy of Malificent";
        description = "Malificent is showing mercy upon her victims by only dealing 30 damage.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }
}

