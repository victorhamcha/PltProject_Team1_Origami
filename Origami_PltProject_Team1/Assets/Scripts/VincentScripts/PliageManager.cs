using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SelectPointOrigami))]
[RequireComponent(typeof(ListePliage))]
[RequireComponent(typeof(Animator))]
public class PliageManager : MonoBehaviour
{

    //Lien des scripts
    private SelectPointOrigami _pointSelectedOrigami = null;
    private ListePliage _listePliage = null;

    //Informations du pliage en cours d'executions
    private Pliage currentPliage = null;

    //Animator qui va jouez les animations de pliage
    private Animator _animator = null;

    [Header("Custom OrigamiManager")]
    //Timer de la durée de la vibrations une fois qu'un pliage est fini
    [SerializeField] private int _timeVibrationEndPliage = 50;
    //Gameobject utilisez pour montrez qu'elle partie doit ètre selectionnez pour le debut du pliage en cours
    [SerializeField] private GameObject _cursorSelectPoint = null;
    [SerializeField] private GameObject _cursorEndPointSelect = null;
    //Vitesse de l'animations du pliage lorsque l'on relache le pliage
    [SerializeField] [Range(0, 1)] private float speedReverseAnim = 1f;

    //Indicateur du nombre de pliage effectuez pour parcourir la liste de tout les pliages à faires
    private int indexPliage = 0;

    private bool _origamiIsFinish = false;
    private bool _reverseAnim = false;
    private bool _currentFoldIsFinish = false;

    [Header("TUTO")]
    [SerializeField] private Animator _handAnimator = null;
    [SerializeField] private GameObject _handGO = null;

    [Header("Boundary Manager")]
    [SerializeField] private Animator _boundaryAnimator = null;
    [SerializeField] private Transform _maskSprite = null;

    [Header("Feedback Origami")]
    [SerializeField] private Animator _animatorOrigami = null;
    [SerializeField] private AnimationClip _animFeedBack = null;

    [Header("Séquenceur")]
    [SerializeField] private float _fixingTimer = 1.0f;
    private float _decrementingTimer = 0.0f;


    void Awake()
    {
        _pointSelectedOrigami = GetComponent<SelectPointOrigami>();
        _listePliage = GetComponent<ListePliage>();
        _animator = GetComponent<Animator>();

        //Set de la speed de l'animator à 0 pour évitez que l'animations se joue dés le debuts
        _animator.speed = 0;
        _decrementingTimer = _fixingTimer;
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
            currentPliage.boundarySprite.color = currentPliage.colorValidationPliage;
            _animator.speed = currentPliage.speedAnimAutoComplete;
            _currentFoldIsFinish = true;
            _cursorEndPointSelect.SetActive(false);
            if (!currentPliage.playedParticleOnce && !currentPliage.isConfirmationPliage)
            {
                StartCoroutine("BoundariesFeedback");
                currentPliage.playedParticleOnce = true;
            }
            if (currentPliage.playedBounceOnce)
            {
                currentPliage.playedBounceOnce = false;
                _animatorOrigami.Play(_animFeedBack.name, -1, 0);
            }
        }

