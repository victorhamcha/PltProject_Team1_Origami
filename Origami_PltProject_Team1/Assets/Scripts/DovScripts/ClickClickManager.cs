using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickClickManager : MonoBehaviour
{
    [HideInInspector] public RaycastHit hit;
    [HideInInspector] public Ray ray;
    [HideInInspector] public bool isTouch;
    [HideInInspector] public bool isTouchTarget;
    [HideInInspector] public TouchPhase touchPhase;
    private static ClickClickManager _instance;

    public static ClickClickManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ClickClickManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("ClickClickManager", typeof(ClickClickManager)).GetComponent<ClickClickManager>();
                }
            }

            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }

    public void RaycastClick(LayerMask layerMask)
    {
        Vector3 touchPos = Vector3.zero;

        if (Input.touchCount > 0)
        {
            isTouch = true;
            Touch touch = Input.GetTouch(0);
            touchPhase = touch.phase;

            touchPos = new Vector3(touch.position.x, touch.position.y, 0);
            ray = Camera.main.ScreenPointToRay(touchPos);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerMask))
            {
                isTouchTarget = true;
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
            }
            else
            {
                isTouchTarget = false;
                Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            }
        }
        else
        {
            isTouchTarget = false;
            isTouch = false;
            touchPhase = TouchPhase.Canceled;
        }
    }
}
