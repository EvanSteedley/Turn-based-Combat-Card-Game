using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    //Tutorial followed for the Tile Creation (Tile Prefab and basic grid of tiles)
    // https://www.youtube.com/watch?v=3NQnpaSNsKY

    //Wall & Obstacle generation, and Light & Camera movement is mine.
    [SerializeField] GameObject TilePrefab;
    [SerializeField] GameObject WallPrefab;
    [SerializeField] GameObject ObstaclePrefab;

    [SerializeField] Light PointLight;
    [SerializeField] Camera MainCam;
    [SerializeField] Player player;

    [SerializeField] public int tileWidth = 10;
    [SerializeField] public int tileLength = 10;
    [SerializeField] public float tileOffset = 1.1f;

    //Added from the tutorial; makes it easier to manage tile-based movement.
    public Tile[,] Tiles;
    public List<Tile> Obstacles;
    public int numberOfObstacles;

    // Start is called before the first frame update
    void Awake()
    {
        //Instantiate lists
        Tiles = new Tile[tileWidth, tileLength];
        Obstacles = new List<Tile>();

        CreateTileGrid();

        //Set the Light's position to 5 units above the center of the tiles
        PointLight.transform.position = new Vector3(tileWidth / 2, 5, tileLength / 2);
        //Adjust the Light's range to cover all of the tiles
        PointLight.range = (tileWidth + tileLength) * 360 / 20;

        //Adjust camera's position to the center of the tiles, but also adjust its height relative to the amount of tiles
        //Square grids give the best result
        MainCam.transform.position = new Vector3(tileWidth / 2, (tileLength + tileWidth) / 2 + 2f, tileLength / 2);

        //Finds the position of the tile in the middle of the bottom row
        Transform middlepos = Tiles[tileWidth-1, tileLength/2].transform;
        //Moves the Player on top of that tile
        player.transform.position = new Vector3(middlepos.position.x, player.transform.localScale.y, middlepos.transform.position.z);
        //Sets the Player's currentTile to that tile & sets occupied to true
        player.GetComponentInParent<Movement>().currentTile = Tiles[tileWidth - 1, tileLength / 2];
        player.GetComponentInParent<Movement>().currentTile.occupied = true;

        //Adjust number of obstacles based on amount of tiles
        numberOfObstacles = ((tileWidth + tileLength) / 10) + 1;
        PlaceObstacles(numberOfObstacles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTileGrid()
    {
        //Nested for-loop; creates the tiles from the prefab
        for (int x = 0; x < tileWidth; x++)
        {
            for (int y = 0; y < tileLength; y++)
            {
                //Instantiates a Copy of the Tile Prefab, as a child of the GameObject holding this script
                GameObject tempTile = Instantiate(TilePrefab, this.transform) as GameObject;
                //Sets the tile's position to its x & y coordinates, adjusted with the offset
                //Offset = distance between each tile
                tempTile.transform.position = new Vector3(x * tileOffset, 0, y * tileOffset);

                //Adds the tile to the 2D array of tiles at its x & y position
                Tile t = tempTile.GetComponent<Tile>() as Tile;
                Tiles[x, y] = t;
                //tempTile.transform.parent = this.transform;

                //Sets the tile's name to its x & y coordinates, for convenience in the Unity editor
                tempTile.name = x.ToString() + ", " + y.ToString();

                //Sets the tile's x & y values, for the Movement class' use.
                t.x = x;
                t.y = y;


                //Wall creation; instantiates the Wall prefab at the edge of each of the 4 sides of the Tile grid.
                if (x == 0)
                {
                    GameObject tempWall = Instantiate(WallPrefab, this.transform) as GameObject;
                    //The y-value is the WallPrefab's Scale divided by 2 because for cubes, the position (0, 0, 0) is at the direct 
                    //center of the object; (0, y/2, 0) accounts for the object's height; this places it just above each tile, and to the side.
                    tempWall.transform.position = new Vector3(-1 * tileOffset, WallPrefab.transform.localScale.y / 2, y * tileOffset);
                }
                if (y == 0)
                {
                    GameObject tempWall = Instantiate(WallPrefab, this.transform) as GameObject;
                    tempWall.transform.position = new Vector3(x * tileOffset, WallPrefab.transform.localScale.y / 2, -1 * tileOffset);
                }
                if (x == tileWidth-1)
                {
                    GameObject tempWall = Instantiate(WallPrefab, this.transform) as GameObject;
                    tempWall.transform.position = new Vector3((x + 1) * tileOffset, WallPrefab.transform.localScale.y / 2, y * tileOffset);
                }
                if (y == tileLength-1)
                {
                    GameObject tempWall = Instantiate(WallPrefab, this.transform) as GameObject;
                    tempWall.transform.position = new Vector3(x * tileOffset, WallPrefab.transform.localScale.y / 2, (y + 1) * tileOffset);
                }
            }
        }
    }

    //Places Obstacles randomly inside the grid of tiles
    public void PlaceObstacles(int n)
    {
        //n is the number of obstacles to place.
        for(int i = 0; i < n; i++)
        {
            //Randomly generate x & y coords within the grid
            int x = Random.Range(0, tileWidth);
            int y = Random.Range(0, tileLength);
            //If the Tile isn't already occupied by the Player, another obstacle, etc...
            if (!Tiles[x, y].occupied)
            {
                //Occupies that tile, instantiates the Obstacle prefab, and places it on top of the tile.
                Tiles[x, y].occupied = true;
                GameObject obs = Instantiate(ObstaclePrefab, transform);
                //Here, the y-value is just y instead of y/2.  For some reason, Cylinder's center (0, 0, 0) is calculated differently.
                obs.transform.position = Tiles[x, y].transform.position + new Vector3(0f, obs.transform.localScale.y, 0f);
                //Adds the Obstacle to the List of Obstacles.
                Obstacles.Add(Tiles[x, y]);
            }
            //If that Tile was already occupied; reset i to the previous value to try again.
            else
                i--;
        }
    }
}
