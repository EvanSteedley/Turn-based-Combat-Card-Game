using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


class EnemyAttack : EnemyCard
    {

    void Start()
    {
        p = FindObjectOfType<Player>();
        e = GetComponent<Enemy>();
        value = gameObject.GetComponent<Enemy>().damage;
        cardName = "Attack";
    }


    public override void Action()
    {
        e.Attack();
        e.anim.SetTrigger("Attack");
    }


}

