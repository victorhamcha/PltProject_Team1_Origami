using System;
using UnityEngine;

public class SelectPointOrigami : MonoBehaviour
{
    public GameObject cubeTest;

    private Transform _goodPointSelections = null;
    private Transform _pointSelected = null;

    private Vector3 posHitOrigami = Vector3.zero;

    [SerializeField] private int _timeVibration = 50;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, 0);
            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (_pointSelected == null)
                {
                    _pointSelected = hit.collider.transform;
                    if (IsGoodSelections())
                    {
                        Vibration.Vibrate(_timeVibration);
                    }
                }
                posHitOrigami = hit.point;
                cubeTest.SetActive(true);
                cubeTest.transform.position = posHitOrigami;
            }
        }
        else if (_pointSelected != null)
        {
            _pointSelected = null;
            cubeTest.SetActive(false);
        }
       
    }

    public void SetPointGoodSelection(Transform pointSelections)
    {
        _goodPointSelections = pointSelections;
        _pointSelected = null;
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

    public Vector3 GetPosHitOrigami()
    {

        return posHitOrigami;
    }

}
