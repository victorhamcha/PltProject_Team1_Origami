using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalMovement : MonoBehaviour
{
    private float _timer = 0f;
    public Transform pathPoint;
    private NavMeshAgent agent = null;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = pathPoint.position;
        //agent.Warp(pathPoint.position);
        //agent.Resume();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= 0.5f)
        {
            agent.destination = pathPoint.position;
            _timer = 0;
        }
    }


}
