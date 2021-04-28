using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> playerItems = new List<GameObject>();
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

    public void AddItem(GameObject item)
    {
        GameObject instance = Instantiate(item);
        playerItems.Add(instance);
        instance.GetComponentInChildren<PassiveItem>().Activate();
        instance.GetComponentInChildren<PassiveItem>().transform.SetParent(transform);
        Destroy(instance.gameObject);
    }

}
