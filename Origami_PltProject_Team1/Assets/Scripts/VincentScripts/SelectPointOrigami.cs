using System;
using UnityEngine;

public class SelectPointOrigami : MonoBehaviour
{
    public GameObject cubeTest;

    private Transform _goodPointSelections = null;
    private Transform _pointSelected = null;

    private Vector3 posHitOrigami = Vector3.zero;
    private TouchPhase _touchPhase;

    [SerializeField] private int _timeVibration = 50;
    private bool _startWithGoodSelection = false;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            _touchPhase = touch.phase;

            Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, 0);
            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (_pointSelected == null)
                {
                    _pointSelected = hit.collider.transform;
                }
                posHitOrigami = hit.point;
                cubeTest.SetActive(true);
                cubeTest.transform.position = posHitOrigami;
            }

            //Debut du touch
            if (_touchPhase == TouchPhase.Began && IsGoodSelections())
            {
                _startWithGoodSelection = true;
                Vibration.Vibrate(_timeVibration);
            }else if (_touchPhase == TouchPhase.Began)
            {
                _startWithGoodSelection = false;
            }

            //Fin du touch
            if (_touchPhase == TouchPhase.Ended)
            {
                //posHitOrigami = _goodPointSelections.position;
                _pointSelected = null;
                cubeTest.SetActive(false);
            }
        }
        else
        {
            _touchPhase = TouchPhase.Canceled;
        }
       
    }

    public void SetPointGoodSelection(Transform pointSelections)
    {
        _goodPointSelections = pointSelections;
        _pointSelected = null;
        _startWithGoodSelection = false;
        posHitOrigami = _goodPointSelections.position;
    }

    public bool IsGoodSelections()
    {
        return _pointSelected == _goodPointSelections;
    }

    public Transform GetPointSelected()
    {
        return _pointSelected;
    }

    public void SetPointSelected(Transform point_selected)
    {
        _pointSelected = point_selected;
    }

    public Vector3 GetPosHitOrigami()
    {
        return posHitOrigami;
    }

    public TouchPhase GetTouchPhase()
    {
        return _touchPhase;
    }

    public bool AsStartesGoodSelection()
    {
        return _startWithGoodSelection;
    }

}
