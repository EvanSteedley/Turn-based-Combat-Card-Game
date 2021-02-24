using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickToMove : MonoBehaviour
{
    [SerializeField]
    int movesLeft = 3; //How many tiles the player can CURRENTLY move on this turn. (Decremented each time the player moves)
    [SerializeField]
    int movesDefault = 3; //How many tiles the player can move EACH turn.
    public Tile current; //Current tile the player is standing on.
    public Tile finalDestination; //The tile the player has selected, and is trying to move towards.
    TileMapGenerator TMG; //Holds the Grid of Tiles
    TileSelection TS; //Holds the currently-selected tile
    Movement PlayerMovement; //Controls the actual movement of the Player

    // Start is called before the first frame update
    void Start()
    {
        //Assignments
        TMG = FindObjectOfType<TileMapGenerator>();
        TS = FindObjectOfType<TileSelection>();
        PlayerMovement = GetComponentInParent<Movement>();
        PaintReachableTiles();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Highlights all the tiles close enough for the player to move to.
    public void PaintReachableTiles() 
    {
        //Sometimes current is not assigned yet; the order of Start() methods is a little finicky.
        if(current == null)
            current = this.GetComponentInParent<Movement>().currentTile;


        foreach (Tile t in TMG.Tiles)
        {
            //If the Tile is not selected & the difference between its x values, plus the difference between y values
            //is less than or equal to the amount the player can move, then:
            if (t != TS.Selected && (Mathf.Abs(current.x - t.x) + Mathf.Abs(current.y - t.y)) <= movesLeft)
            {
                //The tile is made selectable, and highlighted.
                TileSelectable tempTile = t.GetComponent<TileSelectable>();
                tempTile.ren.material.color = Color.cyan;
                tempTile.enabled = true;
            }
            //If the distance between the tile the Player is on and the current iteration's tile is too great, then:
            else if (t != TS.Selected)
            {
                //The tile is made un-selectable, and returned to its original color.
                TileSelectable tempTile = t.GetComponent<TileSelectable>();
                tempTile.ren.material.color = tempTile.defaultColor;
                tempTile.enabled = false;
            }
        }
    }

    //Simple iterational "Pathfinding" method
    //Basically, draws a straight line to the destination, and tries to move in that direction along the Cardinals
    public void CalculateMove()
    {
        //If the Player is not already on the tile they want to move to, and has enough moves left to move 1 tile:
        if (current != finalDestination && movesLeft > 0)
        {
            //Create a new empty GameObject and set it to the position of the player's current tile
            GameObject GO = new GameObject();
            GO.transform.position = current.transform.position;
            //Use the "LookAt" method to rotate the empty GameObject towards the destination tile
            //This is the "line drawing" part of the method.  A raycast may also work, but other objects could be in the way.
            GO.transform.LookAt(finalDestination.transform);
            //Extract the y-angle from the GameObject's Rotation vector.  This will be the actual "line" from the Player to its destination.
            float angle = GO.transform.localEulerAngles.y;
            //Delete the empty GameObject from memory.  Otherwise, it will fill up the Hiearchy in the Unity Editor.
            Destroy(GO);

            //Debug.Log() messages are the Unity equivalent of a System.err.println() call.
            //I was using it here to make sure the Player was moving in the right direction while watching it in the Editor.
            //Debug.Log(angle);

            //The actual angles are "off" here, because the Tile Grid is rotated for some reason - may need to debug, but it works for now.

            //If the angle is between 225 and 315 (moving UP), and the tile above the current tile exists and is not occupied:
            if (angle >= 225 && angle <= 315 && !TMG.Tiles[current.x - 1, current.y].occupied)
            {
                //Calls the MoveUp() method in the Movement class
                PlayerMovement.MoveUp();
                //decrements movesLeft
                movesLeft--;
                //Sets the current Tile to the Destination tile.
                current = PlayerMovement.destinationTile;
            }
            else if (angle >= 135 && angle < 225 && !TMG.Tiles[current.x, current.y - 1].occupied)
            {
                PlayerMovement.MoveLeft();
                movesLeft--;
                current = PlayerMovement.destinationTile;
            }
            else if (angle >= 45 && angle < 135 && !TMG.Tiles[current.x + 1, current.y].occupied)
            {
                PlayerMovement.MoveDown();
                movesLeft--;
                current = PlayerMovement.destinationTile;
            }
            else if (!TMG.Tiles[current.x, current.y + 1].occupied)
            {
                PlayerMovement.MoveRight();
                movesLeft--;
                current = PlayerMovement.destinationTile;
            }
            
            //Finally, re-paint all the tiles the player can now reach.
            PaintReachableTiles();
        }
    }

    public void MoveToTile() 
    {
        //StartCoroutine(CalculateMove());
    }

    //Called when the End Turn button is pressed; for now it just resets the amount of moves left and re-paints the tiles.
    //Later, it will also move the Enemy, and refresh the player's Hand of cards.
    public void EndTurn()
    {
        movesLeft = movesDefault;
        PaintReachableTiles();
    }
}
