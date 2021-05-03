using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDragonEnemy : Enemy
{
    public List<Player> Targets;
    public GameObject FireballPrefab;
    public float timeToReach = 1f;
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
        carTypes = new List<string>() { "Attack", "StrongAttack", "BuffDefense", "Healing", "DefenseDown" };
        instanceCards.Add(gameObject.AddComponent<EnemyAttack>());
        instanceCards.Add(gameObject.AddComponent<EnemyStrongAttack>());
        instanceCards.Add(gameObject.AddComponent<EnemyBuffDefense>());
    }

    private void Awake()
    {
        health = 500;
        damage = 50;
        defense = 30;
        goldValue = 3000;
    }
    public override void Attack()
    {
        base.Attack();
        anim.SetTrigger("Attack");
        GameObject Fireball = Instantiate(FireballPrefab);
        Fireball.transform.position = transform.position + Vector3.up;
        LerpTowardsTargets LTT = Fireball.GetComponent<LerpTowardsTargets>();
        LTT.Target = p.gameObject;
        LTT.timeToMove = timeToReach;
        //This is controlled by the Turns class now.
        //t.PlayerTurn = true;
    }
    public override void TakeDamage(int d)
    {
        base.TakeDamage(d);
    }

    public override void BuffDefense(int v)
    {
        base.BuffDefense(v);
    }
    public override void Defend(int d)
    {
        base.Defend(d);
    }

    public override void Buff(int value)
    {
        base.Buff(value);
    }

}