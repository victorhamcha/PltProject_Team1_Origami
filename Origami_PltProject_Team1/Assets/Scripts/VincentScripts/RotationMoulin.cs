using UnityEngine;

public class RotationMoulin : MonoBehaviour
{

    [SerializeField] private float speedRotation = 2f;
    [SerializeField] private LayerMask layerMoulin = new LayerMask();
    [SerializeField] private AnimationCurve curveFrein = AnimationCurve.Linear(0,1,1,0);
    [SerializeField] private float _timerFrein = 1f;
    private Transform internalTransform = null;
    private Vector3 _touchDeltaPositionMoulin;
    private float _velocity = 1f;
    private float _maxVelocity = 1f;
    private float _tempTimerFrein = 0f;

    private void Awake()
    {
        internalTransform = transform;
        _tempTimerFrein = _timerFrein;
    }

    void Update()
    {
        ClickClickManager.Instance.RaycastClick(layerMoulin);
        if (ClickClickManager.Instance.isTouchTarget && ClickClickManager.Instance.touchPhase == TouchPhase.Moved)
        {
            _touchDeltaPositionMoulin = ClickClickManager.Instance.touchDeltaPosition;
            if (_touchDeltaPositionMoulin.y * speedRotation * Time.deltaTime > 0)
            {
                internalTransform.localRotation *= Quaternion.Euler(_touchDeltaPositionMoulin.y * speedRotation * Time.deltaTime, 0f, 0f);
                _velocity = _touchDeltaPositionMoulin.y * speedRotation * Time.deltaTime;
                _maxVelocity = _velocity;
                _tempTimerFrein = _timerFrein;
            }
        }
        else if (!ClickClickManager.Instance.isTouchTarget || ClickClickManager.Instance.touchPhase == TouchPhase.Ended || ClickClickManager.Instance.touchPhase == TouchPhase.Canceled)
        {
            _tempTimerFrein -= Time.deltaTime;
            if (_tempTimerFrein > 0f)
            {
                _velocity = Mathf.Lerp(_maxVelocity, 1,curveFrein.Evaluate(_tempTimerFrein / _timerFrein ));
            }
            internalTransform.localRotation *= Quaternion.Euler(speedRotation * _velocity * Time.deltaTime, 0f, 0f);
        }
    }
}
