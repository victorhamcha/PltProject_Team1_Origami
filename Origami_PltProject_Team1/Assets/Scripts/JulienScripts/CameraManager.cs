using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Camera Follow variables
    [Header("Camera Follow")]
    [Space]
    public Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset = Vector3.zero;
    private Vector3 _smoothedPosition = Vector3.zero;
    #endregion

    #region Camera Zoom variables

    [SerializeField] private Camera _blurCam = null;
    [HideInInspector] public Camera _cam = null;
    private float _startSize = 0.0f;
    [HideInInspector] public float _originalStartSize = 0.0f;
    [HideInInspector] public float _originalEndSize = 0.0f;

    [Header("Camera Zoom")]
    private AnimationCurve _zoomCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] [Range(1, 50)] private float _endSize = 5.0f;
    [Range(0.1f, 30.0f)] public float speedZoom = 0.0f;
    [Range(0.1f, 30.0f)] public float speedDezoom = 0.0f;

    public bool _zooming = false;
    private bool _wasZooming = false;

    [SerializeField] private float _slowDuration = 0.0f;
    private float _timerSlow = 0.0f;
    private bool _changeDirection = false;
    private bool _canZoom = true;
    [SerializeField] private AnimationCurve _brakeCurve = null;
    private float _lastSpeed = 0.0f;

    #endregion

    #region Camera Rotation variables

    [Header("Camera rotation")]
    public Transform _pivotPosRotation = null;
    [SerializeField] private AnimationCurve _curveSpeedRotation = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private float _durationCurveSpeedRotation = 1f;
    [SerializeField] private AnimationCurve _brakeCurveSpeedRotation = AnimationCurve.Linear(0, 1, 1, 0);
    [SerializeField] private float _durationBrakeCurveSpeedRotation = 1f;
    public float _finalAngleX = 0f;
    public float _finalAngleY = 0f;
    public float _speedRotationBackward = 2f;
    public float _speedRotationForward = 2f;
    public float _speedRotationUp = 2f;
    public float _speedRotationDown = 2f;
    public bool _rotatingForward = false;
    public bool _rotatingBackward = false;
    public bool _rotatingUp = false;
    public bool _rotatingDown = false;
    private bool _waitNewRotationX = true;
    private bool _waitNewRotationY = true;
    [HideInInspector] public bool _rotationEndedX = true;
    [HideInInspector] public bool _rotationEndedY = true;
    private bool _canRotatingForward = true;
    private bool _canRotatingBackward = false;
    private bool _canRotatingUp = true;
    private bool _canRotatingDown = false;
    private float _currentRotationX = 0f;
    private float _currentRotationY = 0f;
    private float _internTimerCurveBrakeSpeedRotationX = 0f;
    private float _internTimerCurveBrakeSpeedRotationY = 0f;
    private float _internTimerCurveSpeedRotationX = 0f;
    private float _internTimerCurveSpeedRotationY = 0f;

    public bool _rotationLeft = false;
    public bool _rotationUp = true;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {

        #region Zoom variables initialization
        _cam = GetComponent<Camera>();
        _cam.transform.position = target.position + offset;
        _originalStartSize = _cam.orthographicSize;
        _originalEndSize = _endSize;
        #endregion

        //_startPosRotation = _cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region Zoom code

        if (_wasZooming != _zooming && _cam.orthographicSize != _endSize)
        {
            _changeDirection = true;
            _canZoom = false;
        }

        if (_changeDirection)
        {
            _timerSlow += Time.deltaTime;

            if (_zooming)
            {
                _cam.orthographicSize += speedDezoom * _lastSpeed * Time.deltaTime * _brakeCurve.Evaluate(_timerSlow / _slowDuration);
            }
            else
            {
                _cam.orthographicSize -= speedZoom * _lastSpeed * Time.deltaTime * _brakeCurve.Evaluate(_timerSlow / _slowDuration);
            }

            if (_timerSlow >= _slowDuration)
            {
                _changeDirection = false;
                _canZoom = true;
                _timerSlow = 0.0f;
            }
        }

        if (_canZoom)
        {
            if (_zooming)
            {
                CameraZoomIn();
            }
            else
            {
                CameraZoomOut();
            }
        }

        _blurCam.orthographicSize = _cam.orthographicSize;

        _wasZooming = _zooming;

        #endregion

        #region Rotation code
        if (_rotationLeft)
        {
            if (_rotatingForward && _finalAngleX > _currentRotationX && _canRotatingForward)
            {
                RotateForward();
            }
            else if (_rotatingBackward && _currentRotationX > 0 && _canRotatingBackward)
            {
                RotateBackward();
            }

            if (_rotatingBackward && _currentRotationX <= 0)
            {
                _rotationEndedX = true;
                _canRotatingForward = true;
                _canRotatingBackward = false;
                _rotatingForward = false;
                _rotatingBackward = false;
                _currentRotationX = 0;
            }
            else if (_rotatingForward && _currentRotationX >= _finalAngleX)
            {
                _canRotatingForward = false;
                _canRotatingBackward = true;
                _rotatingForward = false;
                _rotatingBackward = false;
                _currentRotationX = _finalAngleX;
            }
            else
            {
                FreinLeft();
            }
        }
        else
        {
            if (_rotatingForward && -_finalAngleX < _currentRotationX && _canRotatingForward)
            {
                RotateBackward();
            }
            else if (_rotatingBackward && _currentRotationX < 0 && _canRotatingBackward)
            {
                RotateForward();
            }

            if (_rotatingBackward && _currentRotationX >= 0)
            {
                _rotationEndedX = true;
                _canRotatingForward = true;
                _canRotatingBackward = false;
                _rotatingForward = false;
                _rotatingBackward = false;
                _currentRotationX = 0;
            }
            else if (_rotatingForward && _currentRotationX <= -_finalAngleX)
            {
                _canRotatingForward = false;
                _canRotatingBackward = true;
                _rotatingForward = false;
                _rotatingBackward = false;
                _currentRotationX = -_finalAngleX;
            }
            else
            {
                FreinRight();
            }
        }


        if (_rotationUp)
        {
            if (_rotatingUp && _finalAngleY > _currentRotationY && _canRotatingUp)
            {
                RotateUp();
            }
            else if (_rotatingDown && _currentRotationY > 0 && _canRotatingDown)
            {
                RotateDown();
            }

            if (_rotatingDown && _currentRotationY <= 0)
            {
                _rotationEndedY = true;
                _canRotatingUp = true;
                _canRotatingDown = false;
                _rotatingUp = false;
                _rotatingDown = false;
                _currentRotationY = 0;
            }
            else if (_rotatingUp && _currentRotationY >= _finalAngleY)
            {
                _canRotatingUp = false;
                _canRotatingDown = true;
                _rotatingUp = false;
                _rotatingDown = false;
                _currentRotationY = _finalAngleY;
            }
            else
            {
                FreinUp();
            }
        }
        else
        {
            if (_rotatingUp && -_finalAngleY < _currentRotationY && _canRotatingUp)
            {
                RotateDown();
            }
            else if (_rotatingDown && _currentRotationY < 0 && _canRotatingDown)
            {
                RotateUp();
            }

            if (_rotatingDown && _currentRotationY >= 0)
            {
                _rotationEndedY = true;
                _canRotatingUp = true;
                _canRotatingDown = false;
                _rotatingUp = false;
                _rotatingDown = false;
                _currentRotationY = 0;
            }
            else if (_rotatingUp && _currentRotationY <= -_finalAngleY)
            {
                _canRotatingUp = false;
                _canRotatingDown = true;
                _rotatingUp = false;
                _rotatingDown = false;
                _currentRotationY = -_finalAngleY;
            }
            else
            {
                FreinDown();
            }
        }

        if (_rotatingBackward || _rotatingForward)
        {
            _waitNewRotationX = false;
        }
        else
        {
            _waitNewRotationX = true;
        }
        
        if (_rotatingDown || _rotatingUp)
        {
            _waitNewRotationY = false;
        }
        else
        {
            _waitNewRotationY = true;
        }

        #endregion
    }

    void LateUpdate()
    {
        #region Follow code
        CameraFollow();
        #endregion
    }


    public void RotateForward()
    {
        if (_rotationLeft)
        {
            _canRotatingForward = true;
            _rotatingBackward = false;
            _rotationEndedX = false;
        }
        else
        {
            _canRotatingBackward = true;
            _rotatingForward = false;
        }
        
        if (_internTimerCurveSpeedRotationX < _durationCurveSpeedRotation)
        {
            _internTimerCurveSpeedRotationX += Time.deltaTime;
        }
        _currentRotationX += _speedRotationForward * Time.deltaTime * _curveSpeedRotation.Evaluate(_internTimerCurveSpeedRotationX / _durationCurveSpeedRotation);
        _cam.transform.RotateAround(_pivotPosRotation.position, Vector3.up, (_speedRotationForward * Time.deltaTime) * _curveSpeedRotation.Evaluate(_internTimerCurveSpeedRotationX / _durationCurveSpeedRotation));
    }

    public void RotateUp()
    {
        if (_rotationUp)
        {
            _canRotatingUp = true;
            _rotatingDown = false;
            _rotationEndedY = false;
        }
        else
        {
            _canRotatingDown = true;
            _rotatingUp = false;
        }
        if (_internTimerCurveSpeedRotationY < _durationCurveSpeedRotation)
        {
            _internTimerCurveSpeedRotationY += Time.deltaTime;
        }

        _currentRotationY += _speedRotationUp * Time.deltaTime * _curveSpeedRotation.Evaluate(_internTimerCurveSpeedRotationY / _durationCurveSpeedRotation);
        _cam.transform.RotateAround(_pivotPosRotation.position, Vector3.back, (_speedRotationUp * Time.deltaTime) * _curveSpeedRotation.Evaluate(_internTimerCurveSpeedRotationY / _durationCurveSpeedRotation));
    }

    public void RotateBackward()
    {
        if (_rotationLeft)
        {
            _canRotatingBackward = true;
            _rotatingForward = false;
        }
        else
        {
            _canRotatingForward = true;
            _rotatingBackward = false;
            _rotationEndedX = false;
        }
        if (_internTimerCurveSpeedRotationX < _durationCurveSpeedRotation)
        {
            _internTimerCurveSpeedRotationX += Time.deltaTime;
        }
        _currentRotationX -= _speedRotationBackward * Time.deltaTime * _curveSpeedRotation.Evaluate(_internTimerCurveSpeedRotationX / _durationCurveSpeedRotation);
        _cam.transform.RotateAround(_pivotPosRotation.position, Vector3.up, -(_speedRotationBackward * Time.deltaTime) * _curveSpeedRotation.Evaluate(_internTimerCurveSpeedRotationX / _durationCurveSpeedRotation));
    }

    public void RotateDown()
    {

        if (_rotationUp)
        {
            _canRotatingDown = true;
            _rotatingUp = false;
        }
        else
        {
            _canRotatingUp = true;
            _rotatingDown = false;
            _rotationEndedY = false;
        }
        
        if (_internTimerCurveSpeedRotationY < _durationCurveSpeedRotation)
        {
            _internTimerCurveSpeedRotationY += Time.deltaTime;
        }
        _currentRotationY -= _speedRotationDown * Time.deltaTime * _curveSpeedRotation.Evaluate(_internTimerCurveSpeedRotationY / _durationCurveSpeedRotation);
        _cam.transform.RotateAround(_pivotPosRotation.position, Vector3.back, -(_speedRotationDown * Time.deltaTime) * _curveSpeedRotation.Evaluate(_internTimerCurveSpeedRotationY / _durationCurveSpeedRotation));
    }

    public void CameraFollow()
    {
        if (_rotationEndedX && _rotationEndedY)
        {
            Vector3 desiredPosition = target.position + offset;
            _smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            _cam.transform.position = _smoothedPosition;
        }

        transform.LookAt(target);
    }

    public void CameraZoomIn()
    {
        _endSize = _originalEndSize;
        _startSize = _originalStartSize;

        if (_cam.orthographicSize > _endSize)
        {
            _lastSpeed = _zoomCurve.Evaluate(Mathf.InverseLerp(_startSize, _endSize, _cam.orthographicSize - speedZoom * Time.deltaTime));
            _cam.orthographicSize = Mathf.Lerp(_startSize, _endSize, _lastSpeed);
        }
    }

    public void CameraZoomOut()
    {
        _endSize = _originalStartSize;
        _startSize = _originalEndSize;
        if (_cam.orthographicSize < _endSize)
        {
            _lastSpeed = _zoomCurve.Evaluate(Mathf.InverseLerp(_startSize, _endSize, _cam.orthographicSize + speedDezoom * Time.deltaTime));
            _cam.orthographicSize = Mathf.Lerp(_startSize, _endSize, _lastSpeed);
        }

    }

    private void FreinLeft()
    {
        if ((_canRotatingForward && !_rotatingForward && _rotatingBackward) || (_canRotatingBackward && _rotatingForward && !_rotatingBackward) || !_rotatingForward && !_rotatingBackward)
        {
            _internTimerCurveBrakeSpeedRotationX += Time.deltaTime;
            _internTimerCurveSpeedRotationX = 0f;
            if (_internTimerCurveBrakeSpeedRotationX >= _durationBrakeCurveSpeedRotation && !_waitNewRotationX)
            {
                _internTimerCurveBrakeSpeedRotationX = 0f;
                if ((_canRotatingForward && !_rotatingForward && _rotatingBackward))
                {
                    _canRotatingForward = false;
                    _canRotatingBackward = true;
                }
                else if (_canRotatingBackward && _rotatingForward && !_rotatingBackward)
                {
                    _canRotatingForward = true;
                    _canRotatingBackward = false;
                }
            }
            else
            {
                float curveEvaluate = _brakeCurveSpeedRotation.Evaluate(_internTimerCurveBrakeSpeedRotationX / _durationBrakeCurveSpeedRotation);
                if (_canRotatingForward && _currentRotationX < _finalAngleX && _currentRotationX > 0)
                {
                    _currentRotationX += _speedRotationForward * Time.deltaTime * curveEvaluate;
                    _cam.transform.RotateAround(_pivotPosRotation.position, Vector3.up, (_speedRotationForward * Time.deltaTime) * curveEvaluate);
                }
                else if (_canRotatingBackward && _currentRotationX > 0 && _currentRotationX < _finalAngleX)
                {
                    _currentRotationX -= _speedRotationBackward * Time.deltaTime * curveEvaluate;
                    _cam.transform.RotateAround(_pivotPosRotation.position, Vector3.up, -(_speedRotationBackward * Time.deltaTime) * curveEvaluate);
                }
            }
        }
    }

    private void FreinRight()
    {
        if ((_canRotatingForward && !_rotatingForward && _rotatingBackward) || (_canRotatingBackward && _rotatingForward && !_rotatingBackward) || !_rotatingForward && !_rotatingBackward)
        {
            _internTimerCurveBrakeSpeedRotationX += Time.deltaTime;
            _internTimerCurveSpeedRotationX = 0f;
            if (_internTimerCurveBrakeSpeedRotationX >= _durationBrakeCurveSpeedRotation && !_waitNewRotationX)
            {
                _internTimerCurveBrakeSpeedRotationX = 0f;
                if ((_canRotatingForward && !_rotatingForward && _rotatingBackward))
                {
                    _canRotatingForward = false;
                    _canRotatingBackward = true;
                }
                else if (_canRotatingBackward && _rotatingForward && !_rotatingBackward)
                {
                    _canRotatingForward = true;
                    _canRotatingBackward = false;
                }
            }
            else
            {
                float curveEvaluate = _brakeCurveSpeedRotation.Evaluate(_internTimerCurveBrakeSpeedRotationX / _durationBrakeCurveSpeedRotation);
                if (_canRotatingForward && _currentRotationX > -_finalAngleX && _currentRotationX < 0)
                {
                    _currentRotationX -= _speedRotationForward * Time.deltaTime * curveEvaluate;
                    _cam.transform.RotateAround(_pivotPosRotation.position, Vector3.up, -(_speedRotationForward * Time.deltaTime) * curveEvaluate);
                }
                else if (_canRotatingBackward && _currentRotationX < 0 && _currentRotationX > -_finalAngleX)
                {
                    _currentRotationX += _speedRotationBackward * Time.deltaTime * curveEvaluate;
                    _cam.transform.RotateAround(_pivotPosRotation.position, Vector3.up, (_speedRotationBackward * Time.deltaTime) * curveEvaluate);
                }
            }
        }
    }

    private void FreinUp()
    {
        if ((_canRotatingUp && !_rotatingUp && _rotatingDown) || (_canRotatingDown && _rotatingUp && !_rotatingDown) || !_rotatingUp && !_rotatingDown)
        {
            _internTimerCurveBrakeSpeedRotationY += Time.deltaTime;
            _internTimerCurveSpeedRotationY = 0f;
            if (_internTimerCurveBrakeSpeedRotationY >= _durationBrakeCurveSpeedRotation && !_waitNewRotationY)
            {
                _internTimerCurveBrakeSpeedRotationY = 0f;
                if ((_canRotatingUp && !_rotatingUp && _rotatingDown))
                {
                    _canRotatingUp = false;
                    _canRotatingDown = true;
                }
                else if (_canRotatingDown && _rotatingUp && !_rotatingDown)
                {
                    _canRotatingUp = true;
                    _canRotatingDown = false;
                }
            }
            else
            {
                float curveEvaluate = _brakeCurveSpeedRotation.Evaluate(_internTimerCurveBrakeSpeedRotationY / _durationBrakeCurveSpeedRotation);
                if (_canRotatingUp && _currentRotationY < _finalAngleY && _currentRotationY > 0)
                {
                    _currentRotationY += _speedRotationUp * Time.deltaTime * curveEvaluate;
                    _cam.transform.RotateAround(_pivotPosRotation.position, Vector3.back, (_speedRotationUp * Time.deltaTime) * curveEvaluate);
                }
                else if (_canRotatingDown && _currentRotationY > 0 && _currentRotationY < _finalAngleY)
                {
                    _currentRotationY -= _speedRotationDown * Time.deltaTime * curveEvaluate;
                    _cam.transform.RotateAround(_pivotPosRotation.position, Vector3.back, -(_speedRotationDown * Time.deltaTime) * curveEvaluate);
                }
            }
        }
    }

    private void FreinDown()
    {
        if ((_canRotatingUp && !_rotatingUp && _rotatingDown) || (_canRotatingDown && _rotatingUp && !_rotatingDown) || !_rotatingUp && !_rotatingDown)
        {
            _internTimerCurveBrakeSpeedRotationY += Time.deltaTime;
            _internTimerCurveSpeedRotationY = 0f;
            if (_internTimerCurveBrakeSpeedRotationY >= _durationBrakeCurveSpeedRotation && !_waitNewRotationY)
            {
                _internTimerCurveBrakeSpeedRotationY = 0f;
                if ((_canRotatingUp && !_rotatingUp && _rotatingDown))
                {
                    _canRotatingUp = false;
                    _canRotatingDown = true;
                }
                else if (_canRotatingDown && _rotatingUp && !_rotatingDown)
                {
                    _canRotatingUp = true;
                    _canRotatingDown = false;
                }
            }
            else
            {
                float curveEvaluate = _brakeCurveSpeedRotation.Evaluate(_internTimerCurveBrakeSpeedRotationY / _durationBrakeCurveSpeedRotation);
                if (_canRotatingUp && _currentRotationY > -_finalAngleY && _currentRotationY < 0)
                {
                    _currentRotationY -= _speedRotationUp * Time.deltaTime * curveEvaluate;
                    _cam.transform.RotateAround(_pivotPosRotation.position, Vector3.back, -(_speedRotationUp * Time.deltaTime) * curveEvaluate);
                }
                else if (_canRotatingDown && _currentRotationY < 0 && _currentRotationY > -_finalAngleY)
                {
                    _currentRotationY += _speedRotationDown * Time.deltaTime * curveEvaluate;
                    _cam.transform.RotateAround(_pivotPosRotation.position, Vector3.back, (_speedRotationDown * Time.deltaTime) * curveEvaluate);
                }
            }
        }
    }

}
