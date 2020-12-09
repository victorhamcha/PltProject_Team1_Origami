using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeuDeCamp : MonoBehaviour
{
    [SerializeField] private Animation _animation;
    [SerializeField] private GameObject _bulle = null;
    public LayerMask layerBubule;

    private bool _isTrigger = false;
    private float _timer = 0.2f;
    private float duration = 0.2f;

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
            _isTrigger = false;
            _bulle.SetActive(false);
        }
    }

    private void ClickClickBubule()
    {
        // Activer son
        // Lancer l'anim
    }

    // Update is called once per frame
    void Update()
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
