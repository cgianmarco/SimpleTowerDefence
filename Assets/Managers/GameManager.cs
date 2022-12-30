using UnityEngine;


public abstract class State
{
    public virtual void HandleTileOnMouseDown(Tile tile) { }
    public virtual void HandleBuyTurretClick() { }

    public virtual void HandleNextWaveClick() { }

    public virtual void HandleIncomingWaveFinished() { }

    public virtual void HandleTileOnMouseEnter(Tile tile) { }
    public virtual void HandleTileOnMouseExit(Tile tile) { }



}

public class BuyingState : State
{
    

    public override void HandleBuyTurretClick()
    {
        if (ShopManager.Instance.CanBuyTurret(20))
            GameManager.Instance.SetState(new PlacingState());
    }

    public override void HandleNextWaveClick()
    {
        WaveSpawner.Instance.SpawnWave();
        GameManager.Instance.SetState(new IncomingWaveState());
    }

    
}

public class PlacingState : State
{

    Turret turret;

    public override void HandleNextWaveClick()
    {
        WaveSpawner.Instance.SpawnWave();
        GameManager.Instance.SetState(new IncomingWaveState());
    }

    public override void HandleTileOnMouseDown(Tile tile)
    {
        if (tile.GetState().IsPlaceable())
        {
            tile.GetState().OnMouseDown(tile);
            Environment.instance.GetComponent<PathManager>()?.GeneratePath();
            ShopManager.Instance.BuyUnit(20);
            GameManager.Instance.SetState(new BuyingState());
        }

    }

    public override void HandleTileOnMouseEnter(Tile tile)
    {
        // IF THERE IS A PATH
        //      TILE.SETSTATE(SELECTEDSTATE)
        // ELSE
        //      TILE.SETSTATE(BUILTSELECTEDSTATE)
        tile.GetState().OnMouseEnter(tile);
        Environment.instance.GetComponent<PathManager>()?.GeneratePath();
    }

    public override void HandleTileOnMouseExit(Tile tile)
    {
        tile.GetState().OnMouseExit(tile);

        Environment.instance.GetComponent<PathManager>()?.GeneratePath();
    }


}

public class IncomingWaveState : State
{

    public override void HandleIncomingWaveFinished()
    {
        GameManager.Instance.SetState(new BuyingState());
    }

    
}


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public State state;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        state = new BuyingState();
    }

    public void SetState(State newState)
    {
        state = newState;
        Debug.Log(state);
    }

    public void HandleTileOnMouseDown(Tile tile)
    {
        state.HandleTileOnMouseDown(tile);
    }
    public void HandleBuyTurretClick()
    {
        state.HandleBuyTurretClick();
    }
    public void HandleNextWaveClick()
    {
        state.HandleNextWaveClick();
    }

    public void HandleIncomingWaveFinished()
    {
        state.HandleIncomingWaveFinished();
    }

 



}
