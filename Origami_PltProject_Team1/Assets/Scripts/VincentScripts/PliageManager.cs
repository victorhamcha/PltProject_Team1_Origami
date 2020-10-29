using UnityEngine;

[RequireComponent(typeof(SelectPointOrigami))]
[RequireComponent(typeof(ListePliage))]
public class PliageManager : MonoBehaviour
{
    private SelectPointOrigami _pointSelectedOrigami = null;
    private ListePliage _listePliage = null;
    private Pliage currentPliage = null;

    private Animator _animator = null;

    [SerializeField] private int _timeVibrationEndPliage = 50;
    [SerializeField] private GameObject _cursorSelectPoint = null;

    private int indexPliage = 0;

    private bool _pliageIsFinish = false;
    private bool _reverseAnim = false;

    void Start()
    {
        _pointSelectedOrigami = GetComponent<SelectPointOrigami>();
        _listePliage = GetComponent<ListePliage>();

        currentPliage = _listePliage.GetPliage(indexPliage);
        _animator = _listePliage.GetAnimator();

        if (currentPliage != null)
        {
            _pointSelectedOrigami.SetPointGoodSelection(currentPliage.goodPointSelection);
            _cursorSelectPoint.transform.position = currentPliage.goodPointSelection.position;
            _cursorSelectPoint.transform.rotation = currentPliage.goodPointSelection.rotation;
        }

        _animator.speed = 0;
        _animator.Play(currentPliage.animToPlay.name);

    }

    void Update()
    {
        float prctAvancementSlide = GetPourcentageAvancementSlide();
        //Debug.DrawLine(currentPliage.goodPointSelection.position, GetPosAvancementSlideByPrct(prctAvancementSlide),Color.red);
        Debug.Log(prctAvancementSlide);
        Debug.Log(_reverseAnim);

        if (prctAvancementSlide > 95f)
        {
            Vibration.Vibrate(_timeVibrationEndPliage);
        }

        if (_pointSelectedOrigami.GetTouchPhase() == TouchPhase.Ended)
        {
            _animator.Play(currentPliage.animToPlay.name + "_reverse", -1, prctAvancementSlide / 100);
            _reverseAnim = true;
            _cursorSelectPoint.SetActive(true);
        } else if (_pointSelectedOrigami.IsGoodSelections() && !PliageIsFinish())
        {
            _animator.Play(currentPliage.animToPlay.name, -1, prctAvancementSlide / 100);
            _animator.speed = 1;
            _cursorSelectPoint.SetActive(false);
        }

        if (CurrentAnimIsFinish() && !PliageIsFinish())
        {
            indexPliage++;
            if (_listePliage.CanGoToNextPliage(indexPliage))
            {
                currentPliage = _listePliage.GetPliage(indexPliage);
                _pointSelectedOrigami.SetPointGoodSelection(currentPliage.goodPointSelection);
                _cursorSelectPoint.transform.position = currentPliage.goodPointSelection.position;
                _cursorSelectPoint.transform.rotation = currentPliage.goodPointSelection.rotation;
                _animator.speed = 0;
                _animator.Play(currentPliage.animToPlay.name);
            }
            else
            {
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
        _cursorSelectPoint.transform.position = currentPliage.goodPointSelection.position;
        _cursorSelectPoint.transform.rotation = currentPliage.goodPointSelection.rotation;
        _animator.speed = 0;
        _animator.Play(currentPliage.animToPlay.name);
        _pliageIsFinish = false;
    }

    public bool CurrentAnimIsFinish()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !_reverseAnim)
        {
            return true;
        }
        return false;
    }

    public float GetPourcentageAvancementSlide()
    {
        Vector3 posHitOrigami = _pointSelectedOrigami.GetPosHitOrigami();

        float prctAvancementX = Mathf.InverseLerp(currentPliage.goodPointSelection.position.x, currentPliage.endPointSelection.position.x, posHitOrigami.x);
        float prctAvancementY = Mathf.InverseLerp(currentPliage.goodPointSelection.position.y, currentPliage.endPointSelection.position.y, posHitOrigami.y);
        float prctAvancementZ = Mathf.InverseLerp(currentPliage.goodPointSelection.position.z, currentPliage.endPointSelection.position.z, posHitOrigami.z);
        return (prctAvancementX + prctAvancementY + prctAvancementZ) / 3 * 100f;
    }

    public Vector3 GetPosAvancementSlideByPrct(float prctAvancement)
    {
        return Vector3.Lerp(currentPliage.goodPointSelection.position, currentPliage.endPointSelection.position, prctAvancement / 100f);
    }
}
