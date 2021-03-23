using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfAllCards : MonoBehaviour
{
    public List<Card> AllCards = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card DrawRandom()
    {
        return AllCards[Random.Range(0, AllCards.Count)];
    }

    public Card DrawByIndex(int i)
    {
        if(i >= 0 && i < AllCards.Count)
            return AllCards[i];
        Debug.Log(i + " is out of the array");
        return null;
    }
}
