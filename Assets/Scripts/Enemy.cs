using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Enemy : MonoBehaviour
{
    //Reference to the Player - will probably only need one of these.
    [SerializeField]
    public Player p;
    [SerializeField]
    public Turns t;
    [SerializeField]
    public Text EnemyAttackValue;
    [SerializeField]
    public Text EnemyDefenseValue;
    public Animator anim;
    //Enemy's health; if <= 0, they die.
    public int health = 100;
    //How much damage the Enemy will do on its turn
    public int damage = 10;
    public bool dead = false;
    public int defense = 0;
    enum states { 
    
        Fight, Defend, Buff, Poison, LowerPlayerDefense
    
    }


    [SerializeField]
    public Text HealthValue;

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

    // Update is called once per frame
    void Update()
    {
     //This is inefficient!  Calling the Attack through a Coroutine or by any other method is much better.
     //"Once per frame" is VERY OFTEN; around 60 times per second, usually.
        //if (!t.PlayerTurn && !dead)
        //    Attack();
        
    }

    virtual public void TakeDamage(int d)
    {
        if (d - defense >= health && !dead)     
             
        {
            dead = true;
            //Ragdoll effect!
            Rigidbody rb = this.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.AddForce(new Vector3(500f, 400f, 0f));
            rb.AddTorque(new Vector3(5f, 50f, 35f));
        }
        if (defense < d)
			health -= (d-defense);
        defense = 0;
        EnemyDefenseValue.text = defense.ToString();
        if (health<0) 
		{ 
			health = 0; 
		}
        //SliderHealth.value = health;
        HealthValue.text = health.ToString();
    }

    public void BuffDefense(int v)
    {
        defense += v;
        EnemyDefenseValue.text = defense.ToString();
    }

    public void BuffHealth(int v)
    {
    
      //leave this BuffHealth method empty for now for the Enemy - come back to this later
    }

    virtual public void Attack()
    {
        p.TakeDamage(damage);
        //This is controlled by the Turns class now.
            //t.PlayerTurn = true;
    }

  
    virtual public void EnemyBehaviour() {


        System.Random random = new System.Random();
        int num = random.Next(System.Enum.GetNames(typeof(states)).Length);
        if (num == (int)states.Fight) {
           Attack();
        }

        if (num == (int)states.Defend)
        {
            Defend(1);
        }

        if (num == (int)states.Buff)
        {
           Buff();
        }

        if(num == (int)states.Poison)
        {
            p.gameObject.AddComponent<Poison>();
        }
        if (num == (int)states.LowerPlayerDefense)
        {
            p.gameObject.AddComponent<Defense>();
        }



    }

    virtual public void Defend(int d)
    {
        defense += 20;
        EnemyDefenseValue.text = defense.ToString();
        
    }

    virtual public void Buff()
    {
        
            damage += 20;
        EnemyAttackValue.text = damage.ToString();
    }

    
}








