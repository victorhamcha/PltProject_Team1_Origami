using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private GameManager() { }

    [SerializeField] private DialoguesManager _dialogueManager;
    [SerializeField] private SwitchModePlayerOrigami _switchModeOrigami;
    [SerializeField] private Entity _entity;
    [SerializeField] private SoundManager _soundManager;

    [SerializeField] private GameObject candyCanvas;
    [SerializeField] private float candyBaseTimer;
    private CanvasGroup candyCanvasGroup;
    private bool candyIsActive = false;
    private float candyTimer;

    [SerializeField] private ZoomVignette _zoomVignette;

    private PliageManager _pliageManager;

    [SerializeField] private List<GameObject> _listPliage = new List<GameObject>();

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("GameManager", typeof(GameManager)).GetComponent<GameManager>();
                }
            }

            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        _pliageManager = _switchModeOrigami._pliageToDo.GetComponent<PliageManager>();
        _soundManager = SoundManager.i;
        candyTimer = candyBaseTimer;
        candyCanvasGroup = candyCanvas.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (candyIsActive)
        {
            if (candyTimer > 0f)
            {
                candyTimer -= Time.deltaTime;
                if (candyBaseTimer - candyTimer > 1f)
                    candyCanvasGroup.alpha -= Time.deltaTime / candyBaseTimer;
            }
            else
            {
                candyIsActive = false;
                candyCanvas.SetActive(false);
                candyTimer = candyBaseTimer;
            }
        }
    }

    #region FunctionGet

    public DialoguesManager GetDialogueManager()
    {
        return _dialogueManager;
    }

    public PliageManager GetPliageManager()
    {
        return _pliageManager;
    }

    public void SetPliageManager(PliageManager manager)
    {
        _pliageManager = manager;
    }

    public SwitchModePlayerOrigami GetSwitchModePlayerOrigami()
    {
        return _switchModeOrigami;
    }

    public Entity GetEntity()
    {
        return _entity;
    }

    public SoundManager GetSoundManager()
    {
        return _soundManager;
    }

    public void ActivateCandyCrush(string mot)
    {
        Debug.Log("cc = ");
        candyIsActive = true;
        candyCanvas.GetComponentInChildren<Text>().text = mot;
        candyCanvas.SetActive(true);
    }

    #endregion

    #region ChangePliage

    private GameObject GetPliage(string name)
    {
        for (int i = 0; i <= _listPliage.Count; i++)
        {
            if (name == _listPliage[i].name)
            {
                return _listPliage[i];
            }
        }
        return null;
    }

    public void SetUpPliage(string name)
    {
        _switchModeOrigami._pliageToDo = GetPliage(name);
        _pliageManager = _switchModeOrigami._pliageToDo.GetComponent<PliageManager>();
        _switchModeOrigami.ActiveOrigami();
    }
    #endregion

    public ZoomVignette GetZoomVignette()
    {
        return _zoomVignette;
    }

}
