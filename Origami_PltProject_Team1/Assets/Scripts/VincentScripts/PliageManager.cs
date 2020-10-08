using Rewired;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SelectPointOrigami))]
[RequireComponent(typeof(ListePliage))]
public class PliageManager : MonoBehaviour
{
    private SelectPointOrigami _pointSelectedOrigami = null;
    private ListePliage _listePliage = null;
    private Pliage currentPliage = null;

    private Animator _animator = null;

    private Player _rewiredPlayer = null;

    private float valueStick = 0f;
    private float valueDivision = 50f;
    private float _lastValueStick;
    private int indexPliage = 0;
    private float maxValueStick = 10f;

    private void Start()
    {
        _pointSelectedOrigami = GetComponent<SelectPointOrigami>();
        _listePliage = GetComponent<ListePliage>();

        currentPliage = _listePliage.GetPliage(indexPliage);
        _animator = _listePliage.GetAnimator();

        _rewiredPlayer = ReInput.players.GetPlayer("Player0");

        if (currentPliage != null)
        {
            _pointSelectedOrigami.SetPointSelection(currentPliage.pointSelections);
            _pointSelectedOrigami.SetPointGoodSelection(currentPliage.goodPointSelection);
        }

        _animator.speed = 0;
        _animator.Play(currentPliage.animToPlay.name);
        _lastValueStick = 0;

    }

    private void Update()
    {
        float moveHorizontal = _rewiredPlayer.GetAxis(actionName: "PliagePapier");

        if (valueStick < maxValueStick)
        {
            valueStick += moveHorizontal / 2 * Time.deltaTime;
        }
        else
        {
            valueStick = maxValueStick;
        }

        if (valueStick <= 0 && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime == 0)
        {
            valueStick = 0;
            _pointSelectedOrigami.GetCursorSprite().SetActive(true);
            _pointSelectedOrigami.SetBoolChangePos(true);
        }else if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            maxValueStick = valueStick;
        }
        else if (_pointSelectedOrigami.IsGoodSelections())
        {
            _pointSelectedOrigami.GetCursorSprite().SetActive(false);
            _pointSelectedOrigami.SetBoolChangePos(false);
        }

        if (_pointSelectedOrigami.IsGoodSelections())
        {
            _animator.PlayInFixedTime(currentPliage.animToPlay.name, -1 , currentPliage.animToPlay.frameRate);
            _lastValueStick = valueStick;
            _animator.speed = valueStick / valueDivision;

        }
        else if (valueStick > 0)
        {
            StartCoroutine("SwitchColor");
            valueStick = _lastValueStick;
            _animator.speed = 0;
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            indexPliage++;
            if (_listePliage.CanGoToNextPliage(indexPliage))
            {
                currentPliage = _listePliage.GetPliage(indexPliage);
                _pointSelectedOrigami.SetPointSelection(currentPliage.pointSelections);
                _pointSelectedOrigami.SetPointGoodSelection(currentPliage.goodPointSelection);
                _animator.speed = 0;
                _animator.Play(currentPliage.animToPlay.name);
                _lastValueStick = 0;
                valueStick = 0;
            }
            else
            {
                Debug.Log("Origami Fini");
            }
        }

    }

    IEnumerator SwitchColor()
    {
        SpriteRenderer _spriteRenderer = _pointSelectedOrigami.GetCursorSprite().GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1f);
        _spriteRenderer.color = Color.green;
    }

}
