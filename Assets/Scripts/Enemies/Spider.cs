using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Spider : Enemy
{
    // Start is called before the first frame update
    void Start()
    {

        p = FindObjectOfType<Player>();
        t = FindObjectOfType<Turns>();
        ET = FindObjectOfType<EnemyTable>();
        p.CardPlayed += HandleCardPlayed;
        //SliderHealth.value = health;
        anim = GetComponentInChildren<Animator>();
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
        damage = 15;
        defense = 0;
        goldValue = 200;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
