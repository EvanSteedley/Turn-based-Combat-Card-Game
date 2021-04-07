using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonStabCard : DebuffCard
{


    // Start is called before the first frame update
    void Start()
    {
        id = 8;
        value = 10;
        mana = 1;
        name = "Poison Stab";
        description = "Poisons one target.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }

    public override void Action()
    {
        foreach (GameObject GO in Targeter.Selections)
        {
            Enemy e = GO.GetComponent<Enemy>();

            if (e != null)
            {
                LaunchProjectile(e.gameObject);
                e.gameObject.AddComponent<Poison>();
            }
        }
        RemoveHighlightTargets();
        ClearSelections();
        Destroy(this.gameObject);
    }
}
