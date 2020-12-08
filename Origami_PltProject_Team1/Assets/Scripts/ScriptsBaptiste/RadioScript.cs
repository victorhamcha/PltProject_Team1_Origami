using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private GameObject _bulle = null;

    [SerializeField] private AudioSource RadioSource;
    [SerializeField] private AudioClip[] RadioClips;

    [SerializeField] private GameObject MusicParticles;
    private bool _isTrigger = false;
    public LayerMask layerBubule;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

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
        int ClipIndex = Random.Range(0, RadioClips.Length);
        RadioSource.PlayOneShot(RadioClips[ClipIndex]);
        MusicParticles.gameObject.SetActive(true);
    }

    private void Update()
    {
        ClickClickManager.Instance.RaycastClick(layerBubule);
        if (ClickClickManager.Instance.isTouch && ClickClickManager.Instance.isTouchTarget && _isTrigger)
        {
            ClickClickBubule();
        }
    }

}
