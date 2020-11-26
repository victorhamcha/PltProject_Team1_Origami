using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
	private Touch _touch;

	private Quaternion _rotationY;
	private Quaternion _rotationX;

	[SerializeField] private float _multiplier = 0.3f;
	[SerializeField] private float _autoRotationSpeed = 0.1f;
	[SerializeField] private float _startRotationTimer = 0.1f;
	 private float _timerAutoRotation = 0.0f;
	[SerializeField] private float _smoothRotationDuration = 0.0f;
	[SerializeField] private AnimationCurve curveRotation;
	 private float _timerBackToY = 0.0f;

	private bool _hasReset = false;

	private Quaternion _lastPos = Quaternion.identity;
	private Quaternion _currentpos = Quaternion.identity;

	void Update()
	{
		if(Input.touchCount > 0)
        {
			_touch = Input.GetTouch(0);
			_timerBackToY = 0.0f;
			_timerAutoRotation = _startRotationTimer;

			if(_touch.phase == TouchPhase.Began && _hasReset)
            {
				_lastPos = transform.rotation;
            }

			if (_touch.phase == TouchPhase.Moved)
			{
				_hasReset = false;
				_rotationY = Quaternion.Euler(_touch.deltaPosition.y * _multiplier, -_touch.deltaPosition.x * _multiplier, 0f);
				
				transform.localRotation = _rotationY * transform.localRotation;
			}

			if(_touch.phase == TouchPhase.Ended)
            {
				_currentpos = transform.rotation;
            }

        }
        else
        {
			_timerAutoRotation -= Time.deltaTime;

			if(_timerAutoRotation < 0f)
            {
				if(_timerBackToY <= _smoothRotationDuration)
                {
					transform.rotation = Quaternion.Lerp(_currentpos,_lastPos, curveRotation.Evaluate((_timerBackToY / _smoothRotationDuration)));
					_timerBackToY += Time.deltaTime;
					Debug.Log(curveRotation.Evaluate(_timerBackToY / _smoothRotationDuration));
					
                }
				else
                {
					_hasReset = true;
                }


				_rotationY = Quaternion.Euler(0f, _autoRotationSpeed, 0f);
				transform.localRotation = _rotationY * transform.localRotation;
			}
        }
	}
}