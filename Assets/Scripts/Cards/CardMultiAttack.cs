using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMultiAttack : AttackCard
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        id = 2;
        value = 20;
        mana = 3;
        name = "Multi Attack";
        description = "Deal 20 damage to 3 targets.";
        numberOfTargets = 3;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = false;
        SetInfo();
    }
}
