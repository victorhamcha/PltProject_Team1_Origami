using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideOrigamiUI : MonoBehaviour
{
    [SerializeField] private Transform _slotDeleteLeft = null;
    [SerializeField] private Transform _slotLeft = null;
    [SerializeField] private Transform _slotRight = null;
    [SerializeField] private Transform _slotCreateRight = null;
    [SerializeField] private Transform _slotMiddle = null;

    //private SpriteRenderer _spriteLeft = null;
    //private SpriteRenderer _spriteRight = null;
    //private SpriteRenderer _spriteMiddle = null;

    [SerializeField] private List<Transform> _transformList = new List<Transform>();

    private int listIndex = 2;
    private float _timer = 0.0f;

    public bool goNextIndex = false;
    private bool playedOnce = false;
    private Vector3 directionLerp = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _transformList[2].position = _slotMiddle.position;
        _transformList[3].position = _slotRight.position;
        _transformList[2].localScale = Vector3.one * 2;

        foreach (Transform trans in _transformList)
        {
            if (trans != _transformList[2] && trans != _transformList[3])
            {
                trans.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if ((listIndex + 2) == (_transformList.Count - 1))
        {
            goNextIndex = false;
            playedOnce = false;
        }

        if (goNextIndex)
        {
            //StartCoroutine("IndexIncrementation");
            if (!playedOnce)
            {
                playedOnce = true;
            }
            else
            {
                listIndex++;
            }
            _timer = 0.0f;
            goNextIndex = false;
        }

        if (playedOnce)
        {
            // CreateRight to Right
            SetPosition(_transformList[listIndex + 2], _transformList[listIndex + 1], _slotCreateRight, _slotRight);

            // Right to middle
            SetPositionRightToMiddle();

            // Middle to left
            SetPositionMiddleToLeft();

            // Left to DeleteLeft
            SetPosition(_transformList[listIndex - 1], _transformList[listIndex - 2], _slotLeft, _slotDeleteLeft);
        }
    }

    private void MoveSprite(Transform startSprite, Transform endSprite)
    {
        directionLerp.x = Mathf.Lerp(startSprite.position.x, endSprite.position.x, _timer * 2);
        directionLerp.y = Mathf.Lerp(startSprite.position.y, endSprite.position.y, _timer * 2);
        directionLerp.z = Mathf.Lerp(startSprite.position.z, endSprite.position.z, _timer * 2);
    }

    private void SetPosition(Transform startSprite, Transform endSprite, Transform startPos, Transform endPos)
    {
        startSprite.gameObject.SetActive(true);
        endSprite.gameObject.SetActive(true);
        startSprite.position = startPos.position;
        endSprite.position = endPos.position;
        MoveSprite(startSprite, endSprite);
        startSprite.position = directionLerp;
    }
    private void SetPositionRightToMiddle()
    {
        SetPosition(_transformList[listIndex + 1], _transformList[listIndex], _slotRight, _slotMiddle);
        _transformList[listIndex + 1].localScale = Vector3.Lerp(Vector3.one, Vector3.one * 2, _timer * 2);
    }

    private void SetPositionMiddleToLeft()
    {
        SetPosition(_transformList[listIndex], _transformList[listIndex - 1], _slotMiddle, _slotLeft);
        _transformList[listIndex].localScale = Vector3.Lerp(Vector3.one * 2, Vector3.one, _timer * 2);
    }
}