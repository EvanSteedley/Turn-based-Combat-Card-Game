using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFireball : AttackCard
{


    // Start is called before the first frame update
    void Start()
    {
        id = 6;
        value = 50;
        mana = 2;
        name = "Fireball";
        description = "Shoot a 50 damage Fireball at one target.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }
}
