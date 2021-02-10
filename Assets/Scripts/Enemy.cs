using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    //Reference to the Player - will probably only need one of these.
    [SerializeField]
    Player p;
    [SerializeField]
    Turns t;
    //Enemy's health; if <= 0, they die.
    int health = 100;
    //How much damage the Enemy will do on its turn
    int damage = 10;
    public bool dead = false;
    int defense = 0;
    enum states { 
    
        Fight, Defend, Buff
    
    }

    //UI elements.  Will need to be re-done if multiple Enemies.
    [SerializeField]
    Slider SliderHealth;
    [SerializeField]
    Text HealthValue;

    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
        SliderHealth.value = health;
    }

    // Update is called once per frame
    void Update()
    {
     //This is inefficient!  Calling the Attack through a Coroutine or by any other method is much better.
     //"Once per frame" is VERY OFTEN; around 60 times per second, usually.
        //if (!t.PlayerTurn && !dead)
        //    Attack();
        
    }

    public void TakeDamage(int d)
    {
        if (d - defense >= health)     
             
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
        if (health<0) 
		{ 
			health = 0; 
		}
        SliderHealth.value = health;
        HealthValue.text = health.ToString();
    }

    public void Attack()
    {
        p.TakeDamage(damage);
        //This is controlled by the Turns class now.
            //t.PlayerTurn = true;
    }

  
    public void EnemyBehaviour() {


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

    }

    public void Defend(int d)
    {
        defense += 20;
        
    }

    public void Buff()
    {
        
            damage += 20;
    }
}








