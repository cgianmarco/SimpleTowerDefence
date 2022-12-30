using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;





public class UnitManager : MonoBehaviour
{
    protected static List<Unit> pool = new List<Unit>();

    public static UnitManager Instance;

    [SerializeField] private Turret turretPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public Unit SpawnUnitOnTile(Tile tile)
    {
        Unit unit = SpawnUnit(turretPrefab, tile.transform);
        tile.SetBuilding(unit);
        return unit;
    }


    public Unit SpawnUnit(Unit prefab)
    {
        return SpawnUnit(prefab, transform);
    }

    public Unit SpawnUnit(Unit prefab, Transform transform)
    {
        Unit unit = Instantiate(prefab, transform);
        pool.Add(unit);
        unit.onDestroyed += unit => pool.Remove(unit);

        return unit;
    }

    public Unit FindClosest<T>(Turret turret) where T : Unit
    {
        return pool.OfType<T>().Where(e => Vector3.Distance(turret.transform.position, e.transform.position) <= turret.range)
                    .OrderBy(e => Vector3.Distance(turret.transform.position, e.transform.position))
                    .FirstOrDefault();
    }


}
