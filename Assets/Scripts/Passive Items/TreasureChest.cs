using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : MonoBehaviour
{
    public PassiveItem itemWithin;
    public Button OpenChestButton;

    // Start is called before the first frame update
    void Start()
    {
        itemWithin = FindObjectOfType<Treasure>().PullRandom();
        if(itemWithin != null)
        {
            OpenChestButton.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AwardItem()
    {
        FindObjectOfType<Inventory>().AddItem(itemWithin);
        OpenChestButton.gameObject.SetActive(false);
    }
}
