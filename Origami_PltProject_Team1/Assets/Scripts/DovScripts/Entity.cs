using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Properties

    //Movement
    private Vector3 _moveDir = Vector3.zero;
    private Vector3 _previousMoveDir = Vector3.zero;
    private bool _wasMoving = false;

    //Destination
    public float moveDestinationStartSlowdownRange = 2f;
    public float moveDestinationSpeedMin = 1f;
    private Vector3 _moveDestination = Vector3.zero;
    public bool _isMovingToDestination = false;
    private float _moveDestinationRange = 0.25f;
    private float _moveDestinationSpeed = 0f;

    private float _moveDestinationRefreshDirDuration = 0.1f;
    private float _moveDestinationRefreshDirCountdown = -1f;

    //Speed
    [Header("Speed")]
    [SerializeField] public float _speedMax = 5f;
    private Vector3 _velocity = Vector3.zero;

    //Acceleration 
    [Header("Acceleration")]
    [SerializeField] private float _accelerationDuration = 1f;
    [SerializeField] private AnimationCurve _accelerationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    private float _accelerationTimer = 0f;

    //Frictions
    [Header("Frictions")]
    [SerializeField] private float _frictionsDuration = 1f;
    [SerializeField] private AnimationCurve _frictonsCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    private float _frictionsTimer = 0f;

    //Turn
    [Header("Turn")]
    [SerializeField, Range(0f, 90f)] private float _turnAngleMin = 1f;
    [SerializeField, Range(0f, 90f)] private float _turnAngleMax = 180f;
    [SerializeField] private float _turnDurationMin = 0.1f;
    [SerializeField] private float _turnDurationMax = 1f;
    [SerializeField] private AnimationCurve _turnCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    private float _turnTimer = 0f;
    private float _turnDuration = 0f;
    private Vector3 _turnVelocityStart = Vector3.zero;
    private bool _isTurning = false;

    //Turn Around
    [Header("Turn Around")]
    [SerializeField] private float _turnAroundDuration = 1f;
    [SerializeField] private AnimationCurve _turnAroundCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    private float _turnAroundTimer = 0f;
    private bool _isTurningAround = false;

   //Orient
   [Header("Orient")]
   public float orientSpeed = 2f;
    private Vector3 _orientDir = Vector3.right;

    //Rotator
    [Header("Rotator")]
    public float period = 0.05f;
    public float offset = 0.5f;
    private float _timer = 0f;
    private float _offsetvalue = 0f;
    [SerializeField] private AnimationCurve _rotatorCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    //Pathfinding
    [SerializeField] private bool usePathFinding = false;

    //Debug
    [Header("Debug")]
    [SerializeField] private bool _guiDebug = true;

    private Rigidbody _rigidbody = null;
    public bool moveModeOn = true;


    #endregion

    #region Functions Unity Callbacks

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _UpdateMove();
        _ApplyVelocity();
        _UpdateModelOrient();
    }

    private void Update()
    {
        _UpdateRotator();
    }

    private void OnGUI()
    {
        if (!_guiDebug) return;

        GUIStyle guiStyle = new GUIStyle();
        guiStyle.normal.textColor = Color.white;
        guiStyle.fontSize = 40;

        GUILayout.Label("MoveDir = " + _moveDir, guiStyle);
        GUILayout.Label("PrevMoveDir = " + _previousMoveDir, guiStyle);
        GUILayout.Label("Velocity = " + _velocity, guiStyle);
        if (_isTurning) {
            GUILayout.Label("TurnTimer = " + _turnTimer, guiStyle);
            GUILayout.Label("TurnDuration = " + _turnDuration, guiStyle);
        }
        if (_isTurningAround) {
            GUILayout.Label("TurnAroundTimer = " + _turnAroundTimer, guiStyle);
        }
        if (_accelerationTimer < _accelerationDuration) {
            GUILayout.Label("Acceleration Timer = " + _accelerationTimer, guiStyle);
        }
        if (_frictionsTimer < _frictionsDuration) {
            GUILayout.Label("Frictions Timer = " + _accelerationTimer, guiStyle);
        }
    }

    #endregion

    #region Functions Move

    public void Move(Vector3 moveDir)
    {
        _moveDir = moveDir;
    }

    public void MoveStop()
    {
        _moveDestination = Vector3.zero;
        _isMovingToDestination = false;
        moveModeOn = false;
    }

    public void MoveToDestination(Vector3 destination)
    {
        _moveDestination = destination;
        _moveDestination.y = 0f;
        _isMovingToDestination = true;
        _moveDestinationRefreshDirCountdown = -1f;
    }

    private void _UpdateMove()
    {
        bool isMoving = _moveDir != Vector3.zero;

        if (_isMovingToDestination)
        {
            bool hasReachedDestination = false;
            float distFromDestination = (_moveDestination - transform.position).magnitude;
            if (distFromDestination <= _moveDestinationRange)
            {
                hasReachedDestination = true;
            }
            Vector3 dirFromDestination = (_moveDestination - transform.position).normalized;
            if (Vector3.Dot(_moveDir, dirFromDestination) < 0f)
            {
                hasReachedDestination = true;
            }

            if (hasReachedDestination)
            {
                isMoving = false;
                Move(Vector3.zero);
                _velocity = Vector3.zero;
                _isMovingToDestination = false;
            }
            else
            {
                _moveDestinationRefreshDirCountdown -= Time.fixedDeltaTime;
                if (_moveDestinationRefreshDirCountdown <= 0f)
                {
                    Vector3 moveDir = (_moveDestination - transform.position).normalized;
                    Move(moveDir);
                    _moveDestinationRefreshDirCountdown = _moveDestinationRefreshDirDuration;
                }
            }
        }

        if (isMoving) {
            if (_velocity != Vector3.zero) {
                if (Vector3.Dot(_previousMoveDir, _moveDir) < 0f) {
                    _StartTurnAround();
                } else {
                    float angle = Vector3.Angle(_previousMoveDir, _moveDir);
                    if (angle > _turnAngleMin) {
                        _StartTurn(angle);
                    }
                }
            } else {
                _StartAcceleration();
            }

            if (_isTurningAround) {
                _velocity = _ApplyTurnAround(_velocity);
            } else {
                Vector3 velocity = _ApplyAcceleration();
                _velocity = _ApplyTurn(velocity);
                _orientDir = _velocity.normalized;
            }

            _previousMoveDir = _moveDir;
        } else {
            if (_wasMoving) {
                _StartFrictions();
            }


            if (_isMovingToDestination)
            {
                float distFromDestination = (_moveDestination - transform.position).magnitude;
                if (distFromDestination <= moveDestinationStartSlowdownRange)
                {
                    float ratio = distFromDestination / moveDestinationStartSlowdownRange;
                    float speed = Mathf.Lerp(0f, _moveDestinationSpeed, ratio);
                    _velocity = _velocity.normalized * speed;
                }
                else
                {
                    _moveDestinationSpeed = _velocity.magnitude;
                    if (_moveDestinationSpeed < moveDestinationSpeedMin)
                    {
                        _moveDestinationSpeed = moveDestinationSpeedMin;
                    }
                }
            }
            _isTurning = false;
            _isTurningAround = false;

            _velocity = _ApplyFrictions(_velocity);
        }

        _wasMoving = isMoving;
    }

    private void _StartAcceleration()
    {
        float currentSpeed = _velocity.magnitude;
        float accelerationTimerRatio = currentSpeed / _speedMax;
        _accelerationTimer = Mathf.Lerp(0f, _accelerationDuration, accelerationTimerRatio);
        _isTurning = false;
    }

    private Vector3 _ApplyAcceleration()
    {
        Vector3 velocity = Vector3.zero;
        _accelerationTimer += Time.deltaTime;
        if (_accelerationTimer < _accelerationDuration) {
            float ratio = _accelerationTimer / _accelerationDuration;
            ratio = _accelerationCurve.Evaluate(ratio);
            float speed = Mathf.Lerp(0f, _speedMax, ratio);
            return _moveDir * speed;
        } else {
            //Speed Max
            return _moveDir * _speedMax;
        }
    }

    private void _StartFrictions()
    {
        float currentSpeed = _velocity.magnitude;
        float frictionTimerRatio = Mathf.InverseLerp(0f, _speedMax, currentSpeed);
        _frictionsTimer = Mathf.Lerp(_frictionsDuration, 0f, frictionTimerRatio);
    }

    private Vector3 _ApplyFrictions(Vector3 velocity)
    {
        _frictionsTimer += Time.deltaTime;
        if (_frictionsTimer < _frictionsDuration) {
            float ratio = _frictionsTimer / _frictionsDuration;
            ratio = _frictonsCurve.Evaluate(ratio);
            float speed = Mathf.Lerp(_speedMax, 0f, ratio);
            velocity = velocity.normalized * speed;
        } else {
            //Reset speed
            velocity = Vector3.zero;
            _previousMoveDir = Vector3.zero;
        }

        return velocity;
    }

    private void _StartTurnAround()
    {
        _isTurningAround = true;
        _turnAroundTimer = 0f;
        _isTurning = false;
    }

    private Vector3 _ApplyTurnAround(Vector3 velocity)
    {
        _turnAroundTimer += Time.deltaTime;
        if (_turnAroundTimer < _turnAroundDuration) {
            float ratio = _turnAroundTimer / _turnAroundDuration;
            ratio = _turnAroundCurve.Evaluate(ratio);
            float speed = Mathf.Lerp(_speedMax, 0f, ratio);
            velocity = _velocity.normalized * speed;
        } else {
            velocity = Vector3.zero;
            _accelerationTimer = 0f;
            _isTurningAround = false;
        }

        return velocity;
    }

    private void _StartTurn(float angle)
    {
        float turnDurationRatio = Mathf.InverseLerp(_turnAngleMin, _turnAngleMax, angle);
        _turnDuration = Mathf.Lerp(_turnDurationMin, _turnDurationMax, turnDurationRatio);
        _turnTimer = 0f;
        _turnVelocityStart = _velocity;
        _isTurning = true;
    }

    private Vector3 _ApplyTurn(Vector3 velocity)
    {
        if (_isTurning) {
            _turnTimer += Time.deltaTime;
            if (_turnTimer < _turnDuration) {
                float turnRatio = _turnTimer / _turnDuration;
                turnRatio = _turnCurve.Evaluate(turnRatio);
                velocity = Vector3.Lerp(_turnVelocityStart, velocity, turnRatio);
            } else {
                _isTurning = false;
            }
        }

        return velocity;
    }

    private void _UpdateRotator()
    {
        if (_isMovingToDestination)
        {
            _timer += Time.deltaTime;

            float t = Mathf.PingPong(_timer, period);
            float r = t / period;

            Vector3 eulerAngles = transform.eulerAngles;

            eulerAngles.z = _offsetvalue;
            _offsetvalue = Mathf.Lerp(-offset, offset, r);
            eulerAngles.z += _offsetvalue;
            eulerAngles.x -= _rotatorCurve.Evaluate(_timer);

            transform.localEulerAngles = eulerAngles;

        }
    }

    #endregion


    #region Functions Orient

    private void _UpdateModelOrient()
    {
        Quaternion rotation = Quaternion.LookRotation(_orientDir.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * orientSpeed);

    }

    #endregion

    #region Functions Velocity

    private void _ApplyVelocity()
    {
        if (null != _rigidbody) {
            _rigidbody.velocity = _velocity;
        } else {
            transform.position += _velocity * Time.fixedDeltaTime;
        }
    }

    #endregion

}
