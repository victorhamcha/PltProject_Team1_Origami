using UnityEngine;

public class SelectPointOrigami : MonoBehaviour
{
    private Transform _goodPointSelections = null;
    private Transform _pointSelected = null;

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
                _pointSelected = hit.collider.transform;
            }
            else
            {
                _pointSelected = null;
            }
        }
        else if (_pointSelected != null)
        {
            _pointSelected = null;
        }
       
    }

    public void SetPointGoodSelection(Transform pointSelections)
    {
        _goodPointSelections = pointSelections;
        _pointSelected = null;
    }

    public bool IsGoodSelections()
    {
        return _pointSelected == _goodPointSelections;
    }

}
