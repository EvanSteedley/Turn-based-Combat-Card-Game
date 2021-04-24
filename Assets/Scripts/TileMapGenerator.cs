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
    [SerializeField] GameObject ShopTileFloater;
    [SerializeField] GameObject TreasureTileFloater;
    [SerializeField] GameObject CombatTileFloater;
    [SerializeField] GameObject BossTileFloater;

    [SerializeField] Light PointLight;
    [SerializeField] Camera MainCam;
    [SerializeField] Player player;
    [SerializeField] Enemy enemy;

    [SerializeField] public int tileWidth = 10;
    [SerializeField] public int tileLength = 10;
    [SerializeField] public float tileOffset = 2.1f;

    //Added from the tutorial; makes it easier to manage tile-based movement.
    public Tile[,] Tiles;
    public List<Tile> Obstacles;
    public int numberOfObstacles;
    public List<Enemy> enemies = new List<Enemy>();
    public SceneElementController SEC;

    // Start is called before the first frame update
    void Awake()
    {
        //Instantiate lists
        Tiles = new Tile[tileWidth, tileLength];
        Obstacles = new List<Tile>();

        player = FindObjectOfType<Player>();
        SEC = FindObjectOfType<SceneElementController>();
        List<Enemy> enemiesToAdd = FindObjectOfType<EnemySpawner>().SpawnEnemies(UnityEngine.Random.Range(1, 3));

        CreateTileGrid();

        //Set the Light's position to 5 units above the center of the tiles
        PointLight.transform.position = new Vector3(tileWidth / 2, 5, tileLength / 2);
        //Adjust the Light's range to cover all of the tiles
        PointLight.range = (tileWidth + tileLength) * 360 / 20;

        //Adjust camera's position to the center of the tiles, but also adjust its height relative to the amount of tiles
        //Square grids give the best result
        MainCam.transform.position = new Vector3(tileWidth / 2 * tileOffset, (tileLength + tileWidth) + 5f, tileLength * tileOffset / 2);

        //Finds the position of the tile in the middle of the bottom row
        Transform middlepos = Tiles[tileWidth / 2, 0].transform;
        //Moves the Player on top of that tile
        player.transform.position = new Vector3(middlepos.position.x, player.transform.localScale.y, middlepos.transform.position.z);
        //Sets the Player's currentTile to that tile & sets occupied to true
        player.GetComponentInParent<Movement>().currentTile = Tiles[tileWidth / 2, 0];
        player.GetComponentInParent<Movement>().currentTile.occupied = true;
        player.transform.rotation = Quaternion.Euler(0, 180, 0);

        ////Sets the Enemy's currentTile to the Center of the Tile grid, similar to the Player above
        Transform centerPos = Tiles[tileWidth / 2, tileLength / 2].transform;
        //enemy.transform.position = new Vector3(centerPos.position.x, enemy.transform.localScale.y / 2, centerPos.transform.position.z);
        //enemy.GetComponentInParent<Movement>().currentTile = Tiles[tileWidth / 2, tileLength / 2];
        //enemy.GetComponentInParent<Movement>().currentTile.occupied = true;
        Debug.Log("SEC.tileScenesVisited % 3 == 0;  " + (SEC.tileScenesVisited % 3 == 0));
        if(SEC.tileScenesVisited % 3 == 0 && SEC.tileScenesVisited > 0)
        {
            Tile middleTop = Tiles[tileWidth / 2, tileLength - 1];
            middleTop.occupied = true;
            middleTop.SceneToLoad = "Boss" + SEC.tileScenesVisited / 3;
            Debug.Log(middleTop.SceneToLoad);
            middleTop.GetComponent<TileSelectable>().defaultColor = Color.red;
        }
        else 
        {
            Tile middleLeft = Tiles[0, tileLength / 2];
            Tile middleTop = Tiles[tileWidth / 2, tileLength - 1];
            Tile middleRight = Tiles[tileWidth - 1, tileLength / 2];
            middleLeft.occupied = true;
            middleRight.occupied = true;
            middleTop.occupied = true;
            middleLeft.SceneToLoad = "Combat";
            middleLeft.GetComponent<TileSelectable>().defaultColor = Color.red;
            Instantiate(CombatTileFloater, middleLeft.transform);
            middleTop.SceneToLoad = "Shop";
            middleTop.GetComponent<TileSelectable>().defaultColor = Color.green;
            Instantiate(ShopTileFloater, middleTop.transform);
            middleRight.SceneToLoad = "Treasure";
            middleRight.GetComponent<TileSelectable>().defaultColor = Color.yellow;
            Instantiate(TreasureTileFloater, middleRight.transform);
        }

        Light TLCorner = Instantiate(PointLight);
        TLCorner.transform.position = Tiles[0, tileLength - 1].transform.position + new Vector3(0, 1, 0);
        TLCorner.intensity = 0.3f;
        Light TRCorner = Instantiate(PointLight);
        TRCorner.transform.position = Tiles[tileWidth - 1, tileLength - 1].transform.position + new Vector3(0, 1, 0);
        TRCorner.intensity = 0.3f;
        Light BLCorner = Instantiate(PointLight);
        BLCorner.transform.position = Tiles[0, 0].transform.position + new Vector3(0, 1, 0);
        BLCorner.intensity = 0.3f;
        Light BRCorner = Instantiate(PointLight);
        BRCorner.transform.position = Tiles[tileWidth - 1, 0].transform.position + new Vector3(0, 1, 0);
        BRCorner.intensity = 0.3f;


        //Adjust number of obstacles based on amount of tiles
        numberOfObstacles = ((tileWidth + tileLength) / 3) + 1;
        PlaceObstacles(numberOfObstacles);
        PlaceEnemies(enemiesToAdd);
        player.GetComponent<PlayerClickToMove>().UnpaintTiles();
    }

    void Start()
    {
        
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
                Tiles[x, y].walkable = false;
                Tiles[x, y].GetComponent<TileSelectable>().enabled = false;
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

    //Places Enemies randomly inside the grid of tiles
    public void PlaceEnemies(List<Enemy> enemiesToAdd)
    {
        for (int i = 0; i < enemiesToAdd.Count; i++)
        {
            //Randomly generate x & y coords within the grid
            int x = Random.Range(0, tileWidth);
            int y = Random.Range(0, tileLength);
            //If the Tile isn't already occupied by the Player, another obstacle, etc...
            if (!Tiles[x, y].occupied)
            {
                //Occupies that tile, instantiates the Enemy prefab, and places it on top of the tile.
                Tiles[x, y].occupied = true;
                Enemy e = Instantiate(enemiesToAdd[i], transform);
                enemies.Add(e);
                e.GetComponent<Movement>().currentTile = Tiles[x, y];
                e.GetComponent<Collider>().enabled = false;
                //Here, the y-value is just y instead of y/2.  For some reason, Cylinder's center (0, 0, 0) is calculated differently.
                e.transform.position = Tiles[x, y].transform.position;// + new Vector3(0f, e.transform.localScale.y, 0f);
            }
            //If that Tile was already occupied; reset i to the previous value to try again.
            else
                i--;
        }
    }
}
