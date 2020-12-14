using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints = null;
    [SerializeField] private float _minRandomTime = 0f;
    [SerializeField] private float _maxRandomTime = 0f;
    [SerializeField] private Camera cam;
    [SerializeField] private Sprite _mouteMouteQuiFlemme = null;
    [SerializeField] private Sprite _mouteMouteQuiBroute = null;
    [SerializeField] private bool _isBroutage = false;
    private Sprite _mouteMoute = null;
    private SpriteRenderer _mouteMouteRenderer = null;
    private bool _isMoving = false;
    private float _timer = 0f;
    private float _randomTime = 0f;
    private int _indexPoint = 0;
    private NavMeshAgent agent = null;

    //Rotator
    [Header("Rotator")]
    public float period = 0.05f;
    public float offset = 0.5f;
    private float _timerRotator = 0f;
    private float _offsetvalue = 0f;
    [SerializeField] private AnimationCurve _rotatorCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    private void Start()
    {
        _mouteMouteRenderer = GetComponent<SpriteRenderer>();
        _mouteMoute = _mouteMouteRenderer.sprite;
        agent = GetComponent<NavMeshAgent>();
        _randomTime = _minRandomTime;
    }

    private void Update()
    {
        agent.transform.LookAt(cam.transform);
        if (agent.remainingDistance < 0.1f)
        {
            _isMoving = false;
            _timer += Time.deltaTime;
        }
        else
        {
            Rotator();
        }

        if (!_isMoving && _timer > _randomTime)
        {
            GoToNextPoint();
            _timer = 0;
        }

        if (_randomTime > 10 && !_isMoving)
        {
            if (_isBroutage)
            {
                _mouteMouteRenderer.sprite = _mouteMouteQuiBroute;
            }
            else
            {
                _mouteMouteRenderer.sprite = _mouteMouteQuiFlemme;
            }
        }
    }

    private void GoToNextPoint()
    {
        _mouteMouteRenderer.sprite = _mouteMoute;
        _indexPoint = Random.Range(0, pathPoints.Length);
        _randomTime = Random.Range(_minRandomTime, _maxRandomTime);
        agent.destination = pathPoints[_indexPoint].position;
        Vector3 moveDir = new Vector3(agent.destination.x - transform.position.x, 0, agent.destination.z - transform.position.z);
        if (moveDir.normalized.z > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        _isMoving = true;
    }


    private void Rotator()
    {
        _timerRotator += Time.deltaTime;

        float t = Mathf.PingPong(_timerRotator, period);
        float r = t / period;

        Vector3 eulerAngles = transform.eulerAngles;

        eulerAngles.z = _offsetvalue;
        _offsetvalue = Mathf.Lerp(-offset, offset, r);
        eulerAngles.z += _offsetvalue;
        eulerAngles.x -= _rotatorCurve.Evaluate(_timerRotator);

        transform.localEulerAngles = eulerAngles;
    }

}
