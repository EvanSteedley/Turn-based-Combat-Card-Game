using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class EnemyBuffAttack: EnemyCard
    {


    void Start()
    {
        value = 4;
        p = FindObjectOfType<Player>();
        e = GetComponent<Enemy>();
        cardName = "BuffAttack";
    }


    public override void Action()
    {
        e.Buff(value);
        e.anim.SetTrigger("Power Up");
    }



}

