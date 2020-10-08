using Rewired;
using UnityEngine;

public class SelectPointOrigami : MonoBehaviour
{

    [SerializeField]
    private GameObject _cursorSprite = null;
    [SerializeField]
    private Transform[] _pointSelections = null;
    private Transform _goodPointSelections = null;
    private Player _rewiredPlayer = null;

    private bool _pointIsSelected = true;
    private int _indiceVertices = 0;
    private float _timerSwitchChoise = 0.2f;

    private bool _canChangePos = true;

    void Start()
    {
        _rewiredPlayer = ReInput.players.GetPlayer("Player0");
        if (_pointSelections.Length > 0)
        {
            _cursorSprite.transform.position = _pointSelections[_indiceVertices].position;
        }
    }

    private void Update()
    {
        float moveHorizontal = _rewiredPlayer.GetAxis("SelectPoint");
        //bool trySelection = _rewiredPlayer.GetButton("SelectPoint");

        _timerSwitchChoise -= Time.deltaTime;

        if (moveHorizontal == 1 && _timerSwitchChoise <= 0f && _canChangePos)
        {
            //Debug.Log(ReInput.mapping.GetAction("SelectPoint").positiveDescriptiveName);
            _timerSwitchChoise = 0.2f;
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
        else if (moveHorizontal == -1 && _timerSwitchChoise <= 0f && _canChangePos)
        {
            //Debug.Log(ReInput.mapping.GetAction("SelectPoint").negativeDescriptiveName);
            _timerSwitchChoise = 0.2f;
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
        else if (moveHorizontal == 0)
        {
            _timerSwitchChoise = 0;
        }
    }

    public int GetIndiceVertices()
    {
        return _indiceVertices;
    }

    public GameObject GetCursorSprite()
    {
        return _cursorSprite;
    }

    public void SetBoolChangePos(bool canChangePos)
    {
        _canChangePos = canChangePos;
    }

    public void SetPointSelection(Transform[] pointSelections)
    {
        _pointSelections = pointSelections;
        _indiceVertices = 0;
        _cursorSprite.transform.position = _pointSelections[_indiceVertices].position;
    }

    public void SetPointGoodSelection(Transform pointSelections)
    {
        _goodPointSelections = pointSelections;
    }

    public bool IsGoodSelections()
    {
        return _pointSelections[_indiceVertices] == _goodPointSelections && PointIsSelected();
    }

    public bool PointIsSelected()
    {
        return _pointIsSelected;
    }

}
