using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBuff : OnOrOffStatusEffects
{
    // Start is called before the first frame update
    void Start()
    {
        t = FindObjectOfType<Turns>();
        t.TurnEnded += Action;
        valueToChangeBy = 5;
    }

    public override void Revert()
    {
        Enemy e2 = this.transform.GetComponent<Enemy>();
        Player p = this.transform.GetComponent<Player>();
        if (p != null)
        {
            p.BuffDefense(-statChangedBy);
        }

        else if (e2 != null)
        {
            e2.BuffDefense(-statChangedBy);
        }
        t.TurnEnded -= Action;
        Destroy(this);
    }

    public override void ApplyEffect()
    {
        Enemy e2 = this.transform.GetComponent<Enemy>();
        Player p = this.transform.GetComponent<Player>();
        if (p != null)
        {
            int startVal = p.defense;
            p.BuffDefense(valueToChangeBy);
            statChangedBy = p.defense - startVal;
        }

        else if (e2 != null)
        {
            int startVal = e2.defense;
            e2.BuffDefense(valueToChangeBy);
            statChangedBy = e2.defense - startVal;
        }
    }


}
