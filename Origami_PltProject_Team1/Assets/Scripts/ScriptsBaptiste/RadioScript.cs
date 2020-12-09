using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{
    //[SerializeField] private AudioSource RadioSource;
    //[SerializeField] private AudioClip[] RadioClips;

    [SerializeField] private GameObject _bulle = null;
    [SerializeField] private GameObject MusicParticles;
    private bool _isTrigger = false;
    private int _bubuleCount = 0;
    private float _timer = 0.2f;
    private float duration = 0.2f;
    public LayerMask layerBubule;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isTrigger )
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
        Debug.Log("Active");
        //Activer son
        if (_bubuleCount == 0)
        {
            MusicParticles.gameObject.SetActive(true);
            _bubuleCount++;
        }
        else if (_bubuleCount == 1)
        {
            //Désactiver son
            Debug.Log("Désactive");
            MusicParticles.gameObject.SetActive(false);
            _bubuleCount = 0;
        }
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        ClickClickManager.Instance.RaycastClick(layerBubule);
        if (ClickClickManager.Instance.isTouch && ClickClickManager.Instance.isTouchTarget && _isTrigger)
        {
            if (_timer <= 0)
            {
                ClickClickBubule();
                _timer = duration;
            }
        }
    }

}
