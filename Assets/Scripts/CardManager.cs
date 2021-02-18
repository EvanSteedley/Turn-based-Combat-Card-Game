using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//class that contains the list of cards within the game
public class CardManager : MonoBehaviour
{
    public Card card;
    //public Card[] deck;
    // public int index = 0;
    public static List<Card> cardList = new List<Card>();

    // Start is called before the first frame update
    void Start()

    {
        //Card(name, id, mana, damage, defense, element, desciption)
        cardList.Add(new Card("WolfBeak", 0, 1, 2, 3, Card.cardType.Fire,"Card will double the attack on a fire character, and increase damage by 20% if attacked by Fire character"));
        cardList.Add(new Card("RuffRuff", 1, 2, 3, 4, Card.cardType.Fire,"Card will increase health by 20% each time drawn"));
        cardList.Add(new Card("Dragon's Death", 2, 3, 4,5, Card.cardType.Water,"Card boosts attack by 35%"));
        cardList.Add(new Card("Lover's Tail", 3, 4, 5, 6, Card.cardType.Earth,"Card will blocks any damage for one round"));
        cardList.Add(new Card("Breaking Wind", 4, 5, 6, 7, Card.cardType.Wind,"Card doubles the damange of an Earth enemy, increases health by 15%"));
       


    }
    /*public List<Card> getCardList()
     {
         return cardList;
     }*/

}

   

