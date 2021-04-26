using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : MonoBehaviour
{
    public GameObject itemWithin;
    public Button OpenChestButton;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        itemWithin = FindObjectOfType<Treasure>().PullRandom();
        anim = GetComponent<Animator>();
        if (itemWithin != null)
        {
            OpenChestButton.gameObject.SetActive(true);
            anim.SetBool("Open", false);
        }
        else 
        {
            anim.SetBool("Open", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AwardItem()
    {
        Inventory inv = FindObjectOfType<Inventory>();
        inv.AddItem(itemWithin);
        inv.gameObject.SetActive(false);
        OpenChestButton.gameObject.SetActive(false);
        anim.SetBool("Open", true);

    }
}
