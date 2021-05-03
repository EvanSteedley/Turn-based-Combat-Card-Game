using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
        p = FindObjectOfType<Player>();
        t = FindObjectOfType<Turns>();
        p.CardPlayed += HandleCardPlayed;
        //SliderHealth.value = health;
        anim = GetComponentInChildren<Animator>();
        ET = FindObjectOfType<EnemyTable>();
        EnemyDefenseValue.text = defense.ToString();
        HealthValue.text = health.ToString();
        EnemyAttackValue.text = damage.ToString();
        carTypes = new List<string>()
{
  "Attack", "BuffAttack", "BuffDefense", "StrongAttack"
};
        instanceCards.Add(gameObject.AddComponent<EnemyAttack>());
        instanceCards.Add(gameObject.AddComponent<EnemyBuffAttack>());
        instanceCards.Add(gameObject.AddComponent<EnemyBuffDefense>());
        instanceCards.Add(gameObject.AddComponent<EnemyStrongAttack>());
    }

    private void Awake()
    {
        health = 150;
        damage = 20;
        defense = 0;
        goldValue = 200;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
