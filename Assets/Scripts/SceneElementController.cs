using System.Collections;
using System.Collections.Generic;
using System;
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
        if (scene.name.Equals("Sample Combat") || scene.name.Equals("Combat"))
        {
            player.CombatUI.SetActive(true);
            player.TileMoveUI.SetActive(false);
            player.StatsUI.SetActive(true);
            player.GetComponent<PlayerClickToMove>().enabled = false;
            player.GetComponent<Movement>().enabled = false;
            player.t = FindObjectOfType<Turns>();
            player.t.TurnEnded += DebugTurnEnded;
            Debug.Log("Combat scene loaded");
        }
        else if (scene.name.Equals("TileMovement"))
        {
            player.CombatUI.SetActive(false);
            player.TileMoveUI.SetActive(true);
            player.StatsUI.SetActive(true);
            PlayerClickToMove PCTM = player.GetComponent<PlayerClickToMove>();
            PCTM.enabled = true;
            player.GetComponent<Movement>().enabled = true;
            PCTM.t = FindObjectOfType<TurnsTile>();
            PCTM.movesLeft = PCTM.movesDefault;
            PCTM.EndTurnButton.interactable = true;
            Debug.Log("Tile scene loaded");
        }
        else if (scene.name.Equals("Shop"))
        {
            player.CombatUI.SetActive(false);
            player.TileMoveUI.SetActive(false);
            player.StatsUI.SetActive(false);
            player.GetComponent<PlayerClickToMove>().enabled = false;
            player.GetComponent<Movement>().enabled = false;
        }
    }

    public void LoadTileMovementScene()
    {
        SceneManager.LoadScene("TileMovement");
    }
    public void LoadCombatScene()
    {
        SceneManager.LoadScene("Combat");
    }

    public void LoadShopScene()
    {
        SceneManager.LoadScene("Shop");
    }

    public void LoadTreasureScene()
    {
        SceneManager.LoadScene("Treasure");
    }

    //Test for EventHandlers;  Receives the sender and its eventArguments as parameters (Can be avoided/changed/added to following this guide:)
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-publish-events-that-conform-to-net-framework-guidelines
    public void DebugTurnEnded(object sender, EventArgs e)
    {
        Debug.Log("TurnEnded triggered in SEC");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
