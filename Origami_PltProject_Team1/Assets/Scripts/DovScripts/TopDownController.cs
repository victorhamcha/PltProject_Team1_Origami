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


        //if (Input.GetMouseButton(0))
        //{
        //    //Raycast --> ScreenPointToRay --> Input.mousePosition.x, 0f, Input.mousePosition.z
        //    Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    entity.MoveToDestination(worldMousePosition);
        //}
        //else
        //{
        //    entity.MoveStop();
        //}

    }
}
