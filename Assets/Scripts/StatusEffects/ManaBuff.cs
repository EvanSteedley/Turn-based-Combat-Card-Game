using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBuff : OnOrOffStatusEffects
{
    void Start()
    {
        t = FindObjectOfType<Turns>();
        t.TurnEnded += Action;
        valueToChangeBy = 3;
    }

    public override void Revert()
    {
        Enemy e2 = this.transform.GetComponent<Enemy>();
        Player p = this.transform.GetComponent<Player>();
        if (p != null)
        {
            p.BuffMana(-statChangedBy);
        }

        else if (e2 != null)
        {
            
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
            int startVal = p.maxMana;
            p.BuffMana(valueToChangeBy);
            statChangedBy = p.maxMana - startVal;
        }

        else if (e2 != null)
        {
        }
    }

}
