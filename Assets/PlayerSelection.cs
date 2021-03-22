using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    
    [SerializeField] private Button previousButton;

    [SerializeField] private Button nextButton;
    private int currPlayer;
    private void Awake()
    {
        //activate the first player are the beginning
        SelectPlayer(0);
    }
    private void SelectPlayer(int _index)
    {
        //makes previous button disable when on the first player
        previousButton.interactable = (_index != 0);
        //nextbutton disabled when on last car. 
        nextButton.interactable = _index != transform.childCount - 1;
        //loop goes through all the child objects 
        for(int i = 0; i < transform.childCount; i++)
        {
            //Only activating the player gameOject that we need
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
        
    }


    public void ChangePlayer(int _change)
    {
        //everytime a button is pressed it changes the variable by parameter we recieve in PlayerSelction 
        currPlayer += _change;
        SelectPlayer(currPlayer);
        
    }
}
