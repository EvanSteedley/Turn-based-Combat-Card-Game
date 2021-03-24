using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deck : MonoBehaviour
{
    public List<Card> CurrentDeck = new List<Card>();
    public List<Card> FullDeck = new List<Card>();
    public ListOfAllCards AllCards;
    public int initialDeckSize = 24;
    public Hand PlayerHand;
    public Graveyard Graveyard;

    // Start is called before the first frame update
    void Start()
    {
        AllCards = FindObjectOfType<ListOfAllCards>();
        PlayerHand = FindObjectOfType<Hand>();
        Graveyard = FindObjectOfType<Graveyard>();
        GenerateDeckRandomly();
        //GenerateKnightDeck();
        //DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += ResetDeck;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateDeckRandomly()
    {
        CurrentDeck = new List<Card>();
        for(int i = 0; i < initialDeckSize; i++)
        {
            Card added = AllCards.DrawRandom();
            CurrentDeck.Add(added);
            FullDeck.Add(added);
        }
    }

    public void GenerateKnightDeck()
    {
        CurrentDeck = new List<Card>();

        //ArmorUp card
        CurrentDeck.Add(AllCards.DrawByIndex(0));
        FullDeck.Add(AllCards.DrawByIndex(0));
        //HeavyHand card
        CurrentDeck.Add(AllCards.DrawByIndex(7));
        FullDeck.Add(AllCards.DrawByIndex(7));
        //DoubleStrike card
        CurrentDeck.Add(AllCards.DrawByIndex(2));
        FullDeck.Add(AllCards.DrawByIndex(2));
        //Multi-Attack card
        CurrentDeck.Add(AllCards.DrawByIndex(8));
        FullDeck.Add(AllCards.DrawByIndex(8));
        //CrushingBlow card
        CurrentDeck.Add(AllCards.DrawByIndex(1));
        FullDeck.Add(AllCards.DrawByIndex(1));
        //Fireball card
        CurrentDeck.Add(AllCards.DrawByIndex(9));
        FullDeck.Add(AllCards.DrawByIndex(9));
    }

    public int AddCard(Card c)
    {
        CurrentDeck.Add(c);
        FullDeck.Add(c);
        return CurrentDeck.Count;
    }

    public bool RemoveCard(Card c)
    {
        if (CurrentDeck.Contains(c))
        {
            CurrentDeck.Remove(c);
            FullDeck.Remove(c);
            return true;
        }
        else
            return false;
    }

    public bool RemoveCardByIndex(int i)
    {
        if(i >= 0 && i < FullDeck.Count)
        {
            Card removed = FullDeck[i];
            FullDeck.RemoveAt(i);
            for(int k = 0; k < CurrentDeck.Count; k++)
            {
                if(CurrentDeck[k].name.Equals(removed.name))
                {
                    CurrentDeck.RemoveAt(k);
                    return true;
                }
            }
            Debug.Log("Card missing from Deck - in Hand?");
        }
        return false;
    }

    public Card DrawCard()
    {
        if (CurrentDeck.Count > 0)
        {
            Card drawn = CurrentDeck[0];
            CurrentDeck.Remove(drawn);
            return drawn;
        }
        else
        {
            ShuffleDeck();
            if (CurrentDeck.Count <= 0)
                return null;
            Card drawn = CurrentDeck[0];
            CurrentDeck.Remove(drawn);
            return drawn;
            
        }
    }

    public void ShuffleDeck()
    {
        if (Graveyard == null)
            Graveyard = FindObjectOfType<Graveyard>();
        Graveyard.ReturnCardsToDeck();
        for (int i = 0; i < CurrentDeck.Count; i++)
        {
            Card temp = CurrentDeck[i];
            int r = Random.Range(0, CurrentDeck.Count);
            CurrentDeck[i] = CurrentDeck[r];
            CurrentDeck[r] = temp;
        }
    }

    public void ResetDeck(Scene s, LoadSceneMode m)
    {
        CurrentDeck = new List<Card>();
        foreach (Card c in FullDeck)
        {
            CurrentDeck.Add(c);
        }
    }

}
