using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;



public class GenerateDeck : MonoBehaviour
{
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;
    public GameObject card5;
    public GameObject card6;
    public GameObject card7;
    public GameObject card8;
    public GameObject card9;
    public GameObject card10;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        for (int i = 0; i < 5; i++) //fills deck with all 50 cards
        {
            Instantiate(card1, new Vector3(0, 0, -5), Quaternion.identity);
            Instantiate(card2, new Vector3(0, 0, -5), Quaternion.identity);
            Instantiate(card3, new Vector3(0, 0, -5), Quaternion.identity);
            Instantiate(card4, new Vector3(0, 0, -5), Quaternion.identity);
            Instantiate(card5, new Vector3(0, 0, -5), Quaternion.identity);
            Instantiate(card6, new Vector3(0, 0, -5), Quaternion.identity);
            Instantiate(card7, new Vector3(0, 0, -5), Quaternion.identity);
            Instantiate(card8, new Vector3(0, 0, -5), Quaternion.identity);
            Instantiate(card9, new Vector3(0, 0, -5), Quaternion.identity);
            Instantiate(card10, new Vector3(0, 0, -5), Quaternion.identity);
        }
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

