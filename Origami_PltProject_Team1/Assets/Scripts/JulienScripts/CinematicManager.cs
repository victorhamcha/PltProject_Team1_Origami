using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private VideoClip _cinematicVideo = null;
    [SerializeField] private AudioSource _soundScript = null;
    [SerializeField] private Entity _moveScript = null;
    [SerializeField] private WindManager _windScript = null;

    private GameObject _goWindTemp = null;
    private float _soundScriptVolumeValue = 0.0f;

    private float _timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        _moveScript.MoveStop();
        _soundScriptVolumeValue = _soundScript.volume;
        _soundScript.volume = 0.0f;
        _goWindTemp = _windScript.windGo;
        _windScript.windGo = null;

    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > _cinematicVideo.length)
        {
            Debug.Log("DESTROY");
            _moveScript.MovePlay();
            _soundScript.volume = _soundScriptVolumeValue;
            _windScript.enabled = true;
            _windScript.windGo = _goWindTemp;
            Destroy(this);
        }

    }
}
