using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class EnemyBuffHealth: EnemyCard
    {
    void Start()
    {
        value = 15;
        p = FindObjectOfType<Player>();

    }


    public override void Action()
    {
        this.GetComponentInParent<Enemy>().BuffHealth(value);
    }




}

