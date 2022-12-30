using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;




public class Environment : MonoBehaviour
{

    public static Environment instance;

    private int mapSize = 10;
    private Tile[,] map;


    private Vector2Int start = new Vector2Int(0, 0);
    private Vector2Int end = new Vector2Int(8, 8);

    public Action OnGeneratedMap;


    






    private int loadedTiles = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        
        
    }

    private void Start()
    {
        GenerateMap();
        
    }

    public Tile GetSpawnPoint() { return GetTile(start); }
    public Tile GetEndPoint() { return GetTile(end); }


    void GenerateMap()
    {


        map = new Tile[mapSize, mapSize];

        foreach (Tile tile in GetComponentsInChildren<Tile>())
        {
            tile.OnMouseDownEvent += OnMouseDownEvent;
            Vector2Int coords = tile.GetCoordinates();
            map[coords.x, coords.y] = tile;
        }

        Debug.Log("Generated map");


        OnGeneratedMap?.Invoke();

    }

    private void OnMouseDownEvent()
    {
        GetComponent<PathManager>()?.GeneratePath();
    }




    public Tile GetTile(Vector2Int coords)
    {
        return map[coords.x, coords.y];
    }

    public List<Tile> GetNeighbours(Tile tile)
    {
        Vector2Int coords = tile.GetCoordinates();
        List<Tile> neighbours = new List<Tile>();


        if (coords.y + 1 < mapSize)
            neighbours.Add(GetTile(new Vector2Int(coords.x, coords.y + 1)));
        if (coords.y - 1 >= 0)
            neighbours.Add(GetTile(new Vector2Int(coords.x, coords.y - 1)));
        if (coords.x + 1 < mapSize)
            neighbours.Add(GetTile(new Vector2Int(coords.x + 1, coords.y)));
        if (coords.x - 1 >= 0)
            neighbours.Add(GetTile(new Vector2Int(coords.x - 1, coords.y)));

        return neighbours;
    }

    

    




}
