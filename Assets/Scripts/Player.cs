using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    //The player's health; if 0, the player is dead and loses the fight.
    int health = 100;
    int maxHealth = 100;
    //The Player's "actual" mana stat; determines how much mana is restored at the beginning of each turn
    public int maxMana = 5;
    //The Player's mana, for casting cards on the current turn.
    public int mana = 5;
    //Editable defense stat so that cards can add to defense
    public int defense = 0;
    //Currently, just how much the "Attack" card does.  Later, may be a multiplier for the damage dealt by each card?
    int damage = 20;
    //hand size
    int handSize = 0;
    //Player's gold
    public int gold = 0;
    //Reference to the object with the Turns class, which controls when turns are ready.
    [SerializeField]
    public Turns t;
    [SerializeField]
    CardSelection card;
    //GameObject HandResetPrefab;
    //GameObject CurrentHand;
    Hand Hand;
    CardSelection CS;

   


    //UI Elements
    [SerializeField]
    Slider SliderHealth;
    [SerializeField]
    Text HealthValue;
    [SerializeField]
    Text PlayerDefenseValue;
    [SerializeField]
    Text DamageValue;
    [SerializeField]
    Text ManaValue;
    [SerializeField]
    Button EndTurnButton;
    [SerializeField]
    public Button PlayCardButton;
    public Button SelectButton;
    public Button DeselectButton;

    //UI Groups
    public GameObject CombatUI;
    public GameObject TileMoveUI;
    public GameObject StatsUI;



    Animator anim;
    public bool dead = false;

    public event EventHandler<EventBayesian> CardPlayed;  //event handler variable 


    //Creates global instance of the Player; easy way to carry over values into loaded scenes (?)
    //Could try to do a Singleton instead.
    public static Player Instance
    {
        get;
        set;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Finds the Select/Deselect buttons, stores a reference to them, and then sets them inactive;
        //They will be enabled if a Card that is non-exclusive is selected.
        SelectButton = GameObject.Find("Select").GetComponent<Button>();
        DeselectButton = GameObject.Find("Deselect").GetComponent<Button>();
        SelectButton.gameObject.SetActive(false);
        DeselectButton.gameObject.SetActive(false);
        t = FindObjectOfType<Turns>();

        mana = maxMana;
        //This will need to be changed if there are multiple enemies.
        //e = FindObjectOfType<Enemy>();
        card = FindObjectOfType<CardSelection>();
        SliderHealth.value = health;
        anim = GetComponentInChildren<Animator>();

        ////Temporary way of filling the hand
        ////Should NOT be used later.
        ////Card[] c = FindObjectsOfType<Card>();

        ////Finds the first object named "Hand"
        //CurrentHand = GameObject.Find("Hand");

        ////foreach (Card card in c)
        ////{
        ////    card.gameObject.transform.parent = CurrentHand.transform;
        ////}

        ////Sets the "prefab" to a COPY of CurrentHand, then sets it inactive:
        ////Inactive means they are invisible and cannot be interacted with
        //HandResetPrefab = Instantiate(CurrentHand);
        //HandResetPrefab.SetActive(false);
        //Destroy(CurrentHand);
        //CurrentHand = Instantiate(HandResetPrefab);
        //CurrentHand.SetActive(true);

        Hand = FindObjectOfType<Hand>();
        CS = GetComponent<CardSelection>();
        //StartCoroutine(StartTurn());
    }

    // Update is called once per frame
    void Update()
    {
        //Try to do as little coding here as necessary!!
    }


    

    //Called whenever the Player takes damage from any source
    public void TakeDamage(int d)
    {
        //If damage is greater than Player's health + defense, then they're dead.  RIP.
        if (d >= health + defense && !dead)
        {
            t.CamZoomOut();
            dead = true;
            //This is how the Player "Ragdolls" when they die.
            Rigidbody rb = this.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.AddForce(new Vector3(-500f, 400f, 0f));
            rb.AddTorque(new Vector3(5f, 50f, 35f));
        }
        d -= defense;
        if (d < 0)
            d = 0;
        health -= d;
        if (health < 0) { health = 0; }
        SliderHealth.value = health;
        HealthValue.text = health.ToString();
    }

    public void Heal(int v)
    {
        health += v;
        if (health > maxHealth) { health = maxHealth; }
        SliderHealth.value = health;
        HealthValue.text = health.ToString();
    }

    public void BuffDefense(int v)
    {
        Debug.Log("Defense += " + v);
        defense += v;
        Debug.Log("Total defense: " + defense);
        PlayerDefenseValue.text = defense.ToString();
    }

    public void BuffHealth(int v)
    {
        maxHealth += v;
        health += v;
        SliderHealth.value = health;
        HealthValue.text = health.ToString();
    }

    public void BuffMana(int v)
    {
        maxMana += v;
        ManaValue.text = mana.ToString();
    }


    public void EndTurnButtonPressed()
    {
        if (t.PlayerTurn && !dead)
        {
            //This is disabled here, but then re-enabled (if need be) in the Turns class' EndPlayerTurn() Coroutine.
            EndTurnButton.interactable = false;

            //PlayCard will be re-enabled once a card is selected, if the Player has enough Mana; (ManaCheckUI)
            PlayCardButton.interactable = false;
            if (CS.somethingSelected)
                CS.Selected.GetComponent<CardSelectable>().Deselect();
            StartCoroutine(t.EndPlayerTurn());
        }
    }

    public IEnumerator StartTurn() 
    {
        yield return new WaitForSeconds(0.5f);
        //Reset mana to the base stat & update the GUI
        mana = maxMana;
        ManaValue.text = mana.ToString();
        //ResetHand();
        //UpdateHand();
        Hand.RefillHand();

        //Re-enable buttons if the cost can be afforded.
        //if (mana >= 1)
        //    AttackButton.interactable = true;
        //if (mana >= 2)
        //    BuffButton.interactable = true;
        EndTurnButton.interactable = true;
        //PlayCardButton.interactable = true;
        yield return null;
    }


    public void PlayCardButtonPressed()
    {
        //"StartCoroutine" is necessary when you need a function to wait a certain amount of time before finishing.
        //You can think of it as starting a thread, and then waiting for that thread to finish before executing the following lines.
        //The "wait" methods only work inside of the Coroutine methods, which can be identified by the return type of "IEnumerator."
        //The "PlayCardButtonPress" method is necessary because UnityEvents (EX: the Button's OnClick event) cannot trigger IEnumerator methods.
        //This is because the IEnumerator/Coroutine methods must be started by StartCoroutine().
        StartCoroutine(AttackCard());
    }
    public IEnumerator AttackCard()
    {
        Card cardUsed = CS.selected.GetComponent<Card>();
        int manaCost = cardUsed.mana;
        if (t.PlayerTurn && !dead && mana >= manaCost)
        {
            //This "disables" the buttons, so the 'animation' can play.  If the buttons weren't disabled,
            //then the Player could spam the button, and glitch out the animation.
            EndTurnButton.interactable = false;
            PlayCardButton.interactable = false;

            //trigger the Event to tell the Enemies what card has been played by the player

            //"Using" mana.
            mana -= manaCost;
            //Updates the UI mana value
            ManaValue.text = mana.ToString();

            //Triggers the Attack animation to play; this is the transition from both states
            anim.SetTrigger("PlayerAttack");
            //this.transform.Translate(new Vector3(1f, 0f, 0f));

            //Uses the Card's actual action
            cardUsed.Action();
            //Deselects the Card after it's used; can cause Null reference errors otherwise.
            CS.Selected.GetComponent<CardSelectable>().Deselect();

            //This is a "return" for an IEnumerator, however it doesn't actually end the method like a normal return.
            //Instead, this will call the EnemyDelay() method, and then wait for it to finish!
            yield return StartCoroutine(t.EnemyDelay());
            //this.transform.Translate(new Vector3(-1f, 0f, 0f));

            //Triggers the Attack animation again; this is the transition from both states
            anim.SetTrigger("PlayerAttack");
            Hand.CardPlayed(cardUsed);

            //If there are still enemies alive and it's still the Player's Turn, then turn the buttons back on.
            int enemiesAlive = t.EnemiesAlive();
            if (enemiesAlive > 0)
            {
                EndTurnButton.interactable = true;
                //PlayCardButton.interactable = true;
            }

            //If all enemies are dead, Zoom the camera out.
            if (enemiesAlive == 0)
                t.CamZoomOut();
        }
        //This is an empty return for an IEnumerator method.  It does not wait for anything.
        yield return null;

        OnCardPlayed(new EventBayesian(cardUsed));

    }

    //public void UpdateHand() //center = 1, 2.2, -3.2, size of card = abour 4.3 size, will cover an area of 6
    //{

    //   // CurrentHand.draw(FindObjectOfType<CardSelection>());

    //}

    ////Called at the beginning of each turn
    //public void ResetHand()
    //{
    //    //Destroys (rids from memory) the current Hand, and Instantiates (populates memory & calls Start() functions) on the Prefab.
    //    Destroy(CurrentHand);
    //    CurrentHand = Instantiate(HandResetPrefab);
    //    //Sets the Hand active; without this, the Hand will still be invisible and un-interactable.
    //    CurrentHand.SetActive(true);
    //}

    public bool ManaCheckUI()
    {
        Card cardUsed = GetComponent<CardSelection>().selected.GetComponent<Card>();
        int manaCost = cardUsed.mana;
        //Update the Mana Cost number on the PlayCard button
        PlayCardButton.transform.GetChild(1).GetComponentInChildren<Text>().text = manaCost.ToString();
        //PlayCardButton.GetComponentInChildren<Image>().GetComponentInChildren<Text>().text = manaCost.ToString();

        //If the Player can't afford it, disable the button
        if (mana < manaCost)
        {
            PlayCardButton.interactable = false;
            return false;
        }
        //If the Player CAN afford it, enable the button
        else
        {
            PlayCardButton.interactable = true;
            return true;
        }
    }


    public void SelectButtonPressed()
    {
        CS.Selected.GetComponent<SelectionGO>().SelectClicked();
    }

    public void DeselectButtonPressed()
    {
        CS.Selected.GetComponent<SelectionGO>().DeselectClicked();
    }

    public void LoadTileScene()
    {
        SceneManager.LoadScene("TileMovement");
    }

    public void LoadCombatScene()
    {
        SceneManager.LoadScene("Combat");
    }

    public void LoadShopScene()
    {
        SceneManager.LoadScene("Shop");
    }



    protected virtual void OnCardPlayed(EventBayesian e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately after the null check and before the event is raised.
        EventHandler<EventBayesian> raiseEvent = CardPlayed;

        // Event will be null if there are no subscribers
        if (raiseEvent != null)
        {
           
            // Call to raise the event.
            raiseEvent(this, e);
        }
    }

}
