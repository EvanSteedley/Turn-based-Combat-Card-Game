using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneElementController : MonoBehaviour
{

    //Variables
    Player player;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneLogic;
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SceneLogic(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Sample Combat"))
        {
            player.CombatUI.SetActive(true);
            player.TileMoveUI.SetActive(false);
            Debug.Log("Combat scene loaded");
            player.GetComponent<PlayerClickToMove>().enabled = false;
            player.GetComponent<Movement>().enabled = false;
            player.t = FindObjectOfType<Turns>();
        }
        else if (scene.name.Equals("TileMovement"))
        {
            player.CombatUI.SetActive(false);
            player.TileMoveUI.SetActive(true);
            Debug.Log("Tile scene loaded");
            player.GetComponent<PlayerClickToMove>().enabled = true;
            player.GetComponent<Movement>().enabled = true;
            player.GetComponent<PlayerClickToMove>().t = FindObjectOfType<TurnsTile>();
        }
    }
}
