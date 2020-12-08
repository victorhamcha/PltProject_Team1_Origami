using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class TopDownController : MonoBehaviour
{
    [SerializeField] private GameObject _moveParticle;
    private GameObject _ob;
    public Entity entity;
    public LayerMask playerMask;
    private float _timer = 0.2f;
    private float duration = 0.2f;

    void Update()
    {
        _timer -= Time.deltaTime;
        if (entity.moveModeOn)
        {
            ClickClickManager.Instance.RaycastClick(playerMask);
            if (ClickClickManager.Instance.isTouch && ClickClickManager.Instance.isTouchTarget)
            {
                entity.MoveToDestination(ClickClickManager.Instance.hit.point);

                if (_timer <= 0)
                {
                    Destroy(_ob);
                    _ob = Instantiate(_moveParticle, ClickClickManager.Instance.hit.point, Quaternion.identity);
                    _timer = duration;
                }
            }
        }

    }

}
