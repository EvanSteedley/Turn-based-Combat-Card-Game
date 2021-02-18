using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;



public class GenerateDeck : MonoBehaviour
{
    private CardManager cardManager;
    private Card card;
    private StreamWriter fileName = new StreamWriter( "ShawtyWassup.txt");

    // CardManager cardManager = new CardManager();

    IEnumerator Start()
    {
        cardManager = GetComponent<CardManager>();
        yield return new WaitForEndOfFrame();
       
        foreach (Card s in CardManager.cardList)
        {
          
            fileName.WriteLine(s);
            
        }
        fileName.Close();
    }






   // public void GetDeck() { }
    

  //  private  List<Card> cardList = cardManager.getCardList();

}
 /*   // Start is called before the first frame update
    void Start(string cardList)
    {
    var cardList =  Resources.Load(cardList);
    Update();
        
    }

    // Update is called once per frame
    void Update()
    {
    cardList = null;
        
    }
 */

