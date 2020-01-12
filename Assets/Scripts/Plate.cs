using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody = null;
    [SerializeField] private float _lifeTimeMax = 5.0f;

    private float _lifeTime = 0.0f;

    private void Awake()
    {
        StartCoroutine(AutoDestroy());
    }

    private void OnDestroy()
    {
        DestroyEvent?.Invoke();
    }

    private IEnumerator AutoDestroy()
    {
        while (_lifeTime < _lifeTimeMax)
        {
            _lifeTime += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

    public float LifeTimeCurrent => _lifeTime;

    public static Action DestroyEvent = null;
}
