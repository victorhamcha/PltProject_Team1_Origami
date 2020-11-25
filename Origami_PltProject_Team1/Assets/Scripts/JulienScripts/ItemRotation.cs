using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
	private Touch touch;

	private Vector2 touchPosition;

	private Quaternion rotationY;
	private Quaternion rotationX;

	private float rotationSpeed = 0.3f;

	void Update()
	{
		if(Input.touchCount > 0)
        {
			touch = Input.GetTouch(0);

			if(touch.phase == TouchPhase.Moved)
            {
				rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotationSpeed, 0f);
				rotationX = Quaternion.Euler(touch.deltaPosition.y * rotationSpeed, 0f, 0f);

				transform.localRotation = rotationY * transform.localRotation;
				transform.localRotation = rotationX * transform.localRotation;
            }
        }
	}
}