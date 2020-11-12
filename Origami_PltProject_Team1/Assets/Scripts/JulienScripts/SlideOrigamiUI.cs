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

    [SerializeField] private List<Transform> _spriteList = new List<Transform>();

    private int listIndex = 2;
    private float _timer = 0.0f;

    public bool goNextIndex = false;
    private bool playedOnce = false;
    private Vector3 directionLerp = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _spriteList[2].position = _slotMiddle.position;
        _spriteList[3].position = _slotRight.position;

    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if((listIndex + 2) == (_spriteList.Count - 1))
        {
            goNextIndex = false;
            playedOnce = false;
        }

        if (goNextIndex)
        {
            //StartCoroutine("IndexIncrementation");
            if(!playedOnce)
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

        if(playedOnce)
        {
            // CreateRight to Right
            SetPosition(_spriteList[listIndex + 2], _spriteList[listIndex + 1], _slotCreateRight, _slotRight);

            // Right to middle
            SetPosition(_spriteList[listIndex + 1], _spriteList[listIndex], _slotRight, _slotMiddle);


            // Middle to left
            SetPosition(_spriteList[listIndex], _spriteList[listIndex - 1], _slotMiddle, _slotLeft);

            // Left to DeleteLeft
            SetPosition(_spriteList[listIndex - 1], _spriteList[listIndex - 2], _slotLeft, _slotDeleteLeft);
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
        startSprite.position = startPos.position;
        endSprite.position = endPos.position;
        MoveSprite(startSprite, endSprite);
        startSprite.position = directionLerp;
    }
}
