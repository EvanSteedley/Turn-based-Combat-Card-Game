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
    //Amount of gold to drop when killed
    public int goldValue = 100;
    public bool isStunned = false;
    

    public List <String> carTypes = new List<string>() { };  //possible card types 
    public List<String> cardstoPlay = new List<string>() { }; //cards that could be played on the next turn
    public List<EnemyCard> instanceCards = new List<EnemyCard>();

    public EnemyTable ET;


    enum states { 
    
        Fight, Defend, Buff, Poison, LowerPlayerDefense, Stun
    
    }

    


    [SerializeField]
    public Text HealthValue;

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
        p.CardPlayed += HandleCardPlayed;  //subscribing the handle card played to the publisher
        //HandleCardPlayed is a delegate
        ET = FindObjectOfType<EnemyTable>();

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
            p.gold += goldValue;
            p.CardPlayed -= HandleCardPlayed;
            StatusEffects[] se = GetComponentsInChildren<StatusEffects>();
            //Unsubscribes all status effects
            foreach (StatusEffects s in se)
            {
                t.TurnEnded -= s.Action;
            }
            //Ragdoll effect!
            Rigidbody rb = this.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.AddForce(new Vector3(0f, 400f, 500f));
            rb.AddTorque(new Vector3(5f, 50f, 35f));
            Destroy(this.gameObject, 3f);
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

    public virtual void BuffDefense(int v)
    {
        defense += v;
        EnemyDefenseValue.text = defense.ToString();
    }

    public void BuffHealth(int v)
    {

        health += v;
        HealthValue.text = health.ToString();

        //leave this BuffHealth method empty for now for the Enemy - come back to this later
    }

    public void Heal(int v)
    {

        health += v;
        HealthValue.text = health.ToString();

        //leave this BuffHealth method empty for now for the Enemy - come back to this later
    }

    virtual public void Attack()
    {
        p.TakeDamage(damage);
        //This is controlled by the Turns class now.
            //t.PlayerTurn = true;
    }


    //virtual public void EnemyBehaviour() {


    //    System.Random random = new System.Random();
    //    int num = random.Next(System.Enum.GetNames(typeof(states)).Length);
    //    if (num == (int)states.Fight) {
    //       Attack();
    //    }

    //    if (num == (int)states.Defend)
    //    {
    //        Defend(1);
    //    }

    //    if (num == (int)states.Buff)
    //    {
    //       Buff(2);
    //    }

    //    if(num == (int)states.Poison)
    //    {
    //        p.gameObject.AddComponent<Poison>();
    //    }
    //    if (num == (int)states.LowerPlayerDefense)
    //    {
    //        p.gameObject.AddComponent<DefenseDown>();
    //    }



    //}

    virtual public void Defend(int d)
    {
        defense += 20;
        EnemyDefenseValue.text = defense.ToString();

    }

    virtual public void EnemyBehaviour()
    {

        ET = FindObjectOfType<EnemyTable>();
        String cardString = "";
        if (cardstoPlay.Count > 0)
            cardString = cardstoPlay[UnityEngine.Random.Range(0, cardstoPlay.Count)];
        if (cardString == "")
        {
            foreach (EnemyCard ec in instanceCards)
            {
                Debug.Log("Card: " + ec.cardName);
            }
            int rand = UnityEngine.Random.Range(0, instanceCards.Count);
            Debug.Log("Card played: " + instanceCards[rand].cardName);
            instanceCards[rand].Action();
            //figure out which card to play from this list of strings and then play it 
            //within the EnemyBehahviour method
            //find a way to convert to actual Card instead of the string
            //helper method/separate class which has access to all the Enemy Cards - List of Enemy Cards
            //foreach loop to go through all the list of cards, then find which cards match
            //send the match to the Enemy class then the Card will be played by the Enemy
            //each card has a CardName variable and see if they match
            //for the Enemy attack, within the Start method - cardName = "Attack"
            //do it for all the 9 cards

            //Defense/DefenseDown do the same thing - so delete from everywhere

            //alternatively we can create local list for all possible cards (that match the card types list)
            //within the Enemy class itself
            //instantiate those cards and call the Action methods 
        }
        else
        {
            bool cardPlayed = false;
            foreach(EnemyCard ec in instanceCards)
            {
                if(ec.cardName == cardString)
                {
                    Debug.Log("Card played from Table: " + ec.cardName);
                    cardPlayed = true;
                    ec.Action();
                    break;
                }
            }

            //In case the chosen card somehow doesn't match any of the instance Cards
            if(!cardPlayed)
            {
                int rand = UnityEngine.Random.Range(0, instanceCards.Count);
                Debug.Log("Card played: " + instanceCards[rand].cardName);
                instanceCards[rand].Action();
            }

            cardstoPlay = new List<String>(); //refresh the cardstoPlay list after the enemy takes its turn 
            //because we want the enemy to respond to what is played by the Player only on current turn
        }


        //System.Random random = new System.Random();
        //int num = random.Next(System.Enum.GetNames(typeof(states)).Length);
        //if (num == (int)states.Fight)
        //{
        //    Attack();
        //}

        //if (num == (int)states.Defend)
        //{
        //    Defend(1);
        //}

        //if (num == (int)states.Buff)
        //{
        //    Buff(2);
        //}

        //if (num == (int)states.Poison)
        //{
        //    p.gameObject.AddComponent<Poison>();
        //}
        //if (num == (int)states.LowerPlayerDefense)
        //{
        //    p.gameObject.AddComponent<DefenseDown>();
        //}



    }



    virtual public void Buff(int value)
    {
        
            damage += value;
        EnemyAttackValue.text = damage.ToString();
    }


    //make a method in this class that takes cards 
    public void HandleCardPlayed(object sender, EventBayesian e)
    {
        Debug.Log(e.Message.name);
        Debug.Log(carTypes);
        Debug.Log(ET);
        String response = ET.GetResponseCard(carTypes, e.Message);
        if(response != null && response != "")
            cardstoPlay.Add(response);

    }  
        //Call Bayesian table with card played

        //this method should call whatever method from BayesianCardTable 
        //Enemy class has the list of all possible enemy cards - which it then sends to the BayesianCardTable (list # 1)
        //based on the probabilities, the BayesianCardTable sends a list of whatever possible cards the enemy can play
        //in response to the player (list # 2)
        //random number generator chooses what final card the enemy will play, from this list
        //if none of the cards are available/chosen for whatever reason, choose one at random from list # 1
        //if none of the options work, return null
    
}








