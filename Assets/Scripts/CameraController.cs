using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private DragDetector _dragDetector = null;

    [Header("Limits")]
    [SerializeField] private Vector2 _limitAngleX = new Vector2();
    [SerializeField] private Vector2 _limitAngleY = new Vector2();

    [Header("Speeds")]
    [SerializeField] private float _angularSpeedX = 30.0f;
    [SerializeField] private float _angularSpeedY = 60.0f;

    private bool _pressed = false;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void OnEnable()
    {
        _dragDetector.OnBeginDragEvent += OnBeginDrag;
        _dragDetector.OnDragEvent += OnDragEvent;
        _dragDetector.OnEndDragEvent += OnEndDragEvent;
    }

    private void OnDisable()
    {
        _dragDetector.OnBeginDragEvent -= OnBeginDrag;
        _dragDetector.OnDragEvent -= OnDragEvent;
        _dragDetector.OnEndDragEvent -= OnEndDragEvent;
    }

    private void Awake()
    {
        Input.multiTouchEnabled = false;
    }

    private void OnBeginDrag(PointerEventData eventData)
    {
        
    } 

    private void OnDragEvent(PointerEventData eventData)
    {
        yaw += _angularSpeedX * Input.GetAxis("Mouse X") * Time.deltaTime;
        pitch -= _angularSpeedY * Input.GetAxis("Mouse Y") * Time.deltaTime;

        yaw = Mathf.Clamp(yaw, _limitAngleY.x, _limitAngleY.y);
        pitch = Mathf.Clamp(pitch, _limitAngleX.x, _limitAngleX.y);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    private void OnEndDragEvent(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
    }


}
