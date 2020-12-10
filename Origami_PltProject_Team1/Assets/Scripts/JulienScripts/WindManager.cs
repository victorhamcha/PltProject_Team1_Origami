using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class WindManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCam = null;
    [SerializeField] private VideoClip _windClip = null;
    public GameObject windGo = null;
    private Vector2 _screenPoint;
    private Vector3 _worldPos;
    private float _randomSpeed = 0.0f;

    [SerializeField] private Canvas _canvas = null;
    private RectTransform _rect = null;

    void Start()
    {
       _rect = _canvas.GetComponent<RectTransform>();
        StartCoroutine("SetupWind");
    }

    IEnumerator SetupWind()
    {
        GameObject wind = null;
        if (!GameManager.Instance.GetSwitchModePlayerOrigami()._isOnModeOrigami && !PauseScript.paused)
        {
            _randomSpeed = Random.Range(1.0f, 5.0f);
            // _screenPoint = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), _mainCam.nearClipPlane);
            // _worldPos = _mainCam.ScreenToWorldPoint(_screenPoint);
            _screenPoint = new Vector2(Random.Range(0, _rect.rect.width), Random.Range(0, _rect.rect.height));
            
            wind = Instantiate(windGo, _screenPoint, Quaternion.identity,_canvas.transform);
            
        }
        yield return new WaitForSeconds((float)_windClip.length + _randomSpeed);
        Destroy(wind);
        StartCoroutine("SetupWind");
    }
}
