using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAirOfHades : AttackCard
{
    //TODO:  Assign the WhiteSmoke particle effect to the ProjectilePrefab.

    // Start is called before the first frame update
    void Start()
    {
        id = 17;
        value = 40;
        mana = 2;
        name = "Air of Hades";
        description = "Deals 40 damage to one enemy.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }
}


