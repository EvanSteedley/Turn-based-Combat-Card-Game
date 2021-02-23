using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
u


public class EnemyAI : MonoBehaviour
{
	{
    //Reference to the Player - will probably only need one of these.
    [SerializeField]
    Enemy e;
    [SerializeField]
    Player p;
    [SerializeField]
    Turns t;
    [SerializeField]
    Text EnemyAttackValue;
    [SerializeField]
    Text EnemyDefenseValue;
    //Enemy's health; if <= 0, they die.
    int health = 100;
    //How much damage the Enemy will do on its turn
    int damage = 10;
    public bool dead = false;
    int defense = 0;
    Player p = FindObjectOfType<Player>();
    Enemy e = FindObjectOfType<Enemy>();


    enum states
    {

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
            health -= (d - defense);
        defense = 0;
        if (health < 0)
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



    public void EnemyBehaviour()
    {

        System.Random random = new System.Random();
        int num = random.Next(System.Enum.GetNames(typeof(states)).Length);
        if (num == (int)states.Fight)
        {
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

        // ---------------------------------------------
        // ATTACK WHEN PLAYER AND ENEMY ARE CLOSE ENOUGH
        // ---------------------------------------------
        //x1 = p.transform.getposition.x or p.gameObject.transform.position.x; //xcoordinates player
        //y1 = p.transform.getposition.y or p.gameObject.transform.position.y //ycoordinates
        //z1 = p.transform.getposition.z or p.gameObject.transform.position.z //zcoordinates

        //x2 = e.transform.getposition.x or p.gameObject.transform.position.x; //xcoordinates enemy
        //y2 = e.transform.getposition.y or p.gameObject.transform.position.y; //ycoordinates
        //z2 = e.transform.getposition.z or p.gameObject.transform.position.z; //xcoordinates

        //rmin = 10 
        //distsq = (x2-x1)*(x2-x1) + (y2-y1)*(y2-y1) + (z2-z1)*(z2-z1) //square of the distance from the enemy to the player
        // if distsq < ((rmin)*(rmin))  //compare with a minimum distance, if less than minimum distance, the enemy will attack
        //then call to attack function 
        //

        // ----------------------------------------------------
        // ATTACK WHEN PLAYER IS LOITERING AROUND A FIXED POINT
        // ----------------------------------------------------
        // 1. Extract player positions in the last 50 iterations (i.e. frames)
        // 2. Find the average position in these iterations: xAvg = (sum of x)/50, yAvg = (sum of y)/50, zAvg = (sum of z)/50
        // 3. Find standard deviation of (x,y,z) positions from (xAvg,yAvg,zAvg): 
        // stdDev = sqrt(sum( (x-xAvg)^2 + (y-yAvg)^2 + (z-zAvg)^2 ))
        // if stdDev < 5: then attack
    }

    public void Defend(int d)
    {
        defense += 20;
        EnemyDefenseValue.text = defense.ToString();

    }

    public void Buff()
    {

        damage += 20;
        EnemyAttackValue.text = damage.ToString();
    }



}








