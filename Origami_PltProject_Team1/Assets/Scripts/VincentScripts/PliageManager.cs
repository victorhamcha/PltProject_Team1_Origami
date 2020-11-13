using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SelectPointOrigami))]
[RequireComponent(typeof(ListePliage))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SlideOrigamiUI))]
public class PliageManager : MonoBehaviour
{
    //Lien des scripts
    private SelectPointOrigami _pointSelectedOrigami = null;
    private ListePliage _listePliage = null;
    private SlideOrigamiUI _slideOrigamiUI = null;
    [SerializeField] private SwitchModePlayerOrigami _switchModePlayerOrigami = null;

    //Informations du pliage en cours d'executions
    private Pliage _currentPliage = null;
    [SerializeField]private Transform pliageObject = null;

    //Animator qui va jouez les animations de pliage
    private Animator _animator = null;

    private Vector3 _lastPosOrigami = Vector3.zero;
    private float _timerTransitionCentrageOrigami = 0f;

    [Header("Custom OrigamiManager")]
    //Timer de la durée de la vibrations une fois qu'un pliage est fini
    [SerializeField] private int _timeVibrationEndPliage = 50;
    //Gameobject utilisez pour montrez qu'elle partie doit ètre selectionnez pour le debut du pliage en cours
    [SerializeField] private GameObject _cursorSelectPoint = null;
    [SerializeField] private GameObject _cursorEndPointSelect = null;
    //Vitesse de l'animations du pliage lorsque l'on relache le pliage
    [SerializeField] [Range(0, 1)] private float _speedReverseAnim = 1f;
    //Vitesse de l'animations du recentrage de l'origami
    [SerializeField] [Range(0f, 5f)] private float _speedAnimCenterOrigami = 0.1f;
    [SerializeField] private float _timerBounce = 1.0f;
    [SerializeField] private float _timerParticule = 1.0f;

    //Indicateur du nombre de pliage effectuez pour parcourir la liste de tout les pliages à faires
    private int indexPliage = 0;

    //State origami
    private bool _origamiIsFinish = false;
    private bool _reverseAnim = false;
    private bool _currentFoldIsFinish = false;
    private bool _bounceIsFinish = false;
    private bool _particulePlayed = false;
    private float _tempTimerBounce = 0.0f;
    private float _tempTimerParticule = 0.0f;

    [Header("TUTO")]
    [SerializeField] private Animator _handAnimator = null;
    [SerializeField] private GameObject _handGO = null;

    [Header("Boundary Manager")]
    [SerializeField] private Animator _boundaryAnimator = null;
    [SerializeField] private Transform _maskSprite = null;
    private float _initScaleXMask = 0;

    [Header("Feedback Origami")]
    [SerializeField] private Animator _animatorOrigami = null;
    [SerializeField] private AnimationClip _animFeedBack = null;

    [Header("Séquenceur")]
    [SerializeField] private float _fixingTimer = 1.0f;
    private float _timerEndAutoComplete = 0.0f;

    //Rotator
    private bool _isRotating = false;
    private bool _rotatingIsFinish = false;
    private float _timerRotation = 1.0f;
    private Vector3 _lastRotation = new Vector3(0, 0, 0);
    private float _lastOffset = 0.0f;

    void Awake()
    {
        _lastRotation = pliageObject.localEulerAngles;
        _pointSelectedOrigami = GetComponent<SelectPointOrigami>();
        _listePliage = GetComponent<ListePliage>();
        _slideOrigamiUI = GetComponent<SlideOrigamiUI>();
        _animator = GetComponent<Animator>();

        //Set de la speed de l'animator à 0 pour évitez que l'animations se joue dés le debuts
        _animator.speed = 0;
        _initScaleXMask = _maskSprite.localScale.x;
    }

