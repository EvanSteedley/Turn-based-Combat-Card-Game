using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{
    public List<Card> CurrentHand = new List<Card>();
    public List<Card> InstantiatedCards = new List<Card>();
    public Deck Deck;
    public Graveyard Graveyard;
    public Player p;
    public int HandSize;

    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
        Deck = FindObjectOfType<Deck>();
        Graveyard = FindObjectOfType<Graveyard>();
        DontDestroyOnLoad(this.transform.parent);
        SceneManager.sceneLoaded += ResetHand;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw()
    {
        // c.Transform.setParent();// (this.Transform.parent, false);
        if (CurrentHand.Count <= HandSize)
        {
            if (Deck == null)
                Deck = FindObjectOfType<Deck>();
            Card added = Deck.DrawCard();
            if (added != null)
            {
                CurrentHand.Add(added);
                Card GO = Instantiate(added);
                InstantiatedCards.Add(GO);
                GO.gameObject.SetActive(true);
                GO.transform.parent = this.transform;
                //Displaces the X position by 2 for each card
                GO.transform.localPosition = new Vector3(CurrentHand.Count * 2f, 0, 0);
            }
        }
    }

    public void RefillHand()
    {
        //Uncomment if NOT carrying over any cards between turns!
        //CurrentHand = new List<Card>();
        //for(int i = 0; i < HandSize; i++)
        //{
        //    Draw();
        //}
        
        //Uncomment if we ARE carrying over cards between turns.
        for(int i = CurrentHand.Count; i < HandSize; i++)
        {
            Draw();
        }
        UpdateCardPositions();
    }

    public void CardPlayed(Card c)
    {
        //for(int i = 0; i < CurrentHand.Count; i++)
        //{
        //    if(CurrentHand[i].GetType() == c.GetType())
        //    {
        //        Graveyard.Discard(CurrentHand[i]);
        //        CurrentHand.RemoveAt(i);
        //        InstantiatedCards.RemoveAt(i);
        //        break;
        //    }
        //}
        int i = InstantiatedCards.IndexOf(c);
        Graveyard.Discard(CurrentHand[i]);
        CurrentHand.RemoveAt(i);
        InstantiatedCards.RemoveAt(i);
        UpdateCardPositions();
    }

    public void UpdateCardPositions()
    {
        for (int i = 0; i < CurrentHand.Count; i++)
        {
            //Displaces the X position by 2 for each card
            InstantiatedCards[i].gameObject.transform.localPosition = new Vector3(2 + i * 2f, 0, 0);
            CardSelectable CS = InstantiatedCards[i].GetComponent<CardSelectable>();
            if(CS.originalP != null)
                CS.originalP = new Vector3(2 + i * 2f, 0, 0);
        }
    }

    public void ResetHand(Scene s, LoadSceneMode m)
    {
        CurrentHand = new List<Card>();
        foreach (Card c in InstantiatedCards)
        {
            Destroy(c.gameObject);
        }
        InstantiatedCards = new List<Card>();
    }
}
