using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    [SerializeField] private List<PlateSpawner> _spawnerList = new List<PlateSpawner>();
    [SerializeField] private int _spawnTimes = 1;

    public static Action<Level> LoadedEvent = null;

    public void Launch()
    {
        int idx = Random.Range(0, _spawnerList.Count);
        _spawnerList[idx].StartSpawning(_spawnTimes, () => { });
    }

}
