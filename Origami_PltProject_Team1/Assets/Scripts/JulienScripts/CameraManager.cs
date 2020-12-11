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
    [SerializeField] private AnimationCurve _zoomCurve = null;
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
    public Transform _endPosRotation = null;
    [SerializeField] private AnimationCurve _curveSpeedRotation = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private float _durationCurveSpeedRotation = 1f;
    [SerializeField] private AnimationCurve _brakeCurveSpeedRotation = AnimationCurve.Linear(0, 1, 1, 0);
    [SerializeField] private float _durationBrakeCurveSpeedRotation = 1f;
    public float _finalAngle = 0f;
    public float _speedRotationBackward = 2f;
    public float _speedRotationForward = 2f;
    [HideInInspector] public bool _rotatingForward = false;
    [HideInInspector] public bool _rotatingBackward = false;
    private bool _waitNewRotation = true;
    [HideInInspector] public bool _rotationEnded = true;
    private bool _canRotatingForward = true;
    private bool _canRotatingBackward = false;
    private float _currentRotation = 0f;
    private float _internTimerCurveBrakeSpeedRotation = 0f;
    private float _internTimerCurveSpeedRotation = 0f;

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
        if (_rotatingForward && _finalAngle > _currentRotation && _canRotatingForward)
        {
            RotateForward();
        }
        else if (_rotatingBackward && _currentRotation > 0 && _canRotatingBackward)
        {
            RotateBackward();
        }

        if (_rotatingBackward && _currentRotation <= 0)
        {
            _rotationEnded = true;
            _canRotatingForward = true;
            _canRotatingBackward = false;
            _rotatingForward = false;
            _rotatingBackward = false;
            _currentRotation = 0;
        }
        else if (_rotatingForward && _currentRotation >= _finalAngle)
        {
            _canRotatingForward = false;
            _canRotatingBackward = true;
            _rotatingForward = false;
            _rotatingBackward = false;
            _currentRotation = _finalAngle;
        }
        else
        {
            if ((_canRotatingForward && !_rotatingForward && _rotatingBackward) || (_canRotatingBackward && _rotatingForward && !_rotatingBackward) || !_rotatingForward && !_rotatingBackward )
            {
                _internTimerCurveBrakeSpeedRotation += Time.deltaTime;
                _internTimerCurveSpeedRotation = 0f;
                if (_internTimerCurveBrakeSpeedRotation >= _durationBrakeCurveSpeedRotation && !_waitNewRotation)
                {
                    _internTimerCurveBrakeSpeedRotation = 0f;
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
                    float curveEvaluate = _brakeCurveSpeedRotation.Evaluate(_internTimerCurveBrakeSpeedRotation / _durationBrakeCurveSpeedRotation);
                    if (_canRotatingForward && _currentRotation < _finalAngle && _currentRotation > 0)
                    {
                        _currentRotation += _speedRotationForward * Time.deltaTime * curveEvaluate;
                        _cam.transform.RotateAround(_endPosRotation.position, Vector3.up, (_speedRotationForward * Time.deltaTime) * curveEvaluate);
                    }
                    else if (_canRotatingBackward && _currentRotation > 0 && _currentRotation < _finalAngle)
                    {
                        _currentRotation -= _speedRotationBackward * Time.deltaTime * curveEvaluate;
                        _cam.transform.RotateAround(_endPosRotation.position, Vector3.up, -(_speedRotationBackward * Time.deltaTime) * curveEvaluate);
                    }
                }
            }


        }

        if (_rotatingBackward || _rotatingForward)
        {
            _waitNewRotation = false;
        }
        else
        {
            _waitNewRotation = true;
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
        _canRotatingForward = true;
        _rotatingBackward = false;
        _rotationEnded = false;
        if (_internTimerCurveSpeedRotation < _durationCurveSpeedRotation)
        {
            _internTimerCurveSpeedRotation += Time.deltaTime;
        }
        _currentRotation += _speedRotationForward * Time.deltaTime * _curveSpeedRotation.Evaluate(_internTimerCurveSpeedRotation / _durationCurveSpeedRotation);
        _cam.transform.RotateAround(_endPosRotation.position, Vector3.up, (_speedRotationForward * Time.deltaTime) * _curveSpeedRotation.Evaluate(_internTimerCurveSpeedRotation / _durationCurveSpeedRotation));
    }

    public void RotateBackward()
    {
        _canRotatingBackward = true;
        _rotatingForward = false;
        if (_internTimerCurveSpeedRotation < _durationCurveSpeedRotation)
        {
            _internTimerCurveSpeedRotation += Time.deltaTime;
        }
        _currentRotation -= _speedRotationBackward * Time.deltaTime * _curveSpeedRotation.Evaluate(_internTimerCurveSpeedRotation / _durationCurveSpeedRotation);
        _cam.transform.RotateAround(_endPosRotation.position, Vector3.up, -(_speedRotationBackward * Time.deltaTime) * _curveSpeedRotation.Evaluate(_internTimerCurveSpeedRotation / _durationCurveSpeedRotation));
    }

    public void CameraFollow()
    {
        if (_rotationEnded)
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
            //Debug.Log(_lastSpeed);
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
}
