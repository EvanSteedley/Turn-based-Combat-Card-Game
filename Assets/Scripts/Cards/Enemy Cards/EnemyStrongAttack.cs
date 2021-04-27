using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class EnemyStrongAttack: EnemyCard
    {


    void Start()
    {
        value = 30;
        p = FindObjectOfType<Player>();
        cardName = "StrongAttack";
    }


    public override void Action()
    {
        p.TakeDamage(value);
    }

}

