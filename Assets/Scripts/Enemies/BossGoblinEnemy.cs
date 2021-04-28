using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGoblinEnemy : Enemy
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
        anim = GetComponent<Animator>();
        EnemyDefenseValue.text = defense.ToString();
        HealthValue.text = health.ToString();
        EnemyAttackValue.text = damage.ToString();
        carTypes = new List<string>() { "Attack", "Poison", "BuffDefense" };
        instanceCards.Add(gameObject.AddComponent<EnemyAttack>());
        instanceCards.Add(gameObject.AddComponent<EnemyStrongAttack>());
        instanceCards.Add(gameObject.AddComponent<EnemyBuffDefense>());
    }

    private void Awake()
    {
        health = 250;
        damage = 40;
        defense = 10;
        goldValue = 750;
    }
    public override void Attack()
    {
        anim.SetTrigger("Attack");
        GameObject Fireball = Instantiate(FireballPrefab);
        Fireball.transform.position = FindObjectOfType<BossGoblinEnemy>().transform.position + Vector3.up;
        LerpTowardsTargets LTT = Fireball.GetComponent<LerpTowardsTargets>();
        LTT.Target = FindObjectOfType<Player>().gameObject;
        LTT.timeToMove = timeToReach;
        p.TakeDamage(damage);
        //This is controlled by the Turns class now.
        //t.PlayerTurn = true;
    }
    public override void TakeDamage(int d)
    {
        base.TakeDamage(d);
        if (defense > d)
            anim.SetTrigger("Block");
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
