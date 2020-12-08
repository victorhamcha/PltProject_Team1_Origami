using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WindManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCam = null;
    [SerializeField] private VideoClip _windClip = null;
    public GameObject windGo = null;
    private Vector3 _screenPoint;
    private Vector3 _worldPos;
    private float _randomSpeed = 0.0f;

    void Start()
    {
        StartCoroutine("SetupWind");
    }

    IEnumerator SetupWind()
    {

        
       
        if (!GameManager.Instance.GetSwitchModePlayerOrigami()._isOnModeOrigami && !PauseScript.paused)
        {
            _randomSpeed = Random.Range(1.0f, 5.0f);
            _screenPoint = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), _mainCam.nearClipPlane);
            _worldPos = _mainCam.ScreenToWorldPoint(_screenPoint);
            GameObject wind = Instantiate(windGo, _worldPos, Quaternion.identity);

            yield return new WaitForSeconds((float)_windClip.length + _randomSpeed);
            Destroy(wind);
            StartCoroutine("SetupWind");
        }
    }
}
