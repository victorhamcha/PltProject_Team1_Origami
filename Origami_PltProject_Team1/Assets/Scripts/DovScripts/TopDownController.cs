using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class TopDownController : MonoBehaviour
{
    public TopDownEntity entity;


    void Update()
    {
        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveDir.x = -1f;
            //keyboardActive = true;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            moveDir.x = 1f;
            //keyboardActive = true;
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            moveDir.y = 1f;
            //keyboardActive = true;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            moveDir.y = -1f;
            //keyboardActive = true;
        }

        entity.Move(moveDir);


        //if (!keyboardActive) {
        //    if (Input.GetMouseButton(0)) {
        //        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        entity.MoveToDestination(worldMousePosition);
        //    } else {
        //        entity.MoveStop();
        //    }
        //}
    }
}
