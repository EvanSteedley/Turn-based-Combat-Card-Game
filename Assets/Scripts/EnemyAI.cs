using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class EnemyAI : MonoBehaviour
{
    //Reference to the Player - will probably only need one of these.
    [SerializeField]
    Player player;
    [SerializeField]
    Turns t;
    Movement EnemyMovement;
    Movement PlayerMovement;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //This is inefficient!  Calling the Attack through a Coroutine or by any other method is much better.
        //"Once per frame" is VERY OFTEN; around 60 times per second, usually.
        //if (!t.PlayerTurn && !dead)
        //    Attack();

    }


        // ---------------------------------------------
        // ATTACK WHEN PLAYER AND ENEMY ARE CLOSE ENOUGH
        // ---------------------------------------------
        //x1 = p.transform.getposition.x or p.gameObject.transform.position.x; //xcoordinates player
        //y1 = p.transform.getposition.y or p.gameObject.transform.position.y //ycoordinates
        //z1 = p.transform.getposition.z or p.gameObject.transform.position.z //zcoordinates

        //x2 = e.transform.getposition.x or p.gameObject.transform.position.x; //xcoordinates enemy
        //y2 = e.transform.getposition.y or p.gameObject.transform.position.y; //ycoordinates
        //z2 = e.transform.getposition.z or p.gameObject.transform.position.z; //xcoordinates

        //rmin = 10 
        //distsq = (x2-x1)*(x2-x1) + (y2-y1)*(y2-y1) + (z2-z1)*(z2-z1) //square of the distance from the enemy to the player
        // if distsq < ((rmin)*(rmin))  //compare with a minimum distance, if less than minimum distance, the enemy will attack
        //then call to attack function 
        //

        // ----------------------------------------------------
        // ATTACK WHEN PLAYER IS LOITERING AROUND A FIXED POINT
        // ----------------------------------------------------
        // 1. Extract player positions in the last 50 iterations (i.e. frames)
        // 2. Find the average position in these iterations: xAvg = (sum of x)/50, yAvg = (sum of y)/50, zAvg = (sum of z)/50
        // 3. Find standard deviation of (x,y,z) positions from (xAvg,yAvg,zAvg): 
        // stdDev = sqrt(sum( (x-xAvg)^2 + (y-yAvg)^2 + (z-zAvg)^2 ))
        // if stdDev < 5: then attack
   


    public void AStar()
    {
        //x and y values in the grid as x and y 
        //Tile tile1 tile1.x and tile1.y gives the location in the Tile Grid

        var openList = new List<Tile>();
        var closedList = new List<Tile>();

        var path = new List<Tile>(); //append to this every list time for the movement
        EnemyMovement = this.GetComponent<Movement>();
        PlayerMovement = player.GetComponent<Movement>();
        Tile startNode = EnemyMovement.currentTile;
        Tile endNode =  PlayerMovement.currentTile;

        openList.Add(startNode);

        TileMapGenerator TMG = FindObjectOfType<TileMapGenerator>();
        Tile[,] tileGrid = TMG.Tiles;

        int xRightBound = TMG.tileWidth;
        int xLeftBound = 0;
        int yTopBound = TMG.tileLength;
        int yBottomBound = 0;

        startNode.F = 0;
        startNode.G = 0;
        startNode.H = 0;
        endNode.F = 0;
        endNode.G = 0;
        endNode.H = 0;

        int xEnd = PlayerMovement.currentTile.x;
        int yEnd = PlayerMovement.currentTile.y;
        int xStart = EnemyMovement.currentTile.x;
        int yStart = EnemyMovement.currentTile.y;
        Debug.Log("xStart: " + xStart + "\nyStart: " + yStart);
        Debug.Log("xEnd: " + xEnd + "\nyEnd: " + yEnd);

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
            }

            // Remove currentNode from openList
            openList.Remove(currentNode);

            // Add currentNode to closedList
            closedList.Add(currentNode);

            Debug.Log("CurrentNode X = " + currentNode.x + "; CurrentNode Y = " + currentNode.y);

            // If endNode is reached, construct path by tracing parent nodes backwards
            // all the way to startNode; append parent nodes to list "path"
            if (currentNode.x == xEnd && currentNode.y == yEnd)
            {
                Debug.Log("CurrentNode X:" + currentNode.x);
                Debug.Log("CurrentNode Y:" + currentNode.y);
                Debug.Log("OpenList count: " + openList.Count());

                pathCompleted = true;

                while ((currentNode.x != xStart || currentNode.y != yStart) && currentNode != null)
                {
                    Debug.Log("CurrentNode: " + currentNode);
                    path.Add(currentNode);  //appending to path - the nodes are Tiles - the enemy will travel this path
                    if (currentNode.Parent == null)
                        break;
                    currentNode = currentNode.Parent;
                }
            }

            Tile leftNode = null;
            Tile rightNode = null;
            Tile downNode = null;
            Tile upNode = null;

            if (currentNode.x > 0)
            {
                leftNode = tileGrid[currentNode.x - 1, currentNode.y];
                leftNode.Parent = currentNode;
            }
            if (currentNode.x < TMG.tileWidth - 1)
            {
                rightNode = tileGrid[currentNode.x + 1, currentNode.y];
                rightNode.Parent = currentNode;
            }
            if (currentNode.y < TMG.tileLength - 1)
            {
                upNode = tileGrid[currentNode.x, currentNode.y + 1];
                upNode.Parent = currentNode;
            }
            if (currentNode.y > 0)
            {
                downNode = tileGrid[currentNode.x, currentNode.y - 1];
                downNode.Parent = currentNode;
            }

            // Loop over children nodes
            for (int j = 0; j < 4; j++)
            {

                if (j == 0) neighborNode = leftNode;
                if (j == 1) neighborNode = rightNode;
                if (j == 2) neighborNode = upNode;
                else neighborNode = downNode;

                //If neighborNode is null, it was out of bounds - need to break out of the loop.
                if (neighborNode == null)
                    break;

                // Exclude child node if it is in closedList (i.e. already traversed)
                if (closedList.Count > 0 && closedList.Any(t => t.x == neighborNode.x && t.y == neighborNode.y))
                    break;

                bool childFlag = true;

                // Calculate G, H, F values of neighborNode
                neighborNode.G = currentNode.G + Math.Abs((currentNode.x - neighborNode.x)) + Math.Abs((currentNode.y - neighborNode.y));
                neighborNode.H = Math.Abs((neighborNode.x - xEnd)) + Math.Abs((neighborNode.y - yEnd));
                neighborNode.F = neighborNode.G + neighborNode.H;

                // If child node is already in openList, 
                // compare child G value with G values of openList members
                // If child G value is smaller, append again into openList
                for (int k = 0; k < openList.Count; k++)
                {
                    if (openList[k].x == neighborNode.x && openList[k].y == neighborNode.y)
                        if ((neighborNode.G > openList[k].G) && openList[k].G != 0) 
                            childFlag = false;
                }

                if (childFlag == true) openList.Add(neighborNode);

                Debug.Log("j = " + j);
                Debug.Log("OpenList count: " + openList.Count());

            }
        }

        int pathx;
        int pathy;
        int x2 = EnemyMovement.currentTile.x; //x coordinates of the enemy's current position
        int y2 = EnemyMovement.currentTile.y; //y coordinates of the enemy's current position 
        for (int i = path.Count-1; i >= 0; i--)
        {
            Debug.Log("Iteration: " + i);
            pathx = path[i].x;
            pathy = path[i].y;
            if ((pathx - x2) == 1)
                EnemyMovement.MoveRight();
            else if ((pathx - x2) == -1)
                EnemyMovement.MoveLeft();
            else if (((pathy - y2) == 1))
                EnemyMovement.MoveUp();
            else if ((pathy - y2) == - 1)
                EnemyMovement.MoveDown();
            x2 = EnemyMovement.currentTile.x; //since we already have the component assigned
            y2 = EnemyMovement.currentTile.y;
        }

    }


}








