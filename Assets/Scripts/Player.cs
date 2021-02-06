using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //The player's health; if 0, the player is dead and loses the fight.
    int health = 100;
    //The Player's "actual" mana stat; determines how much mana is restored at the beginning of each turn
    public int startingMana = 5;
    //The Player's mana, for casting cards on the current turn.
    public int mana;
    //Currently, just how much the "Attack" card does.  Later, may be a multiplier for the damage dealt by each card?
    int damage = 20;
    //A reference to the enemy.  Needs to be updated if fighting multiple enemies - Or maybe set to the "Selection"?
    [SerializeField]
    Enemy e;
    //Reference to the object with the Turns class, which controls when turns are ready.
    [SerializeField]
    Turns t;

    //UI Elements
    [SerializeField]
    Slider SliderHealth;
    [SerializeField]
    Text HealthValue;
    [SerializeField]
    Button AttackButton;
    [SerializeField]
    Button BuffButton;
    [SerializeField]
    Button EndTurnButton;
    [SerializeField]
    Text ManaValue;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        mana = startingMana;
        //This will need to be changed if there are multiple enemies.
        e = FindObjectOfType<Enemy>();
        SliderHealth.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        //Try to do as little coding here as necessary!!
    }
    public void AttackButtonPress()
    {
        //"StartCoroutine" is necessary when you need a function to wait a certain amount of time before finishing.
        //You can think of it as starting a thread, and then waiting for that thread to finish before executing the following lines.
        //The "wait" methods only work inside of the Coroutine methods, which can be identified by the return type of "IEnumerator."
        //The "AttackButtonPress" method is necessary because UnityEvents (EX: the Button's OnClick event) cannot trigger IEnumerator methods.
        //This is because the IEnumerator/Coroutine methods must be started by StartCoroutine().
        StartCoroutine(Attack());
    }
    public IEnumerator Attack()
    {
        int manaCost = 1;
        if (t.PlayerTurn && !dead && mana >= manaCost)
        {
            //This "disables" the buttons, so the 'animation' can play.  If the buttons weren't disabled,
            //then the Player could spam the button, and glitch out the animation.
            AttackButton.interactable = false;
            BuffButton.interactable = false;
            EndTurnButton.interactable = false;

            //"Using" mana.
            mana -= manaCost;
            //Updates the UI mana value
            ManaValue.text = mana.ToString();

            //This is the "animation."   This is not a good way to do animation.
            //Unity has built-in Animator components, but they take time to set up.
            this.transform.Translate(new Vector3(1f, 0f, 0f));
            //This is a "return" for an IEnumerator, however it doesn't actually end the method like a normal return.
            //Instead, this will call the EnemyDelay() method, and then wait for it to finish!
            yield return StartCoroutine(t.EnemyDelay());
            this.transform.Translate(new Vector3(-1f, 0f, 0f));

            e.TakeDamage(damage);

            //If there are still enemies alive, it's still the Player's Turn, and the Player has enough mana, then turn the buttons back on.
            int enemiesAlive = t.EnemiesAlive();
            if (mana >= 1 && t.PlayerTurn && enemiesAlive > 0)
                AttackButton.interactable = true;
            if(mana >= 2 && t.PlayerTurn && enemiesAlive > 0)
                BuffButton.interactable = true;
            if(enemiesAlive > 0)
                EndTurnButton.interactable = true;

            //If all enemies are dead, Zoom the camera out.
            if (enemiesAlive == 0)
                t.CamZoomOut();
        }
        //This is an empty return for an IEnumerator method.  It does not wait for anything.
        yield return null;

    }

    public void TakeDamage(int d)
    {
        if (d >= health)
        {
            t.CamZoomOut();
            dead = true;
            //This is how the Player "Ragdolls" when they die.
            Rigidbody rb = this.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.AddForce(new Vector3(-500f, 400f, 0f));
            rb.AddTorque(new Vector3(5f, 50f, 35f));
        }
        health -= d;
        SliderHealth.value = health;
        HealthValue.text = health.ToString();
    }

    //This method and its IEnumerator Buff() method are similar to the Attack's.
    public void BuffButtonPressed()
    {
        StartCoroutine(Buff());
    }

    public IEnumerator Buff()
    {
        int manaCost = 2;
        if (t.PlayerTurn && !dead && mana >= manaCost)
        {
            AttackButton.interactable = false;
            BuffButton.interactable = false;
            EndTurnButton.interactable = false;
            mana -= manaCost;
            ManaValue.text = mana.ToString();
            this.transform.Translate(new Vector3(1f, 0f, 0f));
            yield return StartCoroutine(t.EnemyDelay());
            this.transform.Translate(new Vector3(-1f, 0f, 0f));
            int enemiesAlive = t.EnemiesAlive();
            if(mana >= 1 && t.PlayerTurn && enemiesAlive > 0)
                AttackButton.interactable = true;
            if(mana >= 2 && t.PlayerTurn && enemiesAlive > 0)
                BuffButton.interactable = true;
            if (enemiesAlive > 0)
                EndTurnButton.interactable = true;
            damage += 30;
        }
    }

    public void EndTurnButtonPressed()
    {
        if (t.PlayerTurn && !dead)
        {
            //These are disabled here, but then re-enabled (if need be) in the Turns class' EndPlayerTurn() Coroutine.
            EndTurnButton.interactable = false;
            AttackButton.interactable = false;
            BuffButton.interactable = false;
            StartCoroutine(t.EndPlayerTurn());
        }
    }

    public void StartTurn() 
    {
        //Reset mana to the base stat & update the GUI
        mana = startingMana;
        ManaValue.text = mana.ToString();

        //Re-enable buttons if the cost can be afforded.
        if(mana >= 1)
            AttackButton.interactable = true;
        if(mana >= 2)
            BuffButton.interactable = true;
        EndTurnButton.interactable = true;

    }

    
}
