using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    [SerializeField] GameObject TilePrefab;
    [SerializeField] GameObject WallPrefab;
    [SerializeField] Light PointLight;
    [SerializeField] Camera MainCam;
    [SerializeField] Player player;

    [SerializeField] public int tileWidth = 10;
    [SerializeField] public int tileLength = 10;
    [SerializeField] public float tileOffset = 1.1f;

    public Tile[,] Tiles;

    // Start is called before the first frame update
    void Awake()
    {
        Tiles = new Tile[tileWidth, tileLength];
        CreateTileGrid();
        PointLight.transform.position = new Vector3(tileWidth / 2, 5, tileLength / 2);
        PointLight.range = (tileWidth + tileLength) * 360 / 20;
        MainCam.transform.position = new Vector3(tileWidth / 2, (tileLength + tileWidth) / 2 + 2f, tileLength / 2);
        Transform middlepos = Tiles[tileWidth-1, tileLength/2].transform;
        player.transform.position = new Vector3(middlepos.position.x, player.transform.localScale.y, middlepos.transform.position.z);
        player.GetComponentInParent<Movement>().originalTile = Tiles[tileWidth - 1, tileLength / 2];
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
                Tile t = tempTile.GetComponent<Tile>() as Tile;
                Tiles[x, y] = t;
                //tempTile.transform.parent = this.transform;
                tempTile.name = x.ToString() + ", " + y.ToString();
                t.x = x;
                t.y = y;


                if (x == 0)
                {
                    GameObject tempWall = Instantiate(WallPrefab, this.transform) as GameObject;
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
}
