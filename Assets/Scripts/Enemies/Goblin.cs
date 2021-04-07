using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
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



    List<string> listProbValues = new List<string>();
    float PTot = 0.0;

    var carTypes = new List<string>()
    {
      "Attack", "DefenseDown", "BuffAttack"
    }

    var allCards = new List<string>()
    {
      "Attack", "DefenseDown", "BuffAttack", "Stun", "BuffHealth", "Poison"
    }

    for (var i in carTypes)
    {
        for (var j in allCards)
        {
            if (i == j) 
            {
                PTot = PTot + listA[j];
                listProbValues.Add(PTot);
            }
        }
    }

    float r = Random[0, PTot];

    var selectedCard = null;
    for(int k = 0; k<=listProbValues.count(); k++)
    { 
        if (r <= listProbValues[k])
        {
            selectedCard = carTypes[k];
            break;
        }
    }

}
