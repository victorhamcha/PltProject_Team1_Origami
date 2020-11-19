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
}
