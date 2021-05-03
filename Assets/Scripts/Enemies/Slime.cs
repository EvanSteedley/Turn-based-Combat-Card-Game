using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Slime : Enemy
{
    // Start is called before the first frame update
    void Start()
    {

        p = FindObjectOfType<Player>();
        t = FindObjectOfType<Turns>();
        //SliderHealth.value = health;
        anim = GetComponentInChildren<Animator>();
        ET = FindObjectOfType<EnemyTable>();
        p.CardPlayed += HandleCardPlayed;
        EnemyDefenseValue.text = defense.ToString();
        HealthValue.text = health.ToString();
        EnemyAttackValue.text = damage.ToString();
        carTypes = new List<string>()
        {
            "Poison", "Attack", "DefenseDown", "BuffAttack"
        };
        instanceCards.Add(gameObject.AddComponent<EnemyAttack>());
        instanceCards.Add(gameObject.AddComponent<EnemyPoison>());
        instanceCards.Add(gameObject.AddComponent<EnemyDefenseDown>());
        instanceCards.Add(gameObject.AddComponent<EnemyBuffAttack>());
    }

    private void Awake()
    {
        health = 100;
        damage = 10;
        defense = 0;
        goldValue = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
