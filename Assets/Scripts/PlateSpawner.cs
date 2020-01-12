using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlateSpawner : MonoBehaviour
{
    [SerializeField] private Rigidbody _platePrefab = null;
    [SerializeField] private float _minForceAmplitude = 10.0f;
    [SerializeField] private float _maxForceAmplitude = 20.0f;
    [SerializeField] private float _spawnPeriod = 3.0f;

    private Rigidbody _plate = null;

    public void StartSpawning(int spawnTimes, Action onFinished)
    {
        StartCoroutine(SpawnProcess(spawnTimes, onFinished));
    }

    private void SpawnPlate()
    {
        _plate = Instantiate(_platePrefab);
        _plate.transform.position = transform.position;
        _plate.transform.rotation = transform.rotation;

        Vector3 dir = transform.right;
        float forceAplitude = Random.Range(_minForceAmplitude, _maxForceAmplitude);
        _plate.AddForce(dir * forceAplitude, ForceMode.Impulse);
    }

    private IEnumerator SpawnProcess(int spawnTimes, Action onFinished)
    {
        int counter = 0;
        while (counter < spawnTimes)
        {
            SpawnPlate();

            yield return new WaitForSeconds(_spawnPeriod);

            counter++;
        }

        onFinished?.Invoke();
    }
}
