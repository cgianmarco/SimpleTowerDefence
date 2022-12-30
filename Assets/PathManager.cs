using System.Collections.Generic;
using UnityEngine;




public abstract class PathManager : MonoBehaviour
{

    public List<Tile> path = new List<Tile>();

    protected Tile spawnPoint;
    protected Tile endPoint;

    protected Environment env;


    private void Awake()
    {
        env = GetComponent<Environment>();
        env.OnGeneratedMap += GeneratePath;

    }

    public abstract void GeneratePath();

    public virtual void ResetTiles()
    {
        foreach(Tile tile in path)
            tile.Reset();
    }



}
