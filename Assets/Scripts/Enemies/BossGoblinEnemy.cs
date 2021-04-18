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
        //SliderHealth.value = health;
        anim = GetComponent<Animator>();
        EnemyDefenseValue.text = defense.ToString();
        HealthValue.text = health.ToString();
        EnemyAttackValue.text = damage.ToString();
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
        if (d - defense >= health && !dead)

        {
            dead = true;
            p.gold += goldValue;
            //Ragdoll effect!
            Rigidbody rb = this.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.AddForce(new Vector3(0f, 400f, 500f));
            rb.AddTorque(new Vector3(5f, 50f, 35f));
            Destroy(this.gameObject, 3f);
        }
        if (defense < d)
            anim.SetTrigger("Block");
            health -= (d - defense);
            defense = 0;
            EnemyDefenseValue.text = defense.ToString();
        if (health < 0)
        {
            health = 0;
        }
        //SliderHealth.value = health;
        HealthValue.text = health.ToString();
    }

    public override void BuffDefense(int v)
    {
        anim.SetTrigger("Power Up");
        defense += v;
        EnemyDefenseValue.text = defense.ToString();
    }
    public override void Defend(int d)
    {
        defense += 20;
        EnemyDefenseValue.text = defense.ToString();

    }

    public override void Buff(int value)
    {
        anim.SetTrigger("Power Up");
        damage += value;
        EnemyAttackValue.text = damage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
