using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    public TurnsTile t;
    public List<Tile> path = new List<Tile>(); //append to this every list time for the movement

    [SerializeField]
    public Button EndTurnButton;
    [SerializeField]
    public Button ClickToMoveButton;

    // Start is called before the first frame update
    void Start()
    {
        //Assignments
        TMG = FindObjectOfType<TileMapGenerator>();
        TS = FindObjectOfType<TileSelection>();
        PlayerMovement = GetComponentInParent<Movement>();
        //PaintReachableTiles();
        t = FindObjectOfType<TurnsTile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Highlights all the tiles close enough for the player to move to.
    public void PaintReachableTiles() 
    {
        //Sometimes current is not assigned yet; the order of Start() methods is a little finicky.
        if (current == null || TS == null || TMG == null)
        {
            current = this.GetComponentInParent<Movement>().currentTile;
            TS = GetComponent<TileSelection>();
            TMG = FindObjectOfType<TileMapGenerator>();
        }


        foreach (Tile t in TMG.Tiles)
        {
            //If the Tile is not selected & the difference between its x values, plus the difference between y values
            //is less than or equal to the amount the player can move, then:
            if (t != TS.Selected && (Mathf.Abs(current.x - t.x) + Mathf.Abs(current.y - t.y)) <= movesLeft)
            {
                //The tile is made selectable, and highlighted.
                TileSelectable tempTile = t.GetComponent<TileSelectable>();
                tempTile.ren.material.color = Color.cyan;
                //tempTile.enabled = true;
            }
            //If the distance between the tile the Player is on and the current iteration's tile is too great, then:
            else if (t != TS.Selected)
            {
                //The tile is made un-selectable, and returned to its original color.
                TileSelectable tempTile = t.GetComponent<TileSelectable>();
                tempTile.ren.material.color = tempTile.defaultColor;
                //tempTile.enabled = false;
            }
        }
    }

    public void UnpaintTiles()
    {
        //Sometimes current is not assigned yet; the order of Start() methods is a little finicky.
        if (current == null || TS == null || TMG == null)
        {
            current = this.GetComponentInParent<Movement>().currentTile;
            TS = GetComponent<TileSelection>();
            TMG = FindObjectOfType<TileMapGenerator>();
        }


        foreach (Tile t in TMG.Tiles)
        {
            //If the Tile is not selected & the difference between its x values, plus the difference between y values
            //is less than or equal to the amount the player can move, then:
                TileSelectable tempTile = t.GetComponent<TileSelectable>();
                tempTile.ren.material.color = tempTile.defaultColor;
                //tempTile.enabled = false;
        }
    }

    //Simple iterational "Pathfinding" method
    //Basically, draws a straight line to the destination, and tries to move in that direction along the Cardinals
    public IEnumerator CalculateMove()
    {
        //If the Player is not already on the tile they want to move to, and has enough moves left to move 1 tile:
        if (current != finalDestination && movesLeft > 0)
        {
            ClickToMoveButton.interactable = false;
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

            //If the angle is between 225 and 315 (moving UP), and the tile above the current tile exists and is walkable:
            if (angle >= 225 && angle <= 315 && TMG.Tiles[current.x - 1, current.y].walkable)
            {
                //Calls the MoveUp() method in the Movement class
                PlayerMovement.MoveUp();
                //decrements movesLeft
                movesLeft--;
                //Sets the current Tile to the Destination tile.
                current = PlayerMovement.destinationTile;
            }
            else if (angle >= 135 && angle < 225 && TMG.Tiles[current.x, current.y - 1].walkable)
            {
                PlayerMovement.MoveLeft();
                movesLeft--;
                current = PlayerMovement.destinationTile;
            }
            else if (angle >= 45 && angle < 135 && TMG.Tiles[current.x + 1, current.y].walkable)
            {
                PlayerMovement.MoveDown();
                movesLeft--;
                current = PlayerMovement.destinationTile;
            }
            else if (TMG.Tiles[current.x, current.y + 1].walkable)
            {
                PlayerMovement.MoveRight();
                movesLeft--;
                current = PlayerMovement.destinationTile;
            }

            //Finally, re-paint all the tiles the player can now reach.
            //PaintReachableTiles();
            float elapsedTime = 0f;
            while (elapsedTime < PlayerMovement.timeToMove)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            ClickToMoveButton.interactable = true;
        }
    }

    public IEnumerator AStar()
    {
        ClickToMoveButton.interactable = false;
        //x and y values in the grid as x and y 
        //Tile tile1 tile1.x and tile1.y gives the location in the Tile Grid

        var openList = new List<Tile>();
        var closedList = new List<Tile>();

        this.PlayerMovement = GetComponent<Movement>();
        Tile startNode = PlayerMovement.currentTile;
        Tile endNode = finalDestination;

        path = new List<Tile>(); //append to this every list time for the movement

        openList.Add(startNode);

        Tile[,] tileGrid = TMG.Tiles;

        int xRightBound = TMG.tileWidth;
        int xLeftBound = 0;
        int yTopBound = TMG.tileLength;
        int yBottomBound = 0;

        startNode.F = 0;
        startNode.G = 0;
        startNode.H = 0;
        //endNode.F = 0;
        //endNode.G = 0;
        //endNode.H = 0;
        startNode.Parent = null;
        int xEnd = finalDestination.x;
        int yEnd = finalDestination.y;
        int xStart = PlayerMovement.currentTile.x;
        int yStart = PlayerMovement.currentTile.y;
        //Debug.Log("xStart: " + xStart + "\nyStart: " + yStart);
        //Debug.Log("xEnd: " + xEnd + "\nyEnd: " + yEnd);

        Tile currentNode;
        Tile neighborNode;

        bool pathCompleted = false;
        while (openList.Count > 0 && pathCompleted == false)
        {
            // Loop over all openlist elements to identify node with min F as currentNode
            currentNode = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].F < currentNode.F) currentNode = openList[i];
                //yield return null;
            }

            // Remove currentNode from openList
            openList.Remove(currentNode);

            // Add currentNode to closedList
            closedList.Add(currentNode);

            //Debug.Log("CurrentNode X = " + currentNode.x + "; CurrentNode Y = " + currentNode.y);

            // If endNode is reached, construct path by tracing parent nodes backwards
            // all the way to startNode; append parent nodes to list "path"
            if (currentNode.x == xEnd && currentNode.y == yEnd)
            {
                //Debug.Log("END NODE FOUND - making path");

                pathCompleted = true;

                while ((currentNode.x != xStart || currentNode.y != yStart) && currentNode != null)
                {
                    //Debug.Log("CurrentNode: " + currentNode);
                    //yield return null;
                    path.Add(currentNode);  //appending to path - the nodes are Tiles - the enemy will travel this path
                    if (currentNode.Parent == null)
                        break;
                    //if (currentNode == currentNode.Parent.Parent)
                    //    break;
                    currentNode = currentNode.Parent;
                }
            }

            Tile leftNode = null;
            Tile rightNode = null;
            Tile downNode = null;
            Tile upNode = null;

            if (currentNode.x > 0 && !closedList.Any(t => t.x == (currentNode.x - 1) && t.y == currentNode.y) && !openList.Any(t => t.x == (currentNode.x - 1) && t.y == currentNode.y))
            {
                //Debug.Log("Left neighbor added");
                leftNode = tileGrid[currentNode.x - 1, currentNode.y];
                leftNode.Parent = currentNode;
            }
            if (currentNode.x < TMG.tileWidth - 1 && !closedList.Any(t => t.x == (currentNode.x + 1) && t.y == currentNode.y) && !openList.Any(t => t.x == (currentNode.x + 1) && t.y == currentNode.y))
            {
                //Debug.Log("Right neighbor added");
                rightNode = tileGrid[currentNode.x + 1, currentNode.y];
                rightNode.Parent = currentNode;
            }
            if (currentNode.y < TMG.tileLength - 1 && !closedList.Any(t => t.x == currentNode.x && t.y == (currentNode.y + 1)) && !openList.Any(t => t.x == (currentNode.x) && t.y == currentNode.y + 1))
            {
                //Debug.Log("Up neighbor added");
                upNode = tileGrid[currentNode.x, currentNode.y + 1];
                upNode.Parent = currentNode;
            }
            if (currentNode.y > 0 && !closedList.Any(t => t.x == currentNode.x && t.y == (currentNode.y - 1)) && !openList.Any(t => t.x == (currentNode.x) && t.y == currentNode.y - 1))
            {
                //Debug.Log("Down neighbor added");
                downNode = tileGrid[currentNode.x, currentNode.y - 1];
                downNode.Parent = currentNode;
            }

            // Loop over children nodes
            for (int j = 0; j < 4; j++)
            {
                neighborNode = null;
                //yield return null;
               // Debug.Log("j = " + j);
                if (j == 0)
                {
                    //Debug.Log("Traversing Left neighbor");
                    neighborNode = leftNode;
                }
                else if (j == 1)
                {
                    //Debug.Log("Traversing Right neighbor");
                    neighborNode = rightNode;
                }
                else if (j == 2)
                {
                    //Debug.Log("Traversing Up neighbor");
                    neighborNode = upNode;
                }
                else if (j == 3)
                {
                   // Debug.Log("Traversing Down neighbor");
                    neighborNode = downNode;
                }

                //If neighborNode is null, it was out of bounds - need to continue through the loop.
                if (neighborNode == null)
                    continue;
                //Debug.Log("NeighborNode X = " + neighborNode.x + "; NeighborNode Y = " + neighborNode.y);
                // Exclude child node if it is in closedList (i.e. already traversed)
                if (closedList.Count > 0 && closedList.Any(t => t.x == neighborNode.x && t.y == neighborNode.y))
                    continue;

                bool childFlag = true;

                // Calculate G, H, F values of neighborNode
                neighborNode.G = currentNode.G + 1; //It'll only ever be 1 greater since it's already just a neighbor
                neighborNode.H = (int)Mathf.Round(Mathf.Pow(Mathf.Abs(neighborNode.x - endNode.x), 2) + Mathf.Pow(Mathf.Abs(neighborNode.y - endNode.y), 2));
                neighborNode.F = neighborNode.G + neighborNode.H;
               // Debug.Log("F, G, H:  " + neighborNode.F + ", " + neighborNode.G + ", " + neighborNode.H);

                if (neighborNode.walkable == false)
                {
                    neighborNode.G = 100000;
                    neighborNode.H = 100000;
                    neighborNode.F = 100000;
                }


                // If child node is already in openList, 
                // compare child G value with G values of openList members
                // If child G value is smaller, append again into openList
                for (int k = 0; k < openList.Count; k++)
                {
                    //yield return null;
                    if (openList[k].x == neighborNode.x && openList[k].y == neighborNode.y)
                        if ((neighborNode.G > openList[k].G) && openList[k].G != 0)
                            childFlag = false;
                }

                if (childFlag == true) openList.Add(neighborNode);

            }
        }
        PaintPath();
        ClickToMoveButton.interactable = true;
        yield return null;
    }

    public void PaintPath()
    {
        for (int i = path.Count - 1; i >= 0; i--)
        {
            if(path[i].G > movesLeft)
            {
                Debug.Log("G value = " + path[i].G);
                path[i].GetComponent<TileSelectable>().ren.material.color = Color.red;
            }
            else
            {
                Debug.Log("G value = " + path[i].G);
                path[i].GetComponent<TileSelectable>().ren.material.color = Color.green;
            }
        }
    }

    public IEnumerator MoveToTile() 
    {
        ClickToMoveButton.interactable = false;
        EndTurnButton.interactable = false;
        int pathx;
        int pathy;
        int x2 = PlayerMovement.currentTile.x; //x coordinates of the enemy's current position
        int y2 = PlayerMovement.currentTile.y; //y coordinates of the enemy's current position 
        for (int i = path.Count - 1; i >= 0; i--)
        {
            //yield return null;
            //Debug.Log("Iteration: " + i);
            pathx = path[i].x;
            pathy = path[i].y;
            //Debug.Log("Pathx, x2:  " + pathx + ", " + x2);
            //Debug.Log("Pathy, y2:  " + pathy + ", " + y2);
            if ((pathx - x2) == 1 && movesLeft > 0)
            {
                PlayerMovement.MoveDown();
                movesLeft--;
                yield return new WaitForSeconds(PlayerMovement.timeToMove + .05f);
            }
            else if ((pathx - x2) == -1 && movesLeft > 0)
            {
                movesLeft--;
                PlayerMovement.MoveUp();
                yield return new WaitForSeconds(PlayerMovement.timeToMove + .05f);
            }
            else if (((pathy - y2) == 1) && movesLeft > 0)
            {
                movesLeft--;
                PlayerMovement.MoveRight();
                yield return new WaitForSeconds(PlayerMovement.timeToMove + .05f);
            }
            else if ((pathy - y2) == -1 && movesLeft > 0)
            {
                movesLeft--;
                PlayerMovement.MoveLeft();
                yield return new WaitForSeconds(PlayerMovement.timeToMove + .05f);
            }
            x2 = PlayerMovement.currentTile.x; //since we already have the component assigned
            y2 = PlayerMovement.currentTile.y;
        }
        path = new List<Tile>();
        UnpaintTiles();
        ClickToMoveButton.interactable = true;
        EndTurnButton.interactable = true;
    }

    //Called when the End Turn button is pressed; for now it just resets the amount of moves left and re-paints the tiles.
    //Later, it will also move the Enemy, and refresh the player's Hand of cards.
    public void StartTurn()
    {
        movesLeft = movesDefault;
        //PaintReachableTiles();
    }

    public void EndTurnButtonPressed()
    {
        if (t.PlayerTurn) // && !GetComponent<Player>().dead)
        {
            UnpaintTiles();
            //This is disabled here, but then re-enabled (if need be) in the TurnsTile class' EndPlayerTurn() Coroutine.
            EndTurnButton.interactable = false;
            ClickToMoveButton.interactable = false;
            StartCoroutine(t.EndPlayerTurn());
        }
    }

    public void ClickToMoveButtonPressed()
    {

        StartCoroutine(MoveToTile());

    }
}
