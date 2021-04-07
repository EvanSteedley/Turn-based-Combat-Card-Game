using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCrushingBlow : AttackCard
{
    // Start is called before the first frame update
    void Start()
    {
        id = 9;
        value = 55;
        mana = 4;
        name = "Crushing Blow";
        description = "Deal 55 damage to one target.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }
}
