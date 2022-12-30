using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;





public class WaveSpawner : MonoBehaviour
{

    

    [SerializeField] List<Enemy> enemyPrefabs;
    private List<Unit> wave = new List<Unit>();

    int waveSize = 1;

    
    
    

    private readonly float spawnTimer = 1f;

    public static WaveSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }




    public void SpawnWave()
    {
        StartCoroutine(SpawnUnit());
    }

    IEnumerator SpawnUnit()
    {

        for (int i = 0; i < waveSize; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Count());

            Unit unit = UnitManager.Instance.SpawnUnit(enemyPrefabs[randomIndex]);
            wave.Add(unit);
            unit.onDestroyed += unit => RemoveFromWave(unit);

            

            yield return new WaitForSeconds(spawnTimer);
        }

        
        waveSize += 1;
    }

    private void RemoveFromWave(Unit unit)
    {
        wave.Remove(unit);
        if (wave.Count == 0)
            GameManager.Instance.HandleIncomingWaveFinished();
    }

   

    
    

    

    
}
