using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class TopDownController : MonoBehaviour
{
    public Entity entity;
    public LayerMask playerMask;
    private Vector3 moveDir = Vector3.zero;

    void Update()
    {
        if (entity.moveModeOn)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, 0);
                Ray ray = Camera.main.ScreenPointToRay(touchPos);

                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, playerMask))
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
                    entity.MoveToDestination(hit.point);
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
                    Debug.Log("Did not Hit");
                }

            }
        }

    }

}
