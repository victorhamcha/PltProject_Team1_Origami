using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    private CameraManager _cameraManager = null;

    private float _originalEndSizeTemp = 0.0f;
    private float _originalStartSizeTemp = 0.0f;
    private float _speedZoomTemp = 0.0f;
    private float _speedDezoomTemp = 0.0f;

    private float _finalAngleTemp = 0.0f;
    private float _speedRotationForwardTemp = 0.0f;
    private float _speedRotationBackwardTemp = 0.0f;

   // private Transform _endPosRotationTemp = null;

    [SerializeField] private float _originalEndSize = 0.0f;
    [SerializeField] private float _originalStartSize = 0.0f;
    [SerializeField] private float _speedZoom = 0.0f;
    [SerializeField] private float _speedDezoom = 0.0f;

    [SerializeField] private float _finalAngle = 0.0f;
    [SerializeField] private float _speedRotationForward = 0.0f;
    [SerializeField] private float _speedRotationBackward = 0.0f;
    [SerializeField] private Transform _endPosRotation = null;

    public bool rotating = false;
    public bool zooming = false;
    public bool onlyRotation = false;
    public bool onlyZoom = false;

    [HideInInspector] public bool hasExited = false;


    void Start()
    {
        _cameraManager = GameManager.Instance.GetCameraManager();

        _originalEndSizeTemp = _cameraManager._originalEndSize;
        _originalStartSizeTemp = _cameraManager._originalStartSize;
        _speedZoomTemp = _cameraManager.speedZoom;
        _speedDezoomTemp = _cameraManager.speedDezoom;

        _finalAngleTemp = _cameraManager._finalAngle;
        _speedRotationForwardTemp = _cameraManager._speedRotationForward;
        _speedRotationBackwardTemp = _cameraManager._speedRotationBackward;
        // _endPosRotationTemp = _cameraManager._endPosRotation;
    }

    private void Update()
    {
        if(hasExited)
        {
            if(_cameraManager._rotationEnded && _cameraManager._cam.orthographicSize == _originalEndSize)
            {
                _cameraManager._originalEndSize = _originalEndSizeTemp;
                _cameraManager._originalStartSize = _originalStartSizeTemp;
                _cameraManager.speedZoom = _speedZoomTemp;
                _cameraManager.speedDezoom = _speedDezoomTemp;
                _cameraManager._finalAngle = _finalAngleTemp;
                _cameraManager._speedRotationForward = _speedRotationForwardTemp;
                _cameraManager._speedRotationBackward = _speedRotationBackwardTemp;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           
            hasExited = false;

            if (zooming && !onlyRotation)
            {
                _cameraManager._originalEndSize = _originalEndSize;
                _cameraManager._originalStartSize = _originalStartSize;
                _cameraManager.speedZoom = _speedZoom;
                _cameraManager.speedDezoom = _speedDezoom;
                _cameraManager._zooming = true;
            }
            else if (!zooming && !onlyRotation)
            {
                _cameraManager._originalEndSize = _originalEndSize;
                _cameraManager._originalStartSize = _originalStartSize;
                _cameraManager.speedZoom = _speedZoom;
                _cameraManager.speedDezoom = _speedDezoom;
                _cameraManager._zooming = false;
            }

            if (rotating && !onlyZoom)
            {
                _cameraManager._finalAngle = _finalAngle;
                _cameraManager._speedRotationForward = _speedRotationForward;
                _cameraManager._speedRotationBackward = _speedRotationBackward;
                _cameraManager._endPosRotation = _endPosRotation;
                _cameraManager._rotatingForward = true;
                _cameraManager._rotatingBackward = false;
            }
            else if (!rotating && !onlyZoom)
            {
                _cameraManager._finalAngle = _finalAngle;
                _cameraManager._speedRotationForward = _speedRotationForward;
                _cameraManager._speedRotationBackward = _speedRotationBackward;
                _cameraManager._endPosRotation = _endPosRotation;
                _cameraManager._rotatingBackward = true;
                _cameraManager._rotatingForward = false;
            }
        }
        


    }

    private void OnTriggerExit(Collider other)
    {
        hasExited = true;
        if (zooming && !onlyRotation)
        {
            _cameraManager._zooming = false;
        }
        else if (!zooming && !onlyRotation)
        {
            _cameraManager._zooming = true;
        }

        if (rotating && !onlyZoom)
        {
            _cameraManager._rotatingBackward = true;
            _cameraManager._rotatingForward = false;
        }
        else if (!rotating && !onlyZoom)
        {
            _cameraManager._rotatingForward = true;
            _cameraManager._rotatingBackward = false;
        }
    }

    public void ExitCollider()
    {
        hasExited = true;
        if (zooming && !onlyRotation)
        {
            _cameraManager._zooming = false;
        }
        else if (!zooming && !onlyRotation)
        {
            _cameraManager._zooming = true;
        }

        if (rotating && !onlyZoom)
        {
            _cameraManager._rotatingBackward = true;
            _cameraManager._rotatingForward = false;
        }
        else if (!rotating && !onlyZoom)
        {
            _cameraManager._rotatingForward = true;
            _cameraManager._rotatingBackward = false;
        }
    }
}
