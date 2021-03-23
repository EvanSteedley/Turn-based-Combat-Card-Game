using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public ListOfAllCards ListOfAllCards;
    public Deck Deck;
    public List<Card> CardsToBuy;
    public List<Card> InstantiatedCards;
    public int shopSize = 6;
    public Vector3[] positions;
    // Start is called before the first frame update
    void Start()
    {
        ListOfAllCards = FindObjectOfType<ListOfAllCards>();
        Deck = FindObjectOfType<Deck>();
        positions = new Vector3[shopSize];
        positions[0] = new Vector3(-36f, 5.5f, 7.2f);
        positions[1] = new Vector3(-33.25f, 5.5f, 7.2f);
        positions[2] = new Vector3(-30.55f, 5.5f, 7.2f);
        positions[3] = new Vector3(-36f, 1.6f, 7.2f);
        positions[4] = new Vector3(-33.25f, 1.6f, 7.2f);
        positions[5] = new Vector3(-30.55f, 1.6f, 7.2f);
        StartCoroutine(PopulateShop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PopulateShop()
    {
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < shopSize; i++)
        {
            CardsToBuy.Add(ListOfAllCards.DrawRandom());
            Debug.Log("Card Generated: " + CardsToBuy[i].name);
            Card added = Instantiate(CardsToBuy[i]);
            InstantiatedCards.Add(added);
            added.transform.position = positions[i];
            added.transform.eulerAngles = new Vector3(-90f, 0, -90f);
            InstantiatedCards[i].GetComponent<Collider>().enabled = false;
        }
        yield return null;
    }

    public void BuyCard(int i)
    {
        if(i >= 0 && i < CardsToBuy.Count)
        {
            Deck.AddCard(CardsToBuy[i]);
            Card ToDestroy = InstantiatedCards[i];
            Destroy(ToDestroy.gameObject);
        }
    }

    public void RemoveCard(Card c)
    {
        for (int i = 0; i < Deck.Cards.Count; i++)
        {
            if (c.GetType() == Deck.Cards[i].GetType())
            {
                Deck.Cards.RemoveAt(i);
                break;
            }
        }
    }
}
