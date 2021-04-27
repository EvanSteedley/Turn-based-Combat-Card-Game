using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


    class EnemyPoison: EnemyCard
    {

    void Start()
    {
        value = 10;
        p = FindObjectOfType<Player>();
        cardName = "Poison";
    }


    public override void Action()
    {
        p.gameObject.AddComponent<Poison>();


    }

}


