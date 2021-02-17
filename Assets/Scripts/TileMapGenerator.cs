using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    [SerializeField] GameObject TilePrefab;

    [SerializeField] int tileWidth = 10;
    [SerializeField] int tileLength = 10;
    [SerializeField] float tileOffset = 1.1f;

    [SerializeField]
    GameObject[,] Tiles;

    // Start is called before the first frame update
    void Start()
    {
        CreateTileGrid();
        Tiles = new GameObject[tileWidth, tileLength];
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
                GameObject tempTile = Instantiate(TilePrefab);
                tempTile.transform.position = new Vector3(x * tileOffset, 0, y * tileOffset);
                tempTile.transform.parent = this.transform;
                tempTile.name = x.ToString() + ", " + y.ToString();
                
            }
        }
    }
}