        if (_currentFoldIsFinish && !OrigamiIsFinish())
        {
            _decrementingTimer -= Time.deltaTime;
            _maskSprite.localScale = new Vector3(Mathf.Lerp(1, currentPliage.maxSizeSpriteMask, _animator.GetCurrentAnimatorStateInfo(0).normalizedTime), 1, 1);
            currentPliage.boundarySprite.color = Color.Lerp(currentPliage.colorBoundary, currentPliage.colorValidationPliage, _animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }

        if (_currentFoldIsFinish && CurrentAnimIsFinish() && !OrigamiIsFinish() && _decrementingTimer <= 0.0f)
        {
            _decrementingTimer = _fixingTimer;
            Vibration.Vibrate(_timeVibrationEndPliage);
            indexPliage++;
            if (_listePliage.CanGoToFolding(indexPliage))
            {
                SetUpCurrentPliage();
            }
            else
            {
                _boundaryAnimator.Play("Boundary");
                _origamiIsFinish = true;
            }
        }

        //Récupération du pourcentage d'avancement entre le point de début et le point de fin du pliage actuel en cours en fontion du point ou l'on click
        float prctAvancementSlide = GetPourcentageAvancementSlide();

        if (_reverseAnim && !OrigamiIsFinish() && !_currentFoldIsFinish)
        {
            _maskSprite.localScale = new Vector3(Mathf.Lerp(currentPliage.maxSizeSpriteMask, 1, _animator.GetCurrentAnimatorStateInfo(0).normalizedTime), 1, 1);
            currentPliage.boundarySprite.color = Color.Lerp(currentPliage.colorValidationPliage, currentPliage.colorBoundary, _animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }

        //Si on arrète de touché l'écran ET qu'on avais bien sélectionnez le bont point de l'origamie ET que l'origamie n'est pas fini
        //      Alors on joue l'animation du pliage en cours à l'envers en calculant son bon point de départs pour que les deux animations soit sans discontinuité
        //Sinon Si on à le bont point de sélection ET que l'origamie n'est pas fini
        //      Alors on joue l'animations du pliage en cours en fonction du pourcentage d'avancement de notre doigts sur l'écran
        //
        if (!_currentFoldIsFinish && _pointSelectedOrigami.GetTouchPhase() == TouchPhase.Ended && _pointSelectedOrigami.AsStartesGoodSelection() && !OrigamiIsFinish())
        {
            _animator.Play(currentPliage.animToPlay.name + "_reverse", -1, 1 - prctAvancementSlide);
            _animator.speed = speedReverseAnim;
            _reverseAnim = true;
            SetActiveCursor(true);
        }
        else if (!_currentFoldIsFinish && _pointSelectedOrigami.IsGoodSelections() && !OrigamiIsFinish())
        {
            // Disable hand's gameobject
            _handGO.SetActive(false);
            _animator.Play(currentPliage.animToPlay.name, -1, prctAvancementSlide);
            _animator.speed = currentPliage.speedAnimAutoComplete;
            _reverseAnim = false;
            SetActiveCursor(false);
            _maskSprite.localScale = new Vector3(Mathf.Lerp(1, currentPliage.maxSizeSpriteMask, prctAvancementSlide), 1, 1);
            currentPliage.boundarySprite.color = Color.Lerp(currentPliage.colorBoundary, currentPliage.colorValidationPliage, prctAvancementSlide);
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
            _animator.Play(currentPliage.animToPlay.name, -1, 1);
            _boundaryAnimator.Play("Boundary");
            _handGO.SetActive(false);
            SetActiveCursor(false);
            _animator.speed = 0;
            return;
        }
        currentPliage = _listePliage.GetPliage(indexPliage);
        _pointSelectedOrigami.SetPointGoodSelection(currentPliage.goodPointSelection);
        _cursorSelectPoint.transform.position = currentPliage.goodPointSelection.position;
        _cursorSelectPoint.transform.rotation = currentPliage.goodPointSelection.rotation;
        _cursorEndPointSelect.transform.position = currentPliage.endPointSelection.position;
        _cursorEndPointSelect.transform.rotation = currentPliage.endPointSelection.rotation;
        SetActiveCursor(true);
        _animator.speed = 0;
        _animator.Play(currentPliage.animToPlay.name);
        currentPliage.boundarySprite.color = currentPliage.colorBoundary;
        _currentFoldIsFinish = false;
        if (currentPliage.boundaryAnim != null)
        {
            _boundaryAnimator.Play(currentPliage.boundaryAnim.name);
        }
        else
        {
            _boundaryAnimator.Play("Boundary");
        }

        if (currentPliage.isConfirmationPliage)
        {
            // Enable hand's Gameobject
            _handGO.SetActive(true);

            // Enable hand's animation
            _handAnimator.Play(currentPliage.handAnim.name);
        }
    }

    public void ResetPliage()
    {
        indexPliage = 0;
        currentPliage = _listePliage.GetPliage(indexPliage);
        _pointSelectedOrigami.SetPointGoodSelection(currentPliage.goodPointSelection);
        _cursorSelectPoint.transform.position = currentPliage.goodPointSelection.position;
        _cursorSelectPoint.transform.rotation = currentPliage.goodPointSelection.rotation;
        _cursorEndPointSelect.transform.position = currentPliage.endPointSelection.position;
        _cursorEndPointSelect.transform.rotation = currentPliage.endPointSelection.rotation;
        _animator.speed = 0;
        _animator.Play(currentPliage.animToPlay.name);
        _origamiIsFinish = false;
        currentPliage.boundarySprite.color = currentPliage.colorBoundary;
        _currentFoldIsFinish = false;
    }

    public void SetActiveCursor(bool value)
    {
        _cursorSelectPoint.SetActive(currentPliage.drawPointSelection && value);
        _cursorEndPointSelect.SetActive(currentPliage.drawPointSelection && !value);
    }

    public bool CurrentAnimIsFinish()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }

    public bool CurrentFoldsIsFinish()
    {
        return GetPourcentageAvancementSlide() > currentPliage.prctMinValueToCompleteFold && !_reverseAnim && _pointSelectedOrigami.AsStartesGoodSelection();
    }

    public float GetPourcentageAvancementSlide()
    {
        Vector3 posHitOrigami = _pointSelectedOrigami.GetPosHitOrigami();

        float prctAvancementX = Mathf.InverseLerp(currentPliage.goodPointSelection.position.x, currentPliage.endPointSelection.position.x, posHitOrigami.x);
        float prctAvancementY = Mathf.InverseLerp(currentPliage.goodPointSelection.position.y, currentPliage.endPointSelection.position.y, posHitOrigami.y);
        float prctAvancementZ = Mathf.InverseLerp(currentPliage.goodPointSelection.position.z, currentPliage.endPointSelection.position.z, posHitOrigami.z);
        return (prctAvancementX + prctAvancementY + prctAvancementZ) / 3;
    }

    public Vector3 GetPosAvancementSlideByPrct(float prctAvancement)
    {
        return Vector3.Lerp(currentPliage.goodPointSelection.position, currentPliage.endPointSelection.position, prctAvancement);
    }

    IEnumerator BoundariesFeedback()
    {

        Debug.Log(currentPliage);
        for (int i = 0; i < currentPliage.listBoundaryParticle.Count; i++)
        {
            currentPliage.listBoundaryParticle[i].Play();

        }

        currentPliage.playedParticleOnce = false;
        yield return new WaitForSeconds(0.1f);

    }
}
