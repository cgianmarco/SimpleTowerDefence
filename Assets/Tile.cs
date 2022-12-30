using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class TileState
{
    
    public abstract bool IsTraversable();
    public abstract bool IsPlaceable();

    public virtual void OnMouseDown(Tile tile) { }
    public virtual void OnMouseEnter(Tile tile) { }
    public virtual void OnMouseExit(Tile tile) { }

    public virtual void OnReset(Tile tile) { }

}


public class EmptyTileState : TileState
{
    
    public EmptyTileState(Tile tile) 
    {
        tile.GetComponent<ColorManager>().SetColor(TileColors.IDLE);
    }

    public override bool IsPlaceable() { return true; }

    public override bool IsTraversable() { return true; }


    public override void OnMouseEnter(Tile tile)
    {
        tile.SetState(new SelectedTileState(tile));
    }

    


}

public class OccupiedTileState : TileState
{
    public OccupiedTileState(Tile tile)
    {
        tile.GetComponent<ColorManager>().SetColor(TileColors.IDLE);
    }

    public override bool IsPlaceable() { return false; }


    public override bool IsTraversable() { return false; }

    public override void OnMouseEnter(Tile tile)
    {
        tile.SetState(new OccupiedSelectedTileState(tile));
    }

    

}

public class SelectedTileState : TileState
{
    public SelectedTileState(Tile tile)
    {
        tile.GetComponent<ColorManager>().SetColor(TileColors.GOOD);
    }

    public override bool IsPlaceable() { return true; }


    public override bool IsTraversable() { return false; }

    public override void OnMouseExit(Tile tile)
    {
        tile.SetState(new EmptyTileState(tile));
    }

    public override void OnMouseDown(Tile tile)
    {
        UnitManager.Instance.SpawnUnitOnTile(tile);
        tile.SetState(new OccupiedTileState(tile));
    }

}

public class OccupiedSelectedTileState : TileState
{
    public OccupiedSelectedTileState(Tile tile)
    {
        tile.GetComponent<ColorManager>().SetColor(TileColors.BAD);
    }

    public override bool IsPlaceable() { return false; }


    public override bool IsTraversable() { return false; }

    public override void OnMouseExit(Tile tile)
    {
        tile.SetState(new OccupiedTileState(tile));
    }

}

public class PathTileState : TileState
{
    public PathTileState(Tile tile)
    {
        tile.GetComponent<ColorManager>().SetColor(TileColors.PATH);
    }

    public override bool IsPlaceable() { return true; }

    public override bool IsTraversable() { return true; }

    public override void OnMouseEnter(Tile tile)
    {
        tile.SetState(new SelectedTileState(tile));
    }

    public override void OnReset(Tile tile)
    {
        tile.SetState(new EmptyTileState(tile));
    }
}



public static class TileColors
{
    public static Color IDLE = new Color(0.1682093f, 0.3342578f, 0.3396226f, 1.0f);
    public static Color PATH = new Color(0.2598789f, 0.3582676f, 0.7547169f, 1.0f);
    public static Color GOOD = new Color(0.2598789f, 0.7547169f, 0.3582676f, 1.0f);
    public static Color BAD = new Color(0.7547169f, 0.2598789f, 0.3582676f, 1.0f);
}

public class Tile : MonoBehaviour
{
    
    [SerializeField] private Transform tileCube;

    TileState state;

    public Unit turret;
    

    private Vector2Int coordinates;


    

    public event Action OnStart;
    public event Action OnMouseDownEvent;

    private void Awake()
    {
        int i = (int)transform.position.x / 10;
        int j = (int)transform.position.z / 10;

        coordinates = new Vector2Int(i, j);

        state = new EmptyTileState(this);
    }


    public bool IsPlaceable()
    {
        return state.IsPlaceable();
    }

    public bool IsTraversable()
    {
        return state.IsTraversable();
    }

    public void SetState(TileState state)
    {
        this.state = state;
    }

    public TileState GetState()
    {
        return state;
    }




    public Vector2Int GetCoordinates()
    {
        return coordinates;
    }

    public void SetBuilding(Unit turret)
    {
        this.turret = turret;
        this.state = new OccupiedTileState(this);
    }


    public void Empty()
    {
        this.turret = null;
    }



    private void OnMouseEnter()
    {
        
        GameManager.Instance.state.HandleTileOnMouseEnter(this);
    }

    private void OnMouseExit()
    {
        GameManager.Instance.state.HandleTileOnMouseExit(this);
    }



    private void OnMouseDown()
    {

        GameManager.Instance.state.HandleTileOnMouseDown(this);       

    }

    

    

    public void Reset()
    {
        state.OnReset(this);
    }

    public void MarkAsPath()
    {
        state = new PathTileState(this);
    }

    




}
