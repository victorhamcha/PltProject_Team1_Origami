using UnityEngine;

[RequireComponent(typeof(SelectPointOrigami))]
[RequireComponent(typeof(ListePliage))]
public class PliageManager : MonoBehaviour
{
    private SelectPointOrigami _pointSelectedOrigami = null;
    private ListePliage _listePliage = null;
    private Pliage currentPliage = null;

    private Animator _animator = null;

    [SerializeField]
    private float speedDownAnim = 50f;

    private float valueStick = 0f;
    private int indexPliage = 0;

    private bool _pliageIsFinish = false;

    private void Start()
    {
        _pointSelectedOrigami = GetComponent<SelectPointOrigami>();
        _listePliage = GetComponent<ListePliage>();

        currentPliage = _listePliage.GetPliage(indexPliage);
        _animator = _listePliage.GetAnimator();

        if (currentPliage != null)
        {
            _pointSelectedOrigami.SetPointGoodSelection(currentPliage.goodPointSelection);
        }

        _animator.speed = 0;
        _animator.Play(currentPliage.animToPlay.name);

    }

    private void Update()
    {
        //float moveHorizontal = _rewiredPlayer.GetAxis(actionName: "PliagePapier");
        float moveHorizontal = 1;

        if (_pointSelectedOrigami.IsGoodSelections() && !PliageIsFinish())
        {
             valueStick += moveHorizontal / 2 * Time.deltaTime;

            if (valueStick < 0)
            {
                valueStick = 0;
            }

            _animator.PlayInFixedTime(currentPliage.animToPlay.name, -1, currentPliage.animToPlay.frameRate);
            _animator.speed = valueStick / speedDownAnim;

        }

        if (CurrentAnimIsFinish() && !PliageIsFinish())
        {
            indexPliage++;
            if (_listePliage.CanGoToNextPliage(indexPliage))
            {
                currentPliage = _listePliage.GetPliage(indexPliage);
                _pointSelectedOrigami.SetPointGoodSelection(currentPliage.goodPointSelection);
                _animator.speed = 0;
                _animator.Play(currentPliage.animToPlay.name);
                valueStick = 0;
            }
            else
            {
                Debug.Log("Origami Fini");
                _pliageIsFinish = true;
            }
        }
    }

    public bool PliageIsFinish()
    {
        return _pliageIsFinish;
    }

    public void ResetPliage()
    {
        indexPliage = 0;
        currentPliage = _listePliage.GetPliage(indexPliage);
        _pointSelectedOrigami.SetPointGoodSelection(currentPliage.goodPointSelection);
        _animator.speed = 0;
        _animator.Play(currentPliage.animToPlay.name);
        valueStick = 0;
        _pliageIsFinish = false;
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
