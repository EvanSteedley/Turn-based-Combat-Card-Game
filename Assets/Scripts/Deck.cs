using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> Cards = new List<Card>();
    public ListOfAllCards AllCards;
    public int initialDeckSize = 5;
    public Hand PlayerHand;
    public Graveyard Graveyard;

    // Start is called before the first frame update
    void Start()
    {
        AllCards = FindObjectOfType<ListOfAllCards>();
        PlayerHand = FindObjectOfType<Hand>();
        Graveyard = FindObjectOfType<Graveyard>();
        //GenerateDeckRandomly();
        GenerateKnightDeck();
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateDeckRandomly()
    {
        Cards = new List<Card>();
        for(int i = 0; i < initialDeckSize; i++)
        {
            Cards.Add(AllCards.DrawRandom());
        }
    }

    public void GenerateKnightDeck()
    {
        Cards = new List<Card>();

        //ArmorUp card
        Cards.Add(AllCards.AllCards[1]);
        //HeavyHand card
        Cards.Add(AllCards.AllCards[8]);
        //DoubleStrike card
        Cards.Add(AllCards.AllCards[3]);
        //Multi-Attack card
        Cards.Add(AllCards.AllCards[9]);
        //CrushingBlow card
        Cards.Add(AllCards.AllCards[2]);
        //Fireball card
        Cards.Add(AllCards.AllCards[10]);
    }

    public int AddCard(Card c)
    {
        Cards.Add(c);
        return Cards.Count;
    }

    public bool RemoveCard(Card c)
    {
        if (Cards.Contains(c))
        {
            Cards.Remove(c);
            return true;
        }
        else
            return false;
    }

    public Card DrawCard()
    {
        if (Cards.Count > 0)
        {
            Card drawn = Cards[0];
            Cards.Remove(drawn);
            return drawn;
        }
        else
        {
            ShuffleDeck();
            if (Cards.Count <= 0)
                return null;
            Card drawn = Cards[0];
            Cards.Remove(drawn);
            return drawn;
            
        }
    }

    public void ShuffleDeck()
    {
        if (Graveyard == null)
            Graveyard = FindObjectOfType<Graveyard>();
        Graveyard.ReturnCardsToDeck();
        for (int i = 0; i < Cards.Count; i++)
        {
            Card temp = Cards[i];
            int r = Random.Range(0, Cards.Count);
            Cards[i] = Cards[r];
            Cards[r] = temp;
        }
    }

}