    void Update()
    {
        //TODO UPDATE COM
        //Si le pliage en cours est fini et que l'origami n'est pas fini
        //      Alors on fait vibrez le télephone
        //            on passe au pliage suivant
        //      Si il y a bien un prochain pliage
        //          Alors on réinitialise les variables et on charge les nouvelles informations du prochain pliage
        //      Sinon on dit que l'origami est fini
        //
        if (CurrentFoldsIsFinish() && !OrigamiIsFinish())
        {
            _currentPliage.boundarySprite.color = _currentPliage.colorValidationPliage;
            _animator.speed = _currentPliage.speedAnimAutoComplete;
            _currentFoldIsFinish = true;
            _cursorEndPointSelect.SetActive(false);
            if (!_currentPliage.playedParticleOnce && !_currentPliage.isConfirmationPliage)
            {
                StartCoroutine("BoundariesFeedback");
                _currentPliage.playedParticleOnce = true;
                SoundManager.i.PlaySound(SoundManager.Sound.FoldsSucced);
                _particulePlayed = true;
                _tempTimerParticule = _timerParticule;
            }

            if (_currentPliage.playBounce)
            {
                _currentPliage.playBounce = false;
                _animatorOrigami.Play(_animFeedBack.name, -1, 0);
                SoundManager.i.PlaySound(SoundManager.Sound.FoldsSucced);
                _tempTimerBounce = _timerBounce;
            }
            else
            {
                _tempTimerBounce = 0;
            }
        }

        if (_currentFoldIsFinish && !OrigamiIsFinish())
        {
            _timerEndAutoComplete -= Time.deltaTime;
            _maskSprite.localScale = new Vector3(Mathf.Lerp(_initScaleXMask, _currentPliage.maxSizeSpriteMask, _animator.GetCurrentAnimatorStateInfo(0).normalizedTime), _maskSprite.localScale.y, _maskSprite.localScale.z);
            _currentPliage.boundarySprite.color = Color.Lerp(_currentPliage.colorBoundary, _currentPliage.colorValidationPliage, _animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }

        if (_currentFoldIsFinish && CurrentAnimIsFinish() && !OrigamiIsFinish() && _timerEndAutoComplete <= 0.0f && (_rotatingIsFinish || !_currentPliage.makeRotation ) && _tempTimerParticule <= 0)
        {
            _timerEndAutoComplete = _fixingTimer;
            _rotatingIsFinish = false;
            Vibration.Vibrate(_timeVibrationEndPliage);
            indexPliage++;
            if (_listePliage.CanGoToFolding(indexPliage))
            {
                SetUpCurrentPliage();
            }
            else
            {
                _boundaryAnimator.Play("BoundaryNone");
                _origamiIsFinish = true;
            }

            if (_currentPliage.isConfirmationPliage && _slideOrigamiUI != null)
            {
                _slideOrigamiUI.goNextIndex = true;
            }
        }

        //Récupération du pourcentage d'avancement entre le point de début et le point de fin du pliage actuel en cours en fontion du point ou l'on click
        float prctAvancementSlide = GetPourcentageAvancementSlide();

        if (_reverseAnim && !OrigamiIsFinish() && !_currentFoldIsFinish)
        {
            _maskSprite.localScale = new Vector3(Mathf.Lerp(_currentPliage.maxSizeSpriteMask, _initScaleXMask, _animator.GetCurrentAnimatorStateInfo(0).normalizedTime), _maskSprite.localScale.y, _maskSprite.localScale.z);
            _currentPliage.boundarySprite.color = Color.Lerp(_currentPliage.colorValidationPliage, _currentPliage.colorBoundary, _animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }

        //Si on arrète de touché l'écran ET qu'on avais bien sélectionnez le bont point de l'origamie ET que l'origamie n'est pas fini
        //      Alors on joue l'animation du pliage en cours à l'envers en calculant son bon point de départs pour que les deux animations soit sans discontinuité
        //Sinon Si on à le bont point de sélection ET que l'origamie n'est pas fini
        //      Alors on joue l'animations du pliage en cours en fonction du pourcentage d'avancement de notre doigts sur l'écran
        //
        if (!_currentFoldIsFinish && _pointSelectedOrigami.GetTouchPhase() == TouchPhase.Ended && _pointSelectedOrigami.AsStartesGoodSelection() && !OrigamiIsFinish())
        {
            _animator.Play(_currentPliage.animToPlay.name + "_reverse", -1, 1 - prctAvancementSlide);
            _animator.speed = _speedReverseAnim;
            _reverseAnim = true;
            SetActiveCursor(true);
        }
        else if (!_currentFoldIsFinish && _pointSelectedOrigami.IsGoodSelections() && !OrigamiIsFinish())
        {
            // Disable hand's gameobject
            _handGO.SetActive(false);
            _animator.Play(_currentPliage.animToPlay.name, -1, prctAvancementSlide);
            _animator.speed = _currentPliage.speedAnimAutoComplete;
            _reverseAnim = false;
            SetActiveCursor(false);
            _maskSprite.localScale = new Vector3(Mathf.Lerp(_initScaleXMask, _currentPliage.maxSizeSpriteMask, prctAvancementSlide), _maskSprite.localScale.y, _maskSprite.localScale.z);
            _currentPliage.boundarySprite.color = Color.Lerp(_currentPliage.colorBoundary, _currentPliage.colorValidationPliage, prctAvancementSlide);
        }

        if (_currentPliage != null && _lastPosOrigami != _currentPliage.offsetPlacementPliage && indexPliage > 0)
        {
            _timerTransitionCentrageOrigami += Time.deltaTime;
            //pliageObject.localPosition = Vector3.Lerp(_lastPosOrigami, _currentPliage.offsetPlacementPliage, _timerTransitionCentrageOrigami * _speedAnimCenterOrigami);
        }

        if (OrigamiIsFinish())
        {
            _switchModePlayerOrigami._OnModeEnd = true;
        }

        if (_tempTimerBounce > 0)
        {
            _tempTimerBounce -= Time.deltaTime;
        }

        if (_particulePlayed && _tempTimerParticule > 0)
        {
            _tempTimerParticule -= Time.deltaTime;
        }

        // Si c'est une confirmation de pli et que le pliage est fini et que le bounce est fini
        if (_currentPliage.isConfirmationPliage && CurrentFoldsIsFinish() && _tempTimerBounce <= 0 && !_isRotating && !_rotatingIsFinish && _currentPliage.makeRotation )
        {
            _isRotating = true;
            _bounceIsFinish = true;
            _lastOffset = _currentPliage.yValueWanted;
            _timerRotation = 0f;
        }

        // Si la rotation n'est pas égale à la rotation finale et qu'il est en train de rotate
        if (_isRotating && _bounceIsFinish && _timerRotation < 1f)
        {
            // pliageObject.localRotation = Quaternion.Lerp(Quaternion.Euler(lastRotation.x, lastRotation.y, lastRotation.z), Quaternion.Euler(lastRotation.x, _currentPliage.rotationOffset, lastRotation.z), _timerRotation);
            pliageObject.localEulerAngles = Vector3.Lerp(_lastRotation, new Vector3(_lastRotation.x, _lastOffset, _lastRotation.z), _timerRotation);
            _timerRotation += Time.deltaTime;
        }
        else if (_timerRotation >= 1f && _isRotating)
        {
            pliageObject.localEulerAngles = new Vector3(_lastRotation.x, _lastOffset, _lastRotation.z);
            _lastRotation = pliageObject.localEulerAngles;
            _isRotating = false;
            _rotatingIsFinish = true;
        }

    }

    public bool OrigamiIsFinish()
    {
        return _origamiIsFinish;
    }

    public void SetUpCurrentPliage()
    {
        if (OrigamiIsFinish())
        {
            _animator.Play(_currentPliage.animToPlay.name, -1, 1);
            _boundaryAnimator.Play("BoundaryNone");
            _handGO.SetActive(false);
            SetActiveCursor(false);
            _animator.speed = 0;
            return;
        }

        _currentPliage = _listePliage.GetPliage(indexPliage);
        _lastPosOrigami = pliageObject.localPosition;
        _pointSelectedOrigami.SetPointGoodSelection(_currentPliage.goodPointSelection);
        _cursorSelectPoint.transform.position = _currentPliage.goodPointSelection.position;
        _cursorSelectPoint.transform.rotation = _currentPliage.goodPointSelection.rotation;
        _cursorEndPointSelect.transform.position = _currentPliage.endPointSelection.position;
        _cursorEndPointSelect.transform.rotation = _currentPliage.endPointSelection.rotation;
        SetActiveCursor(true);
        _animator.speed = 0;
        _animator.Play(_currentPliage.animToPlay.name);
        _currentPliage.boundarySprite.color = _currentPliage.colorBoundary;
        _currentFoldIsFinish = false;
        if (_currentPliage.boundaryAnim != null)
        {
            _boundaryAnimator.Play(_currentPliage.boundaryAnim.name);
        }
        else
        {
            _boundaryAnimator.Play("BoundaryNone");
        }

        if (_currentPliage.isConfirmationPliage)
        {

            // Enable hand's Gameobject
            // Enable hand's animation
            if (_currentPliage.handAnim)
            {
                _handGO.SetActive(true);
                _handAnimator.Play(_currentPliage.handAnim.name);
            }
        }
    }

    public void ResetPliage()
    {
        indexPliage = 0;
        _currentPliage = _listePliage.GetPliage(indexPliage);
        _pointSelectedOrigami.SetPointGoodSelection(_currentPliage.goodPointSelection);
        _cursorSelectPoint.transform.position = _currentPliage.goodPointSelection.position;
        _cursorSelectPoint.transform.rotation = _currentPliage.goodPointSelection.rotation;
        _cursorEndPointSelect.transform.position = _currentPliage.endPointSelection.position;
        _cursorEndPointSelect.transform.rotation = _currentPliage.endPointSelection.rotation;
        _animator.speed = 0;
        _animator.Play(_currentPliage.animToPlay.name);
        _origamiIsFinish = false;
        _currentPliage.boundarySprite.color = _currentPliage.colorBoundary;
        _currentFoldIsFinish = false;
    }

    public void SetActiveCursor(bool value)
    {
        _cursorSelectPoint.SetActive(_currentPliage.drawPointSelection && value);
        _cursorEndPointSelect.SetActive(_currentPliage.drawPointSelection && !value);
    }

    public bool CurrentAnimIsFinish()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }

    public bool CurrentFoldsIsFinish()
    {
        return GetPourcentageAvancementSlide() > _currentPliage.prctMinValueToCompleteFold && !_reverseAnim && _pointSelectedOrigami.AsStartesGoodSelection();
    }

    public float GetPourcentageAvancementSlide()
    {
        Vector3 posHitOrigami = _pointSelectedOrigami.GetPosHitOrigami();
        float prctAvancementX = Mathf.InverseLerp(_currentPliage.goodPointSelection.position.x, _currentPliage.endPointSelection.position.x, posHitOrigami.x);
        float prctAvancementY = Mathf.InverseLerp(_currentPliage.goodPointSelection.position.y, _currentPliage.endPointSelection.position.y, posHitOrigami.y);
        float prctAvancementZ = Mathf.InverseLerp(_currentPliage.goodPointSelection.position.z, _currentPliage.endPointSelection.position.z, posHitOrigami.z);
        return (prctAvancementX + prctAvancementY + prctAvancementZ) / 3;
    }

    public Vector3 GetPosAvancementSlideByPrct(float prctAvancement)
    {
        return Vector3.Lerp(_currentPliage.goodPointSelection.position, _currentPliage.endPointSelection.position, prctAvancement);
    }

    IEnumerator BoundariesFeedback()
    {
        for (int i = 0; i < _currentPliage.listBoundaryParticle.Count; i++)
        {
            _currentPliage.listBoundaryParticle[i].Play();

        }

        _currentPliage.playedParticleOnce = false;
        yield return new WaitForSeconds(0.1f);

    }
}
