using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera _cam = null;
    private float _timer = 0.0f;
    private float _startSize = 0.0f;
    private float _originalStartSize = 0.0f;
    private float _originalEndSize = 0.0f;

    [Header("Curve du zoom caméra")]
    [SerializeField] private AnimationCurve _zoomCurve = null;

    [Header("Paramètres")]
    [SerializeField] [Range(1,50)] private float _endSize = 5.0f;
    [SerializeField] [Range(0.1f, 30.0f)] private float _zoomSpeed = 10.0f;
    [SerializeField] [Range(0.1f, 30.0f)] private float _dezoomSpeed = 5.0f;

    [Header("Zoom / Dezoom")]
    [SerializeField] private bool _zooming = false;
    [SerializeField] private bool _dezooming = false;
    [SerializeField] private bool _zoomStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _originalStartSize = _cam.orthographicSize;
        _originalEndSize = _endSize;
        _startSize = _originalStartSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (_zooming && !_zoomStopped)
        {
            _startSize = _originalStartSize;
            _endSize = _originalEndSize;
            _timer += Time.deltaTime;
            _cam.orthographicSize = Mathf.Lerp(_startSize, _endSize, _zoomCurve.Evaluate(_timer / _zoomSpeed));

        }
        else if (_dezooming)
        {
            if(_zoomStopped)
            {
                _endSize = _cam.orthographicSize;
                _startSize = _originalStartSize;
                _timer += Time.deltaTime;
                _cam.orthographicSize = Mathf.Lerp(_endSize, _startSize, _zoomCurve.Evaluate(_timer / (_dezoomSpeed * 10)));
            }
            else
            {
                _endSize = _originalEndSize;
                _startSize = _originalStartSize;
                _timer += Time.deltaTime;
                _cam.orthographicSize = Mathf.Lerp(_endSize, _startSize, _zoomCurve.Evaluate(_timer / _dezoomSpeed));
            }
            
        }
        /*else if (_zoomStopped)
        {
            _timer += Time.deltaTime;
            _startSize = _cam.orthographicSize;
            _endSize = _originalSize;
            _cam.orthographicSize = Mathf.Lerp(_startSize, _endSize, _curve.Evaluate(_timer / _dezoomSpeed / 100));
        }*/
        else
        {
            _timer = 0.0f;
        }
    }
}
