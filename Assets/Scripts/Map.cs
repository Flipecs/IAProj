using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class Map : MonoBehaviour
{
    public string basename;
    public TileData[] tileLib;
    public Tile tilePrefab;

    public Vector2Int size;
    Tile[,] tileMap;
    Dictionary<string,TileData> tiles;
    Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<Grid>();
        tileMap = new Tile[size.x,size.y];
        tiles = new Dictionary<string, TileData>();
        LoadTiles();
    }

    void LoadTiles() {
        foreach (TileData data in tileLib) {
            tiles.Add(data.name, data);
        }

        System.Random rng = new System.Random();
        string[] keys = new string[tiles.Keys.Count];
        tiles.Keys.CopyTo(keys, 0);

        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                tileMap[i,j] = Instantiate(tilePrefab, grid.GetCellCenterLocal(new Vector3Int(i,j,0)), Quaternion.identity, this.transform);
                tileMap[i,j].position = new Vector2Int(i,j);
                tileMap[i,j].data = tiles[keys[rng.Next(keys.Length)]];
                tileMap[i,j].gameObject.SetActive(true);
            }
        }
    }

    void Update()
    {
        
    }
}
