using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    //Information about the cards
    [SerializeField]
    public string cardName = "";
    [SerializeField]
    public int id = 0;
    [SerializeField]
    public int mana = 0;
    [SerializeField]
    public int damage = 0;
    [SerializeField]
    public int defense = 0;
    [SerializeField]
    public enum cardType {Earth,Wind,Fire,Water,Null}
    public cardType cardElement;
    [SerializeField]
    public string description; 

    public Card(){

    }


   
    public Card(string CardName, int Id, int Mana, int Damage, int Defense,cardType CardElement,string Description)
    {
        cardName = CardName;
        id = Id;
        mana = Mana;
        damage = Damage;
        defense = Defense;
        cardElement = CardElement;
        description = Description;


   
    }

    //  Start is called before the first frame update
    void Start()
    {
        
    }

    /*public GameObject Selected
    {
        get { return this.selected; }
        set { this.selected = value; }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
