using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        health = 150;
        damage = 35;
        defense = 0;
        p = FindObjectOfType<Player>();
        t = FindObjectOfType<Turns>();
        //SliderHealth.value = health;
        anim = GetComponent<Animator>();
        EnemyDefenseValue.text = defense.ToString();
        HealthValue.text = health.ToString();
        EnemyAttackValue.text = damage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
