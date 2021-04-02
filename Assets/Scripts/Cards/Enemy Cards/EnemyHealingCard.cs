using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


class EnemyHealingCard: EnemyCard
    {

    void Start()
    {
        value = 25;
        p = FindObjectOfType<Player>();

    }


    public override void Action()
    {
        this.GetComponentInParent<Enemy>().Heal(value);

    }
}

