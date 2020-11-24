using System;
using UnityEngine;

public class SelectPointOrigami : MonoBehaviour
{
    private Transform _goodPointSelections = null;
    private Transform _pointSelected = null;

    //Point du click sur l'origami
    private Vector3 posHitOrigami = Vector3.zero;
    private TouchPhase _touchPhase;

    //Timer de la vibrations quand l'on prend le bon coin de l'origamie
    [SerializeField] private int _timeVibration = 50;
    //Pour savoir si on à commencez par touchez le bon coin de l'origamie dans notre slide sur l'écran 
    private bool _startWithGoodSelection = false;

    ClickClickManager clickManager = null;
    [SerializeField] private LayerMask layerOrigami = new LayerMask();

    private void Update()
    {
        //Si on touche l'écran
        //      Alors on récupère les infos de notre doigts sur l'écran
        //            on fais un Raycast avec notre caméra et la position de notre doigts sur l'écran pour récupérez le point de notre doigts dans le monde 3D
        //Sinon
        //      On met la touche fase à une valeur qu'on dira que c'est la valeur comme null
        clickManager = ClickClickManager.Instance;
        clickManager.RaycastClick(layerOrigami);

        if (clickManager.isTouch && clickManager.isTouchTarget)
        {
            _touchPhase = clickManager.touchPhase;


            if (_pointSelected == null)
            {
                _pointSelected = clickManager.hit.collider.transform;
            }
            posHitOrigami = clickManager.hit.point;

            //Debut du touch avec ou sans bonne selection du coin de l'origamie
            if (_touchPhase == TouchPhase.Began && IsGoodSelections())
            {
                _startWithGoodSelection = true;
                SoundManager.i.PlaySound(SoundManager.Sound.FoldsHandling);
                Vibration.Vibrate(_timeVibration);
            }
            else if (_touchPhase == TouchPhase.Began)
            {
                _startWithGoodSelection = false;
            }

            //Fin du touch
            if (_touchPhase == TouchPhase.Ended)
            {
                _pointSelected = null;
                if (AsStartesGoodSelection())
                {
                    SoundManager.i.PlaySound(SoundManager.Sound.FoldsDrop);
                }
            }
        }
        else
        {
            _touchPhase = TouchPhase.Canceled;
        }
       
    }

    public void SetPointGoodSelection(Transform pointSelections)
    {
        _goodPointSelections = pointSelections;
        _pointSelected = null;
        _startWithGoodSelection = false;
        posHitOrigami = _goodPointSelections.position;
    }

    public bool IsGoodSelections()
    {
        return _pointSelected == _goodPointSelections;
    }

    public Vector3 GetPosHitOrigami()
    {
        return posHitOrigami;
    }

    public TouchPhase GetTouchPhase()
    {
        return _touchPhase;
    }

    public bool AsStartesGoodSelection()
    {
        return _startWithGoodSelection;
    }

}
