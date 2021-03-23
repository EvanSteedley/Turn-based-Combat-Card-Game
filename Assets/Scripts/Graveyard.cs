using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    public List<Card> DiscardedCards = new List<Card>();
    public Deck Deck;

    // Start is called before the first frame update
    void Start()
    {
        Deck = FindObjectOfType<Deck>();
       //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnCardsToDeck()
    {
        foreach (Card c in DiscardedCards)
        {
            Deck.AddCard(c);
        }
        DiscardedCards = new List<Card>();
    }

    public void Discard(Card c)
    {
        DiscardedCards.Add(c);
    }
}
