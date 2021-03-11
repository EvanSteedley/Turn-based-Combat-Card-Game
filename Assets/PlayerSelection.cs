using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] private Button previousButton;

    [SerializeField] private Button nextButton;
    private int currPlayer;
    private void SelectPlayer(int _index)
    {
        previousButton.interactable = (_index != 0);
        nextButton.interactable = _index != transform.childCount - 1;
        for(int i = 0; i < transform.childCount; i++)
        {
            //Only activating the player gameOject that we need
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
        
    }


    public void ChangePlayer(int _change)
    {
        currPlayer += _change;
        SelectPlayer(currPlayer);
        
    }
}
