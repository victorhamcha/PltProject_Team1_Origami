using Rewired;
using System.Collections;
using UnityEngine;

public class SelectPointOrigami : MonoBehaviour
{

    [SerializeField]
    private Transform[] _pointSelections = null;
    private Transform _goodPointSelections = null;

    private GameObject _pointSelected = null;

    private bool _pointIsSelected = false;
    private int _indiceVertices = 0;

    private bool _canChangePos = true;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, 0);
            Ray ray = Camera.main.ScreenPointToRay(touchPos);

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
                Debug.Log("Did Hit");
                _pointSelected = hit.collider.transform.gameObject;
                Debug.Log(_pointSelected.name);
            }
            else
            {
                Debug.Log("Did not Hit");
            }

        }
        /*
        if (trySelection)
        {
            if (IsGoodSelections())
            {
                _canChangePos = false;
                _cursorSprite.SetActive(false);
                _pointIsSelected = true;
            }
            else
            {
                _spriteRenderer.color = Color.red;
            }
        }
        else if (_spriteRenderer.color == Color.red) {
            _spriteRenderer.color = Color.green;
        }

        if (_timerChoise <= 0f && _canChangePos)
        {
            if (moveHorizontal == 1)
            {
                _timerChoise = timerSwitchChoise;
                if (_indiceVertices == _pointSelections.Length - 1)
                {
                    _indiceVertices = 0;
                }
                else
                {
                    _indiceVertices++;
                }
                _cursorSprite.transform.position = _pointSelections[_indiceVertices].position;
            }
            else if (moveHorizontal == -1)
            {
                _timerChoise = 0.2f;
                if (_indiceVertices == 0)
                {
                    _indiceVertices = _pointSelections.Length - 1;
                }
                else
                {
                    _indiceVertices--;
                }
                _cursorSprite.transform.position = _pointSelections[_indiceVertices].position;
            }
        }
        
        if (moveHorizontal == 0)
        {
            _timerChoise = 0;
        }*/
    }

    public void SetPointSelection(Transform[] pointSelections)
    {
        _pointSelections = pointSelections;
        _indiceVertices = 0;
        //_cursorSprite.transform.position = _pointSelections[_indiceVertices].position;
        _canChangePos = true;
        //_cursorSprite.SetActive(true);
        _pointIsSelected = false;
    }

    public void SetPointGoodSelection(Transform pointSelections)
    {
        _goodPointSelections = pointSelections;
    }

    public bool IsGoodSelections()
    {
        return _pointSelections[_indiceVertices] == _goodPointSelections;
    }

    public bool PointIsSelected()
    {
        return _pointIsSelected;
    }

}
