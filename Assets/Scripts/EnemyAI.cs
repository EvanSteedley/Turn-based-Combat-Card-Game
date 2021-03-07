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


    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
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
        Tile startNode = EnemyMovement.currentTile;
        Tile endNode = player.GetComponent<movement>().currentTile;

        openList.Add(startNode);

        TileMapGenerator TMG = FindObjectOfType<TileMapGenerator>();
        Tile[,] tileGrid = TMG.Tiles;

        int xRightBound = TMG.tileWidth;
        int xLeftBound = 0;
        int yTopBound = 0;
        int yBottomBound = TMG.tileLength;

        startNode.F = 0;
        startNode.G = 0;
        startNode.H = 0;
        endNode.F = 0;
        endNode.G = 0;
        endNode.H = 0;

        int xStart = player.GetComponentInParent<Movement>().currentTile.x;
        int yStart = player.GetComponentInParent<Movement>().currentTile.y;
        int xEnd = endNode.X;
        int yEnd = endNode.Y;

        

        Tile currentNode;
        Tile neighborNode;

        //while (openList.Any()) //???
        while (openList.Any())
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

            // If endNode is reached, construct path by tracing parent nodes backwards
            // all the way to startNode; append parent nodes to list "path"
            if (currentNode.X == xEnd && currentNode.Y == yEnd)
            {
                while (currentNode.X != xStart && currentNode.Y != yStart)
                {
                    path.Add(currentNode);  //appending to path - the nodes are Tiles - the enemy will travel this path
                    currentNode = currentNode.Parent;
                }
            }

            // Generate children nodes: left, right, up, down
            //Tile leftNode;
            //leftNode.X = currentNode.X - 1;
            //leftNode.Y = currentNode.Y;
            //leftNode.Parent = currentNode;

            //Tile rightNode;
            //rightNode.X = currentNode.X + 1;
            //rightNode.Y = currentNode.Y;
            //rightNode.Parent = currentNode;

            //Tile upNode;
            //upNode.X = currentNode.X;
            //upNode.Y = currentNode.Y + 1;
            //upNode.Parent = currentNode;

            //Tile downNode;
            //downNode.X = currentNode.X;
            //downNode.Y = currentNode.Y - 1;
            //downNode.Parent = currentNode;


            Tile leftNode = tileGrid[currentNode.X - 1, currentNode.Y];
            leftNode.Parent = currentNode;

            Tile rightNode = tileGrid[currentNode.X + 1, currentNode.Y];
            rightNode.Parent = currentNode;

            Tile upNode = tileGrid[currentNode.X, currentNode.Y + 1];
            upNode.Parent = currentNode;

            Tile downNode = tileGrid[currentNode.X, currentNode.Y - 1];
            downNode.Parent = currentNode;

            // Loop over children nodes
            for (int j = 0; j < 4; j++)
            {
                //if (j == 0) neighborNode = leftNode;
                //if (j == 0) neighborNode = rightNode;
                //if (j == 0) neighborNode = upNode;
                //if (j == 0) neighborNode = downNode;

                if (j == 0) neighborNode = leftNode;
                if (j == 1) neighborNode = rightNode;
                if (j == 2) neighborNode = upNode;
                else neighborNode = downNode;

                bool childFlag = true;

                // Exclude child node if it lies out of maze bounds - this is the array index out of bounds checking
                if (neighborNode.X <= xRightBound && neighborNode.X >= xLeftBound && neighborNode.Y <= yTopBound && neighborNode.Y >= yBottomBound)
                {
                    // Exclude child node if it is in closedList (i.e. already traversed)
                    if (closedList.Any(t => t.X != neighborNode.X && t.Y != neighborNode.Y))
                    {
                        // If child node is already in openList, 
                        // compare child G value with G values of openList members
                        // If child G value is smaller, append again into openList
                        if (openList.Any(t => t.X == neighborNode.X && t.Y == neighborNode.Y))
                        {
                            neighborNode.G = currentNode.G + Mathf.Abs((currentNode.X - neighborNode.X)) + Mathf.Abs((currentNode.Y - neighborNode.Y));
                            neighborNode.H = Mathf.Abs((neighborNode.X - xEnd)) + Mathf.Abs((neighborNode.Y - yEnd));
                            neighborNode.F = neighborNode.G + neighborNode.H;

                            for (int k = 0; k < openList.Count; k++)
                            {
                                if (neighborNode.G > openList[k].G) childFlag = false;
                            }
                        }

                        // If child node has not been traversed before,
                        // append to openList
                        else
                        {
                            if (childFlag == true) openList.Add(neighborNode);
                        }

                    }
                }
            }
        }

        int pathx;
        int pathy;
        int x2 = GetComponentInParent<Movement>().currentTile.transform.position.x; //x coordinates of the enemy's current position
        int y2 = GetComponentInParent<Movement>().currentTile.transform.position.z; //y coordinates of the enemy's current position 

        for (int i = 0; i < path.Count; i++)
        {
            pathx = path[i].x;
            pathy = path[i].y;
            if ((pathx - x2) == 1)
                EnemyMovement.MoveRight();
            else if ((pathx - x2) == -1)
                EnemyMovement.MoveLeft();
            else if (((pathy - y2) == 1))
                EnemyMovement.MoveUp();
            else if ((pathy - y2) == 1)
                EnemyMovement.MoveDown();
            x2 = GetComponentInParent<Movement>().currentTile.x;
            y2 = GetComponentInParent<Movement>().currentTile.y;
        }

    }


}








