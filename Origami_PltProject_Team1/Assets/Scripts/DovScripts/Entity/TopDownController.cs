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
    public LayerMask obstacleMask;
    private float _timer = 0.2f;
    private float duration = 0.2f;


    void Update()
    {
        _timer -= Time.deltaTime;
        if (entity.moveModeOn)
        {
            ClickClickManager.Instance.RaycastClickWithObstacle(playerMask, obstacleMask);
            if (ClickClickManager.Instance.isTouch && ClickClickManager.Instance.isTouchTarget)
            {
                entity.MoveToDestination(ClickClickManager.Instance.hit.point);

                if (_timer <= 0)
                {
                    Destroy(_ob);
                    Vector3 hitpos = new Vector3(ClickClickManager.Instance.hit.point.x, ClickClickManager.Instance.hit.point.y + 0.01f, ClickClickManager.Instance.hit.point.z);
                    _ob = Instantiate(_moveParticle, hitpos, Quaternion.identity);
                    _timer = duration;
                }
            }
        }

    }

}
