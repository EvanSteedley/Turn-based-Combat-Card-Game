using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class StatusEffects : MonoBehaviour
    {

    [SerializeField]
    public Turns t;
    [SerializeField]
    public int statChangedBy;
    public int valueToChangeBy;
    public int turnsLeft = 2;



    void Start()
    {
        t = FindObjectOfType<Turns>();
        t.TurnEnded += Action;
    }

    virtual public void Action (object sender, EventArgs e)

    {

        if (turnsLeft > 0)
        {
            turnsLeft--;
        }
        else
        {
            //Revert and remove this StatusEffect
            Revert();
        }
    }

    public virtual void Revert()
    {
        //ValueChanged -= statChange;
    }

    public virtual void UpdateValues(int value, int turns)
    {
        valueToChangeBy = value;
        turnsLeft = turns;
    }

    public virtual void ApplyEffect()
    {
        //Do the Action
        //int startValue = ValueChanged;
        //ValueChanged += valueToChangeBy;
        //statChangedBy = ValueChanged - startValue;
    }

}



