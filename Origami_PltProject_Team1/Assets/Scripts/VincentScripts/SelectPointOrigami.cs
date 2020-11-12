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

    public GameObject cubeTest = null;

    private void Update()
    {
        //Si on touche l'écran
        //      Alors on récupère les infos de notre doigts sur l'écran
        //            on fais un Raycast avec notre caméra et la position de notre doigts sur l'écran pour récupérez le point de notre doigts dans le monde 3D
        //Sinon
        //      On met la touche fase à une valeur qu'on dira que c'est la valeur comme null
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            _touchPhase = touch.phase;

            Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, 0);
            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (_pointSelected == null)
                {
                    _pointSelected = hit.collider.transform;
                }
                posHitOrigami = hit.point;

                cubeTest.transform.position = posHitOrigami;
            }

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
