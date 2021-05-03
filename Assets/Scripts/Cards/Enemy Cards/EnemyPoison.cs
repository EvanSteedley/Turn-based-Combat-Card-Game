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
        e = GetComponent<Enemy>();
        cardName = "Poison";
    }


    public override void Action()
    {
        Poison pois = p.gameObject.AddComponent<Poison>();
        pois.UpdateValues(value, turns);
        e.anim.SetTrigger("Attack");
        e.anim.SetTrigger("Attack 2");

    }

}


