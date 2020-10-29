using UnityEngine;

[RequireComponent(typeof(SelectPointOrigami))]
[RequireComponent(typeof(ListePliage))]
public class PliageManager : MonoBehaviour
{
    private SelectPointOrigami _pointSelectedOrigami = null;
    private ListePliage _listePliage = null;
    private Pliage currentPliage = null;

    private Animator _animator = null;

    [SerializeField] private float speedDownAnim = 50f;
    [SerializeField] private int _timeVibrationEndPliage = 50;
    [SerializeField] private GameObject _cursorSelectPoint = null;

    //private SkinnedMeshRenderer _meshRendere = null;

    private float valueStick = 0f;
    private int indexPliage = 0;

    private bool _pliageIsFinish = false;

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
        Debug.DrawLine(currentPliage.goodPointSelection.position, GetPosAvancementSlideByPrct(prctAvancementSlide),Color.red);

        Debug.Log(prctAvancementSlide);

        if (prctAvancementSlide > 95f)
        {
            Vibration.Vibrate(_timeVibrationEndPliage);
        }

        float moveHorizontal = 1;

        if (_pointSelectedOrigami.IsGoodSelections() && !PliageIsFinish())
        {

            _cursorSelectPoint.SetActive(false);

             valueStick += moveHorizontal / 2 * Time.deltaTime;

            if (valueStick < 0)
            {
                valueStick = 0;
            }

            _animator.Play(currentPliage.animToPlay.name, -1, GetPourcentageAvancementSlide() / 100);
            _animator.speed = valueStick / speedDownAnim;
        }
        else
        {
            _cursorSelectPoint.SetActive(true);
            _animator.speed = 0;
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
                valueStick = 0;
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
