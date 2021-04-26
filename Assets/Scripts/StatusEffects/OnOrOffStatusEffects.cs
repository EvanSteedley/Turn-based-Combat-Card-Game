using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOrOffStatusEffects : StatusEffects
{
    // Start is called before the first frame update
    void Start()
    {
        t = FindObjectOfType<Turns>();
        t.TurnEnded += Action;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Action(object sender, EventArgs e)
    {
        if (turnsLeft > 0)
        {
            //Do the Action
            //int startValue = ValueChanged;
            //ValueChanged += valueToChangeBy;
            //statChangedBy = ValueChanged - startValue;
            turnsLeft--;
        }
        else
        {
            //Revert and remove this StatusEffect
            Revert();
        }
    }
}
