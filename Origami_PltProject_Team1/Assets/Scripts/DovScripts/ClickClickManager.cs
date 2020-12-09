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
    [HideInInspector] public bool onMobile = false;
    public Vector3 touchDeltaPosition;
    private Vector3 lastPos = Vector3.zero;
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
            onMobile = true;
            isTouch = true;
            Touch touch = Input.GetTouch(0);
            touchPhase = touch.phase;
            lastPos = touchPos;
            touchPos = new Vector3(touch.position.x, touch.position.y, 0);
            touchDeltaPosition = touch.deltaPosition;
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
        else if (Input.GetMouseButton(0))
        {
            onMobile = false;
            isTouch = true;
            touchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            if (touchPos != lastPos)
            {
                touchDeltaPosition = new Vector3(touchPos.x - lastPos.x, touchPos.y - lastPos.y, touchPos.z - lastPos.z);
            }

            if (Input.GetMouseButtonDown(0))
            {
                touchPhase = TouchPhase.Began;
                touchDeltaPosition = Vector3.zero;
            }
            else
            {
                touchPhase = TouchPhase.Moved;
            }

            lastPos = touchPos;
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

    public void RaycastClickWithObstacle(LayerMask layerHit, LayerMask layerObstacle)
    {
        Vector3 touchPos = Vector3.zero;

        if (Input.touchCount > 0)
        {
            onMobile = true;
            isTouch = true;
            Touch touch = Input.GetTouch(0);
            touchPhase = touch.phase;
            lastPos = touchPos;
            touchPos = new Vector3(touch.position.x, touch.position.y, 0);
            touchDeltaPosition = touch.deltaPosition;
            ray = Camera.main.ScreenPointToRay(touchPos);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerHit + layerObstacle))
            {
                if (hit.transform.gameObject.layer == layerHit)
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
                Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            onMobile = false;
            isTouch = true;
            touchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            if (touchPos != lastPos)
            {
                touchDeltaPosition = new Vector3(touchPos.x - lastPos.x, touchPos.y - lastPos.y, touchPos.z - lastPos.z);
            }

            if (Input.GetMouseButtonDown(0))
            {
                touchPhase = TouchPhase.Began;
                touchDeltaPosition = Vector3.zero;
            }
            else
            {
                touchPhase = TouchPhase.Moved;
            }

            lastPos = touchPos;
            ray = Camera.main.ScreenPointToRay(touchPos);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerHit + layerObstacle))
            {
                if (layerHit == (layerHit | (1 << hit.transform.gameObject.layer)))
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
