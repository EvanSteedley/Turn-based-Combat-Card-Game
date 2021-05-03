using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class EnemyStun : EnemyCard
{

    void Start()
    {
        value = 10;
        p = FindObjectOfType<Player>();
        e = GetComponent<Enemy>();
        cardName = "Stun";
    }


    public override void Action()
    {
        p.gameObject.AddComponent<Stun>();
        e.anim.SetTrigger("Attack");
        e.anim.SetTrigger("Attack 2");

    }

}
