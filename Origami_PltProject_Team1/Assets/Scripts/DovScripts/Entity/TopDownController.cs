using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class TopDownController : MonoBehaviour
{
    public Entity entity;
    public LayerMask playerMask;

    void Update()
    {
        if (entity.moveModeOn)
        {
            ClickClickManager.Instance.RaycastClick(playerMask);
            if (ClickClickManager.Instance.isTouch && ClickClickManager.Instance.isTouchTarget)
            {
                entity.MoveToDestination(ClickClickManager.Instance.hit.point);
            }
        }

    }

}
