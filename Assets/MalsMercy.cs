using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalsMercy : AttackCard

{
   
    void Start()
    {
        id = 18;
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


