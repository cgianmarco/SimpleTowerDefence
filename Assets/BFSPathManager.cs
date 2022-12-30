using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BFSTileInfo
{

    private Tile parent;
    public BFSTileInfo() { }


    public BFSTileInfo(Tile parent)
    {
        this.parent = parent;
    }



    public Tile GetParent()
    {
        return this.parent;
    }



    public bool HasParent()
    {
        return parent is not null;
    }

}

public class BFSPathManager : PathManager
{

    private Dictionary<Tile, BFSTileInfo> TileInfos = new Dictionary<Tile, BFSTileInfo>();


    public override void GeneratePath()
    {

        spawnPoint = env.GetSpawnPoint();
        endPoint = env.GetEndPoint();
        Debug.Log("Generate path");

        TileInfos.Clear();
        ResetTiles();

        Queue<Tile> Q = new Queue<Tile>();


        Q.Enqueue(endPoint);
        TileInfos.Add(endPoint, new BFSTileInfo());


        while (Q.Count > 0)
        {
            Tile v = Q.Dequeue();


            if (v.Equals(spawnPoint))
            {
                Debug.Log("found start");
                break;
            }
            foreach (Tile neighbour in env.GetNeighbours(v))
            {

                if (!TileInfos.ContainsKey(neighbour) && neighbour.IsTraversable())
                {
                    Q.Enqueue(neighbour);
                    TileInfos.Add(neighbour, new BFSTileInfo(v));
                }
            }
        }

        path.Clear();

        Tile waypoint = spawnPoint;
        path.Add(waypoint);
        waypoint.MarkAsPath();



        while (!TileInfos[waypoint].GetParent().Equals(endPoint))
        {
            waypoint = TileInfos[waypoint].GetParent();
            path.Add(waypoint);
            waypoint.MarkAsPath();

        }

        path.Add(endPoint);
        endPoint.MarkAsPath();

    }
}
