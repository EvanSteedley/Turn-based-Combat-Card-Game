using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    // Start is called before the first frame update
    void Start()
    {

        p = FindObjectOfType<Player>();
        t = FindObjectOfType<Turns>();
        //SliderHealth.value = health;
        anim = GetComponent<Animator>();
        EnemyDefenseValue.text = defense.ToString();
        HealthValue.text = health.ToString();
        EnemyAttackValue.text = damage.ToString();
    }

    private void Awake()
    {
        health = 150;
        damage = 35;
        defense = 0;
        goldValue = 200;
    }

    // Update is called once per frame
    void Update()
    {

    }

    var carTypes = new List<string>()
    {
      "Attack", "DefenseDown", "BuffAttack"
    }
}
