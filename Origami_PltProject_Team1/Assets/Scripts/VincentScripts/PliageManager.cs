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

    [SerializeField]
    private float speedDownAnim = 50f;

    private float valueStick = 0f;
    private int indexPliage = 0;

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

    }

    private void Update()
    {
        float moveHorizontal = _rewiredPlayer.GetAxis(actionName: "PliagePapier");

        if (_pointSelectedOrigami.PointIsSelected())
        {
             valueStick += moveHorizontal / 2 * Time.deltaTime;

            if (valueStick < 0)
            {
                valueStick = 0;
            }

            _animator.PlayInFixedTime(currentPliage.animToPlay.name, -1, currentPliage.animToPlay.frameRate);
            _animator.speed = valueStick / speedDownAnim;

        }

        if (CurrentAnimIsFinish())
        {
            indexPliage++;
            if (_listePliage.CanGoToNextPliage(indexPliage))
            {
                currentPliage = _listePliage.GetPliage(indexPliage);
                _pointSelectedOrigami.SetPointSelection(currentPliage.pointSelections);
                _pointSelectedOrigami.SetPointGoodSelection(currentPliage.goodPointSelection);
                _animator.speed = 0;
                _animator.Play(currentPliage.animToPlay.name);
                valueStick = 0;
            }
            else
            {
                Debug.Log("Origami Fini");
            }
        }
    }

    public bool CurrentAnimIsFinish()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            return true;
        }
        return false;
    }
}
