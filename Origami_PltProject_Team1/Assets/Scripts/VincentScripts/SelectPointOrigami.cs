using Rewired;
using System.Collections;
using UnityEngine;

public class SelectPointOrigami : MonoBehaviour
{

    [SerializeField]
    private GameObject _cursorSprite = null;
    [SerializeField]
    private Transform[] _pointSelections = null;
    private Transform _goodPointSelections = null;
    private Player _rewiredPlayer = null;
    private SpriteRenderer _spriteRenderer = null;

    private bool _pointIsSelected = false;
    private int _indiceVertices = 0;

    [SerializeField]
    private float timerSwitchChoise = 0.2f;

    private float _timerChoise = 0f;

    private bool _canChangePos = true;

    void Start()
    {
        _timerChoise = timerSwitchChoise;

        _rewiredPlayer = ReInput.players.GetPlayer("Player0");
        if (_pointSelections.Length > 0)
        {
            _cursorSprite.transform.position = _pointSelections[_indiceVertices].position;
        }
        _spriteRenderer = _cursorSprite.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float moveHorizontal = _rewiredPlayer.GetAxis("SelectPoint");
        bool trySelection = _rewiredPlayer.GetButton("Validation");



        _timerChoise -= Time.deltaTime;

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
        }
    }

    public void SetPointSelection(Transform[] pointSelections)
    {
        _pointSelections = pointSelections;
        _indiceVertices = 0;
        _cursorSprite.transform.position = _pointSelections[_indiceVertices].position;
        _canChangePos = true;
        _cursorSprite.SetActive(true);
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
