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

    private Camera _cam = null;
    private float _timerZoom = 0.0f;
    private float _startSize = 0.0f;
    private float _originalStartSize = 0.0f;
    private float _originalEndSize = 0.0f;

    [Header("Camera Zoom")]
    [SerializeField] private AnimationCurve _zoomCurve = null;
    [SerializeField] [Range(1, 50)] private float _endSize = 5.0f;
    [SerializeField] [Range(0.1f, 30.0f)] private float _zoomSpeed = 0.0f;
    [SerializeField] [Range(0.1f, 30.0f)] private float _dezoomSpeed = 0.0f;

    [SerializeField] private bool _zooming = false;
    [SerializeField] private bool _dezooming = false;
    //[SerializeField] private bool _zoomStopped = false;
    #endregion

    #region Camera Rotation variables
    [Header("Camera rotation")]
    [SerializeField] private Transform _endPosRotation = null;
    [SerializeField] [Range(1.0f, 30.0f)] private float _durationForward = 0f;
    [SerializeField] [Range(1.0f, 30.0f)] private float _durationBackward = 0f;
    [SerializeField] [Range(-360.0f, 360.0f)] private float _finalAngle = 0f;
    [SerializeField] private bool _rotatingForward = false;
    [SerializeField] private bool _rotatingBackward = false;
    private bool _rotationEnded = true;
    private float _timerRotation = 0f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        

        #region Zoom variables initialization
        _cam = Camera.main;
        _cam.transform.position = target.position + offset;
        _originalStartSize = _cam.orthographicSize;
        _originalEndSize = _endSize;
        _startSize = _originalStartSize;
        #endregion

        //_startPosRotation = _cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region Zoom code
        if (_zooming)
        {
            CameraZoomIn();
        }
        else if (_dezooming)
        {
            CameraZoomOut();
        }
        else
        {
            _zooming = false;
            _dezooming = false;
            _timerZoom = 0.0f;
            _endSize = _cam.orthographicSize;
        }
        #endregion

        #region Rotation code
        if (_rotatingForward && (_finalAngle * _timerRotation) / _durationForward < _finalAngle && _finalAngle > 0 && _finalAngle < 360)
        {
            RotateForwardClockwise();
        }
        else if (_rotatingBackward && (_finalAngle * _timerRotation) / _durationBackward < _finalAngle && _finalAngle > 0 && _finalAngle < 360)
        {
            RotateBackwardClockwise();
        }
        else if(_rotatingForward && (-_finalAngle * _timerRotation) / _durationForward < -_finalAngle && _finalAngle < 0 && _finalAngle > -360)
        {
            RotateForwardCounterclockwise();
        }
        else if (_rotatingBackward && (-_finalAngle * _timerRotation) / _durationBackward < -_finalAngle && _finalAngle < 0 && _finalAngle > -360)
        {
            RotateBackwardCounterclockwise();
        }
        else if (_rotatingBackward && _timerRotation > 1.0f)
        {
            
            _rotationEnded = true;
            _rotatingBackward = false;
        }
        else if(_rotationEnded)
        {
            _rotatingForward = false;
            _timerRotation = 0f;
            
        }
        else
        {
            _rotatingBackward = false;
            _rotatingForward = false;
            _timerRotation = 0f;
        }
        #endregion
    }

    void LateUpdate()
    {
        #region Follow code
        CameraFollow();
        #endregion
    }


    public void RotateForwardClockwise()
    {
        _rotatingBackward = false;
        _rotationEnded = false;
        _timerRotation += Time.deltaTime;
        //_cam.transform.position = Vector3.Slerp(_smoothedPosition, _endPosRotation.position, _rotationCurve.Evaluate(_timerRotation * _speedMultiplierForward));*/
        _cam.transform.RotateAround(_endPosRotation.position, Vector3.up, (_finalAngle * Time.deltaTime) / _durationForward);
    }

    public void RotateForwardCounterclockwise()
    {
        _rotatingBackward = false;
        _rotationEnded = false;
        _timerRotation += Time.deltaTime;
        //_cam.transform.position = Vector3.Slerp(_smoothedPosition, _endPosRotation.position, _rotationCurve.Evaluate(_timerRotation * _speedMultiplierForward));*/
        _cam.transform.RotateAround(_endPosRotation.position, Vector3.up, (_finalAngle * Time.deltaTime) / _durationForward);
    }

    public void RotateBackwardClockwise()
    {
        _rotatingForward = false;
        _timerRotation += Time.deltaTime;
        // _cam.transform.position = Vector3.Slerp(_endPosRotation.position, _smoothedPosition, _rotationCurve.Evaluate(_timerRotation * _speedMultiplierBackward));
        _cam.transform.RotateAround(_endPosRotation.position, Vector3.up, -(_finalAngle * Time.deltaTime) / _durationBackward); 
    }

    public void RotateBackwardCounterclockwise()
    {
        _rotatingForward = false;
        _timerRotation += Time.deltaTime;
        // _cam.transform.position = Vector3.Slerp(_endPosRotation.position, _smoothedPosition, _rotationCurve.Evaluate(_timerRotation * _speedMultiplierBackward));
        _cam.transform.RotateAround(_endPosRotation.position, Vector3.up, -(_finalAngle * Time.deltaTime) / _durationBackward);
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
        if(_timerZoom < _zoomSpeed)
        {
            //_dezooming = false;
            _startSize = _originalStartSize;
            _endSize = _originalEndSize;
            _timerZoom += Time.deltaTime;
            _cam.orthographicSize = Mathf.Lerp(_startSize, _endSize, _zoomCurve.Evaluate(_timerZoom / _zoomSpeed));

            if (_dezooming)
            {
                _dezooming = true;
                _zooming = false;
                _startSize = _cam.orthographicSize;
                _endSize = _originalStartSize;
            }
        }
        else
        {
            _startSize = _cam.orthographicSize;
            _endSize = _originalStartSize;
            _zooming = false;
            _timerZoom = 0.0f;
        }
    }

    public void CameraZoomOut()
    {
        if(_timerZoom < _dezoomSpeed)
        {
            // _zooming = false;
            _endSize =_originalStartSize;
            //_startSize = _originalStartSize;
            _timerZoom += Time.deltaTime;
            _cam.orthographicSize = Mathf.Lerp(_startSize, _endSize, _zoomCurve.Evaluate(_timerZoom / _dezoomSpeed));
            

            if (_zooming)
            {
                _zooming = true;
                _dezooming = false;
                _startSize = _cam.orthographicSize;
                _endSize = _originalEndSize;
            }
        }
        else
        {
            _startSize = _originalStartSize;
            _endSize = _originalEndSize;
            _dezooming = false;
            _timerZoom = 0.0f;
        }
    }
}
