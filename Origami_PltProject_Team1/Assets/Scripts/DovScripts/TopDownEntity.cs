using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Rewired;

public class TopDownEntity : MonoBehaviour
{
    //Movement
    [Header("Movement")]
    public float acceleration = 5f;
    public float speedMax = 10f;
    public Vector3 _moveDir = Vector3.zero;

    //Destination
    /*[Header("Movement Destination")]
    public float moveDestinationStartSlowdownRange = 2f;
    public float moveDestinationSpeedMin = 1f;
    private Vector3 _moveDestination = Vector3.zero;
    private bool _isMovingToDestination = false;
    private float _moveDestinationRange = 0.25f;
    private float _moveDestinationSpeed = 0f;

    private float _moveDestinationRefreshDirDuration = 0.1f;
    private float _moveDestinationRefreshDirCountdown = -1f;*/

    //Frictions
    [Header("Frictions")]
    public float friction = 20f;
    public float turnFriction = 50f;

    //Speed
    private Vector3 _velocity = Vector3.zero;

    public Rigidbody _rigidbody = null;

    private Player player;
    private int playerID = 0;

    //Orient
    [Header("Orient")]
   //public GameObject orientSprite = null;
   //public float orientSpeed = 20f;
   private Vector3 _orientDir = Vector3.right;
   private float turnSmoothTime = 0.1f;
   private float turnSmoothVelocity;


    #region Functions Unity Callbacks

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        player = ReInput.players.GetPlayer(playerID);
    }


    private void FixedUpdate()
    {
        _UpdateMove();
        _ApplySpeed();
    }

    private void Update()
    {
        _UpdateSpriteOrient();
    }

    private void OnGUI()
    {
        GUIStyle debugStyle = new GUIStyle();
        debugStyle.normal.textColor = Color.white;
        debugStyle.fontSize = 48;
        GUILayout.BeginVertical();
        GUILayout.Label(gameObject.name, debugStyle);
        GUILayout.Label("Move Dir = " + _moveDir, debugStyle);
        //GUILayout.Label("Move Destination Speed = " + _moveDestinationSpeed, debugStyle);
        //GUILayout.Label("Move Destination = " + _moveDestination, debugStyle);
        GUILayout.Label("Velocity = " + _velocity, debugStyle);
        GUILayout.EndVertical();
    }

    #endregion

    #region Functions Move

    public bool IsMoving {
        get { return _moveDir != Vector3.zero; }
    }

    public void Move(Vector3 dir)
    {
        _moveDir = dir.normalized;
    }

    public void MoveStop()
    {
        _moveDir = Vector3.zero;
        //_isMovingToDestination = false;
    }

    /*public void MoveToDestination(Vector3 destination)
    {
        _moveDestination = destination;
        _moveDestination.z = 0f;
        _isMovingToDestination = true;
        _moveDestinationRefreshDirCountdown = -1f;
    }*/

    private void _UpdateMove()
    {
        #region scriptsouris
        /*if (_isMovingToDestination) {
            bool hasReachedDestination = false;
            float distFromDestination = (_moveDestination - transform.position).magnitude;
            if (distFromDestination <= _moveDestinationRange) {
                hasReachedDestination = true;
            }
            Vector3 dirFromDestination = (_moveDestination - transform.position).normalized;
            if (Vector3.Dot(_moveDir, dirFromDestination) < 0f) {
                hasReachedDestination = true;
            }

            if (hasReachedDestination) {
                //Entity has reached destination
                Move(Vector3.zero);
                _velocity = Vector3.zero;
                _isMovingToDestination = false;
            } else {
                _moveDestinationRefreshDirCountdown -= Time.fixedDeltaTime;
                if (_moveDestinationRefreshDirCountdown <= 0f) {
                    Vector3 moveDir = (_moveDestination - transform.position).normalized;
                    Move(moveDir);
                    _moveDestinationRefreshDirCountdown = _moveDestinationRefreshDirDuration;
                }
            }
        }*/
        #endregion

        if (_moveDir != Vector3.zero)
        {
            //Apply Turn Frictions
            float angle = Vector2.SignedAngle(_velocity, _moveDir);
            float angleRatio = Mathf.Abs(angle) / 360f;
            float frictionToApply = turnFriction * angleRatio * Time.fixedDeltaTime;
            Vector3 frictionDir = -_velocity.normalized;
            _velocity += frictionDir * frictionToApply;

            _velocity += _moveDir * acceleration * Time.fixedDeltaTime;
            if (_velocity.sqrMagnitude > speedMax * speedMax)
            {
                _velocity = _velocity.normalized * speedMax;
            }

            _orientDir = _moveDir;
        }
        else
        {
            float frictionToApply = friction * Time.fixedDeltaTime;
            if (_velocity.sqrMagnitude > frictionToApply * frictionToApply)
            {
                _velocity -= _velocity.normalized * frictionToApply;
            }
            else
            {
                _velocity = Vector3.zero;
            }
        }

        /*if (_isMovingToDestination) {
            float distFromDestination = (_moveDestination - transform.position).magnitude;
            if (distFromDestination <= moveDestinationStartSlowdownRange) {
                float ratio = distFromDestination / moveDestinationStartSlowdownRange;
                float speed = Mathf.Lerp(0f, _moveDestinationSpeed, ratio);
                _velocity = _velocity.normalized * speed;
            } else {
                _moveDestinationSpeed = _velocity.magnitude;
                if (_moveDestinationSpeed < moveDestinationSpeedMin) {
                    _moveDestinationSpeed = moveDestinationSpeedMin;
                }
            }
        }*/
    }

    #endregion


    #region Functions Orient

    private void _UpdateSpriteOrient()
    {
        float horizontal = player.GetAxisRaw("Move Horizontal");
        float vertical = player.GetAxisRaw("Move Vertical");
        _orientDir = new Vector3(-horizontal, 0f, -vertical).normalized;
        _moveDir = _orientDir;

        if (_orientDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_orientDir.x, _orientDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

        }
        /*float startAngle = Vector2.SignedAngle(Vector2.right, orientSprite.transform.right);
        float endAngle = startAngle + Vector2.SignedAngle(orientSprite.transform.right, _orientDir);

        float angle = Mathf.Lerp(startAngle, endAngle, Time.deltaTime * orientSpeed);

        Vector3 eulerAngles = orientSprite.transform.eulerAngles;
        eulerAngles.z = angle;
        orientSprite.transform.eulerAngles = eulerAngles;*/
    }

    #endregion

    #region Functions Speed

    private void _ApplySpeed()
    {
        if (null != _rigidbody)
        {
            _rigidbody.velocity = _velocity;
        }
        else
        {
            Vector3 position = transform.position;
            position += _velocity * Time.fixedDeltaTime;
            transform.position = position;
        }
    }

    #endregion

}
