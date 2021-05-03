using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Golem : Enemy
{
    // Start is called before the first frame update
    void Start()
    {

        p = FindObjectOfType<Player>();
        t = FindObjectOfType<Turns>();
        //SliderHealth.value = health;
        anim = GetComponentInChildren<Animator>();
        EnemyDefenseValue.text = defense.ToString();
        HealthValue.text = health.ToString();
        EnemyAttackValue.text = damage.ToString();
        carTypes = new List<string>()
{
  "StrongAttack", "Stun", "BuffHealth"
};
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

}
