using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private GameManager() { }

    [SerializeField] private DialoguesManager _dialogueManager;
    [SerializeField] private PliageManager _pliageManager;
    [SerializeField] private SwitchModePlayerOrigami _switchModeOrigami;
    [SerializeField] private Entity _entity;
    [SerializeField] private SoundManager _soundManager;

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
        _soundManager = SoundManager.i;
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



}
