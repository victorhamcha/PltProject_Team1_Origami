using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchScript : MonoBehaviour
{
    [SerializeField] private GameObject _bulle = null;
    [SerializeField] private Transform _posBench = null;
    [SerializeField] private Transform _posEndBench = null;
    [SerializeField] private Transform _posPlayer = null;
    [SerializeField] private GameObject _cameraCollider = null;
    private bool _isTrigger = false;
    private float _timer = 0.0f;
    private float _timerClickClick = 0.2f;
    private float _durationClickClick = 0.2f;
    private int _bubuleCount = 0;
    public LayerMask layerBubule;
    public LayerMask layerPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isTrigger)
        {
            _isTrigger = true;
            _bulle.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _bulle.SetActive(false);
            _isTrigger = false;
        }
    }

    private void ClickClickBubule()
    {
        if (_bubuleCount == 0)
        {
            //Activer son
            GameManager.Instance.GetEntity().MoveStop();
            _posPlayer.position = _posBench.position;
            _posPlayer.rotation = _posBench.rotation;
            _cameraCollider.SetActive(true);
            _bubuleCount++;
        }
        else if (_bubuleCount == 1)
        {
            _timer = 0.01f;
            //Désactiver son
            _bubuleCount = 0;
        }
    }

    private void ExitClickClickBubule()
    {
        if (_bubuleCount == 1)
        {
            _timer = 0.01f;
            //Désactiver son
        }
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        _timerClickClick -= Time.deltaTime;

        ClickClickManager.Instance.RaycastClick(layerBubule);
        if (ClickClickManager.Instance.isTouch && ClickClickManager.Instance.isTouchTarget && _isTrigger)
        {
            if (_timerClickClick <= 0)
            {
                ClickClickBubule();
                _timerClickClick = _durationClickClick;
            }
        }

        ClickClickManager.Instance.RaycastClick(layerPlayer);
        if (ClickClickManager.Instance.isTouch && ClickClickManager.Instance.isTouchTarget && _isTrigger)
        {
            if (_timerClickClick <= 0)
            {
                ExitClickClickBubule();
                _timerClickClick = _durationClickClick;
            }
        }

        if (_timer < 0)
        {
            _cameraCollider.GetComponent<CameraCollider>().ExitCollider();
            _cameraCollider.SetActive(false);
            _posPlayer.position = _posEndBench.position;
            GameManager.Instance.GetEntity().MovePlay();
            _timer = 0f;
        }
    }
}
