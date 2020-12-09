using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class ZoomVignette : MonoBehaviour
{
   // private CameraManager _cameraManager = null;


    private Vignette _vignette = null;
    private Volume _volume;

    [SerializeField] [Range(0, 100)] private float variablePli = 0.0f;
    [SerializeField] private float _intensityFactor = 0.05f;
    [SerializeField] private float _zoomFactor = 0.1f;
    private float _intensityValue = 0.1f;

    [SerializeField] private float _startOrthoSize = 0.0f;
    [SerializeField] private float _endOrthoSize = 0.0f;
    [SerializeField] private float _changedOrthoSize = 0.0f;
    private Camera _cam = null;

    private float _timerDezoom = 0.0f;
    public bool _testBool = false;
    // Start is called before the first frame update
    void Start()
    {
        //_cameraManager = GameManager.Instance.GetCameraManager();

        _cam = GetComponent<Camera>();
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet<Vignette>(out _vignette);
       // _startOrthoSize = _cam.orthographicSize;
       // _changedOrthoSize = _startOrthoSize;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (_testBool)
        {
            VignetteZoom();
        }
        else
        {
            VignetteDezoom();
        }

        // intensité = (% du pli / 100) * facteur ([0;1]);
        // orthographicSize -= facteurZoom;
    }

    private void VignetteZoom()
    {
        _intensityValue = (variablePli / 100) * _intensityFactor;
        _vignette.intensity.value = _intensityValue;
        //_changedOrthoSize -= (variablePli / 100) * _zoomFactor;
        //_cam.orthographicSize = _changedOrthoSize;
        //_endOrthoSize = _changedOrthoSize;
        //_changedOrthoSize = _startOrthoSize;

    }

    private void VignetteDezoom()
    {
        _timerDezoom += Time.deltaTime;
        _vignette.intensity.value = Mathf.Lerp(_intensityValue, 0, _timerDezoom);        
       // _cam.orthographicSize = Mathf.Lerp(_endOrthoSize, _startOrthoSize, _timerDezoom);
    }

    public void SetVariablePli(float pourcentagePli)
    {
        variablePli = pourcentagePli;
    }
}
