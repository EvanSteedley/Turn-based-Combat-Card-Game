using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<PassiveItem> playerItems = new List<PassiveItem>();
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(PassiveItem item)
    {
        playerItems.Add(item);
        Instantiate(item, this.gameObject.transform);
        UpdateUI();
    }

    public void UpdateUI()
    {

    }
}
