using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEnhancedEndurance : HealthBuffCard
{

    // Start is called before the first frame update
    void Start()
    {
        id = 7;
        mana = 4;
        value = 25; //will icrease max hp 25
        name = "Enhanced Endurance";
        description = "Increases Max Health by 25.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }
}
