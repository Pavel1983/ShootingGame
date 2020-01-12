using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[Serializable]
public struct AimItem
{
    public float Duration;
    public float AimTime;
}

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] private Button _launchButton = null;
    [SerializeField] private Level _level = null;
    [SerializeField] private Transform _pricel = null;
    [SerializeField] private Image _progress = null;

    [SerializeField] private List<AimItem> _aimItems = new List<AimItem>();

    private bool _objectHit = false;
    private GameObject _hitObject = null;
    private int _layerMask;
    private float _hitTime = 0.0f;
    private int _aimIndex = 0;

    private void OnEnable()
    {
        _launchButton.onClick.AddListener(OnLaunch);
        Plate.DestroyEvent += OnPlateDestroy;
    }

    private void OnDisable()
    {
        _launchButton.onClick.RemoveListener(OnLaunch);
        Plate.DestroyEvent -= OnPlateDestroy;
    }

    private void Awake()
    {
        _layerMask = LayerMask.GetMask("PlateLayer");
    }

    private void OnPlateDestroy()
    {
        _launchButton.interactable = true;
    }

    private void OnLaunch()
    {
        _level.Launch();
        _launchButton.interactable = false;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, _layerMask))
        {
            GameObject newObject = hit.collider.gameObject;
            if (_hitObject != newObject)
            {
                _hitObject = newObject;
                _objectHit = true;
                _hitTime = 0.0f;

                Plate plate = _hitObject.GetComponent<Plate>();
                Assert.IsTrue(plate != null);

                _aimIndex = GetAimIndexByLifeTime(plate.LifeTimeCurrent);
            }
            else
            {
                _hitTime += Time.deltaTime;
            }
        }
        else
        {
            _objectHit = false;
            _hitTime = 0.0f;
            _hitObject = null;
            _progress.fillAmount = 0.0f;
        }

        if (_objectHit)
        {
            if (_aimIndex != -1)
            {
                float aimTime = _aimItems[_aimIndex].AimTime;

                _progress.fillAmount = _hitTime / aimTime;

                if (Mathf.Abs(_progress.fillAmount - 1.0f) < 1E-3f)
                {
                    Shoot(_hitObject);
                }
            }
        }
    }

    private void Shoot(GameObject plateToKill)
    {
        Destroy(plateToKill);
    }

    private int GetAimIndexByLifeTime(float lifeTime)
    {
        int i = 0;
        float timer = 0.0f;
        while (timer < lifeTime)
        {
            timer += _aimItems[i].Duration;
            if (i + 1 > _aimItems.Count)
            {
                return -1;
            }
        }

        return i;
    }

}
