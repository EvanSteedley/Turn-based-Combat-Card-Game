using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    [SerializeField] GameObject TilePrefab;
    [SerializeField] GameObject WallPrefab;

    [SerializeField] int tileWidth = 10;
    [SerializeField] int tileLength = 10;
    [SerializeField] float tileOffset = 1.1f;

    Tile[,] Tiles;

    // Start is called before the first frame update
    void Start()
    {
        Tiles = new Tile[tileWidth, tileLength];
        CreateTileGrid();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTileGrid()
    {
        
        for (int x = 0; x < tileWidth; x++)
        {
            for (int y = 0; y < tileLength; y++)
            {
                GameObject tempTile = Instantiate(TilePrefab, this.transform) as GameObject;
                tempTile.transform.position = new Vector3(x * tileOffset, 0, y * tileOffset);
                if(x == 0)
                {
                    GameObject tempWall = Instantiate(WallPrefab, this.transform) as GameObject;
                    tempWall.transform.position = new Vector3(-1 * tileOffset, WallPrefab.transform.localScale.y / 2, y * tileOffset);
                }
                if (y == 0)
                {
                    GameObject tempWall = Instantiate(WallPrefab, this.transform) as GameObject;
                    tempWall.transform.position = new Vector3(x * tileOffset, WallPrefab.transform.localScale.y / 2, -1 * tileOffset);
                }
                if (x == 9)
                {
                    GameObject tempWall = Instantiate(WallPrefab, this.transform) as GameObject;
                    tempWall.transform.position = new Vector3((x + 1) * tileOffset, WallPrefab.transform.localScale.y / 2, y * tileOffset);
                }
                if (y == 9)
                {
                    GameObject tempWall = Instantiate(WallPrefab, this.transform) as GameObject;
                    tempWall.transform.position = new Vector3(x * tileOffset, WallPrefab.transform.localScale.y / 2, (y + 1) * tileOffset);
                }
                Tile t = tempTile.GetComponent<Tile>() as Tile;
                Tiles[x, y] = t;
                //tempTile.transform.parent = this.transform;
                tempTile.name = x.ToString() + ", " + y.ToString(); 
            }
        }
    }
}
