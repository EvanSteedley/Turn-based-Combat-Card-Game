using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : OnOrOffStatusEffects
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
            p.DebuffHealth(statChangedBy);
        }

        else if (e2 != null)
        {
            e2.BuffHealth(-statChangedBy);
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
            int startVal = p.maxHealth;
            p.BuffHealth(valueToChangeBy);
            statChangedBy = p.maxHealth - startVal;
        }

        else if (e2 != null)
        {
            int startVal = e2.health;
            e2.BuffHealth(valueToChangeBy);
            statChangedBy = e2.health - startVal;
        }
    }
}
