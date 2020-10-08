using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class TopDownController : MonoBehaviour
{
    public TopDownEntity entity;
    private Vector3 moveDir = Vector3.zero;


    void Update()
    {

        entity.Move(moveDir);







        //controller.Move(direction * speed * Time.deltaTime);

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
