using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDoubleStrike : AttackCard
{
    public List<Enemy> Targets;


    // Start is called before the first frame update
    void Start()
    {
        id = 10;
        value = 30;
        mana = 3;
        name = "Double Strike";
        description = "Deal 30 damage to 2 targets.";
        numberOfTargets = 2;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }
}
