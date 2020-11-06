﻿using System.Collections;
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

    [SerializeField] private Animator _boundaryAnimator = null;
    [SerializeField] private List<ParticleSystem> listBoundaryParticle = new List<ParticleSystem>();
    private bool playedParticleOnce = false;

    void Awake()
    {
        
        _pointSelectedOrigami = GetComponent<SelectPointOrigami>();
        _listePliage = GetComponent<ListePliage>();
        _animator = GetComponent<Animator>();

        //Set de la speed de l'animator à 0 pour évitez que l'animations se joue dés le debuts
        _animator.speed = 0;
    }

    void Update()
    {
        //TODO UPDATE COM
        //Si le pliage en cours est fini et que l'origami n'est pas fini
        //      Alors on fait vibrez le télephone
        //            on passe au pliage suivant
        //      Si il y à bien un prochain pliage
        //          Alors on réinitialise les variables et on charges les nouvelles informations du prochain pliage
        //      Sinon on dit que l'origami est fini
        //
        if (CurrentFoldsIsFinish() && !OrigamiIsFinish())
        {
            currentPliage.boundarySprite.color = currentPliage.colorValidationPliage;
            _animator.speed = currentPliage.speedAnimAutoComplete;
            _currentFoldIsFinish = true;

            if(!playedParticleOnce)
            {
                StartCoroutine("BoundariesFeedback");
                playedParticleOnce = true;
            }
            
        }

        if (_currentFoldIsFinish && CurrentAnimIsFinish() && !OrigamiIsFinish())
        {
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
            _animator.Play(currentPliage.animToPlay.name, -1 , 1);
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
        _animator.speed = 0;
        _animator.Play(currentPliage.animToPlay.name);
        _origamiIsFinish = false;
        currentPliage.boundarySprite.color = currentPliage.colorBoundary;
        _currentFoldIsFinish = false;
    }

    public void SetActiveCursor(bool value)
    {
        _cursorSelectPoint.SetActive(currentPliage.drawPointSelection && value);
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
        for (int i = 0; i < listBoundaryParticle.Count; i++)
        {
            listBoundaryParticle[i].Play();
            Debug.Log(listBoundaryParticle[i].name);
        }
        yield return new WaitForSeconds(0.0f);
    }
}
