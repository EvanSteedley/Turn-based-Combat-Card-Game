using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManaBoost : PassiveItem
{
    bool applied = false;
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Tooltip.transform.position = Input.mousePosition + new Vector3(100f, -60f, 0);
    }

    /// <summary>
    /// Permanently increases the Player's max health by 25.
    /// </summary>
    public void Effect()
    {
        Debug.Log("HealthUp Effect called");
        FindObjectOfType<Player>().BuffMana(3);
        applied = true;
    }

    public override void Activate()
    {
        Effect();
    }
}
