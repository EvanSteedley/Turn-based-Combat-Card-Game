using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDeck : MonoBehaviour
{
    [SerializeField]
    public List<Card> cards = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        var cardList = Resources.Load("CardList");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
