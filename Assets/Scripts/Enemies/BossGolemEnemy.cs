using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGolemEnemy : Enemy
{
    
    // Start is called before the first frame update
    void Start()
    {

        p = FindObjectOfType<Player>();
        t = FindObjectOfType<Turns>();
        p.CardPlayed += HandleCardPlayed;
        ET = FindObjectOfType<EnemyTable>();
        //SliderHealth.value = health;
        anim = GetComponentInChildren<Animator>();
        EnemyDefenseValue.text = defense.ToString();
        HealthValue.text = health.ToString();
        EnemyAttackValue.text = damage.ToString();
        carTypes = new List<string>() {"Attack", "StrongAttack", "BuffDefense"};
        instanceCards.Add(gameObject.AddComponent<EnemyAttack>());
        instanceCards.Add(gameObject.AddComponent<EnemyStrongAttack>());
        instanceCards.Add(gameObject.AddComponent<EnemyBuffDefense>());
    }

    private void Awake()
    {
        health = 300;
        damage = 35;
        defense = 35;
        goldValue = 1500;
    }
    public override void Attack()
    {
        base.Attack();
        anim.SetTrigger("Attack");
    }

    public override void BuffDefense(int v)
    {
        base.BuffDefense(v);
        anim.SetTrigger("Power Up");
    }

    public override void Buff(int value)
    {
        base.Buff(value);
        anim.SetTrigger("Power Up");
    }

}