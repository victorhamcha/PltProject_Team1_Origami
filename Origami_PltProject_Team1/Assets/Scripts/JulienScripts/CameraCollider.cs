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

    private float _finalAngleTempX = 0.0f;
    private float _speedRotationForwardTemp = 0.0f;
    private float _speedRotationBackwardTemp = 0.0f;
    private float _finalAngleTempY = 0.0f;
    private float _speedRotationUpTemp = 0.0f;
    private float _speedRotationDownTemp = 0.0f;
    private bool _startRotatingUpTemp = false;
    private bool _StartRotatingLeftTemp = false;
    private float _nearClipPlaneTemp = 0f;

    [SerializeField] private float _nearClipPlane = 0.0f;
    [SerializeField] private float _originalEndSize = 0.0f;
    [SerializeField] private float _originalStartSize = 0.0f;
    [SerializeField] private float _speedZoom = 0.0f;
    [SerializeField] private float _speedDezoom = 0.0f;

    [SerializeField] private float _finalAngleX = 0.0f;
    [SerializeField] private float _speedRotationForward = 0.0f;
    [SerializeField] private float _speedRotationBackward = 0.0f;
    [SerializeField] private float _finalAngleY = 0.0f;
    [SerializeField] private float _speedRotationUp = 0.0f;
    [SerializeField] private float _speedRotationDown = 0.0f;
    [SerializeField] private Transform _endPosRotation = null;

    public bool zooming = false;
    public bool onlyZoom = false;
    public bool onlyRotation = false;

    public bool rotatingX = false;
    public bool onlyRotationX = false;

    public bool rotatingY = false;
    public bool onlyRotationY = false;

    public bool startRotatingUp = false;
    public bool StartRotatingLeft = false;

    [HideInInspector] public bool hasExited = false;


    void Start()
    {
        _cameraManager = GameManager.Instance.GetCameraManager();

/*        _originalEndSizeTemp = _cameraManager._originalEndSize;
        _originalStartSizeTemp = _cameraManager._originalStartSize;
        _speedZoomTemp = _cameraManager.speedZoom;
        _speedDezoomTemp = _cameraManager.speedDezoom;

        _finalAngleTempX = _cameraManager._finalAngleX;
        _speedRotationForwardTemp = _cameraManager._speedRotationForward;
        _speedRotationBackwardTemp = _cameraManager._speedRotationBackward;

        _finalAngleTempY = _cameraManager._finalAngleY;
        _speedRotationForwardTemp = _cameraManager._speedRotationUp;
        _speedRotationBackwardTemp = _cameraManager._speedRotationDown;
        _startRotatingUpTemp = _cameraManager._rotationUp;
        _StartRotatingLeftTemp = _cameraManager._rotationLeft;
        _nearClipPlaneTemp = _cameraManager.sizeNearPLane;*/
    }

    /*private void Update()
    {
        if (hasExited)
        {
            if (_cameraManager._rotationEndedX && _cameraManager._rotationEndedY && _cameraManager._cam.orthographicSize == _originalEndSize)
            {
                _cameraManager._originalEndSize = _originalEndSizeTemp;
                _cameraManager._originalStartSize = _originalStartSizeTemp;
                _cameraManager.speedZoom = _speedZoomTemp;
                _cameraManager.speedDezoom = _speedDezoomTemp;

                _cameraManager._finalAngleX = _finalAngleTempX;
                _cameraManager._speedRotationForward = _speedRotationForwardTemp;
                _cameraManager._speedRotationBackward = _speedRotationBackwardTemp;

                _cameraManager._finalAngleY = _finalAngleTempY;
                _cameraManager._speedRotationUp = _speedRotationUpTemp;
                _cameraManager._speedRotationDown = _speedRotationDownTemp;

                _cameraManager._rotationUp = _startRotatingUpTemp;
                _cameraManager._rotationLeft = _StartRotatingLeftTemp;
            }
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (zooming && !onlyRotation)
            {
                _cameraManager._originalEndSize = _originalEndSize;
                _cameraManager._originalStartSize = _originalStartSize;
                _cameraManager.speedZoom = _speedZoom;
                _cameraManager.speedDezoom = _speedDezoom;
                _cameraManager._zooming = true;
                _cameraManager.sizeNearPLane = _nearClipPlane;
            }
            else if (!zooming && !onlyRotation)
            {
                _cameraManager._originalEndSize = _originalEndSize;
                _cameraManager._originalStartSize = _originalStartSize;
                _cameraManager.speedZoom = _speedZoom;
                _cameraManager.speedDezoom = _speedDezoom;
                _cameraManager._zooming = false;
            }

            if (rotatingX && !onlyZoom && !onlyRotationY)
            {
                _cameraManager._finalAngleX = _finalAngleX;
                _cameraManager._speedRotationForward = _speedRotationForward;
                _cameraManager._speedRotationBackward = _speedRotationBackward;
                _cameraManager._pivotPosRotation = _endPosRotation;
                _cameraManager._rotationLeft = StartRotatingLeft;
                _cameraManager._rotatingForward = true;
                _cameraManager._rotatingBackward = false;
            }
            else if (!rotatingX && !onlyZoom && !onlyRotationY)
            {
                _cameraManager._finalAngleX = _finalAngleX;
                _cameraManager._speedRotationForward = _speedRotationForward;
                _cameraManager._speedRotationBackward = _speedRotationBackward;
                _cameraManager._pivotPosRotation = _endPosRotation;
                _cameraManager._rotationLeft = StartRotatingLeft;
                _cameraManager._rotatingBackward = true;
                _cameraManager._rotatingForward = false;
            }

            if (rotatingY && !onlyZoom && !onlyRotationX)
            {
                _cameraManager._finalAngleY = _finalAngleY;
                _cameraManager._speedRotationUp = _speedRotationUp;
                _cameraManager._speedRotationDown = _speedRotationDown;
                _cameraManager._pivotPosRotation = _endPosRotation;
                _cameraManager._rotatingUp = startRotatingUp;
                _cameraManager._rotatingUp = true;
                _cameraManager._rotatingDown = false;
            }
            else if (!rotatingY && !onlyZoom && !onlyRotationX)
            {
                _cameraManager._finalAngleX = _finalAngleX;
                _cameraManager._speedRotationForward = _speedRotationForward;
                _cameraManager._speedRotationBackward = _speedRotationBackward;
                _cameraManager._pivotPosRotation = _endPosRotation;
                _cameraManager._rotatingUp = startRotatingUp;
                _cameraManager._rotatingDown = true;
                _cameraManager._rotatingUp = false;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        _cameraManager.sizeNearPLane = _nearClipPlaneTemp;
        if (zooming && !onlyRotation)
        {
            _cameraManager._zooming = false;
        }
        else if (!zooming && !onlyRotation)
        {
            _cameraManager._zooming = true;
        }

        if (rotatingX && !onlyZoom && !onlyRotationY)
        {
            _cameraManager._rotatingBackward = true;
            _cameraManager._rotatingForward = false;
        }
        else if (!rotatingX && !onlyZoom && !onlyRotationY)
        {
            _cameraManager._rotatingForward = true;
            _cameraManager._rotatingBackward = false;
        }

        if (rotatingY && !onlyZoom && !onlyRotationX)
        {
            _cameraManager._rotatingDown = true;
            _cameraManager._rotatingUp = false;
        }
        else if (!rotatingY && !onlyZoom && !onlyRotationX)
        {
            _cameraManager._rotatingUp = true;
            _cameraManager._rotatingDown = false;
        }
    }

    public void ExitCollider()
    {
        if (zooming && !onlyRotation)
        {
            _cameraManager._zooming = false;
        }
        else if (!zooming && !onlyRotation)
        {
            _cameraManager._zooming = true;
        }

        if (rotatingX && !onlyZoom && !onlyRotationY)
        {
            _cameraManager._rotatingBackward = true;
            _cameraManager._rotatingForward = false;
        }
        else if (!rotatingX && !onlyZoom && !onlyRotationY)
        {
            _cameraManager._rotatingForward = true;
            _cameraManager._rotatingBackward = false;
        }

        if (rotatingY && !onlyZoom && !onlyRotationX)
        {
            _cameraManager._rotatingDown = true;
            _cameraManager._rotatingUp = false;
        }
        else if (!rotatingY && !onlyZoom && !onlyRotationX)
        {
            _cameraManager._rotatingUp = true;
            _cameraManager._rotatingDown = false;
        }
    }
}
