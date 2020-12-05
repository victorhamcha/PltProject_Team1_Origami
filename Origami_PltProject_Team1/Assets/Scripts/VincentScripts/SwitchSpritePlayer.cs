using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSpritePlayer : MonoBehaviour
{

    private bool _asTimbre = false;
    private bool _asFinishOrigamiFacteur = false;
    private bool _asFinishOrigamiPecheur = false;
    private SkinnedMeshRenderer _skinnedMeshRenderer = null;

    [SerializeField] private Material matVide = null;
    [SerializeField] private Material matVideTimbre = null;
    [SerializeField] private Material matFacteur = null;
    [SerializeField] private Material matFacteurTimbre = null;
    [SerializeField] private Material matPecheur = null;
    [SerializeField] private Material matPecheurTimbre = null;

    private void Start()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_asTimbre)
        {
            if (_asFinishOrigamiPecheur && _skinnedMeshRenderer.material != matPecheurTimbre)
            {
                _skinnedMeshRenderer.material = matPecheurTimbre;
            }else if (_asFinishOrigamiFacteur && _skinnedMeshRenderer.material != matFacteurTimbre)
            {
                _skinnedMeshRenderer.material = matFacteurTimbre;
            }
            else if (_skinnedMeshRenderer.material != matVideTimbre)
            {
                _skinnedMeshRenderer.material = matVideTimbre;
            }
        }
        else
        {
            if (_asFinishOrigamiPecheur && _skinnedMeshRenderer.material != matPecheur)
            {
                _skinnedMeshRenderer.material = matPecheur;
            }
            else if (_asFinishOrigamiFacteur && _skinnedMeshRenderer.material != matFacteur)
            {
                _skinnedMeshRenderer.material = matFacteur;
            }
            else if (_skinnedMeshRenderer.material != matVide)
            {
                _skinnedMeshRenderer.material = matVide;
            }
        }
    }

    public void AsGetTimbre()
    {
        _asTimbre = true;
    }

    public void AsFinishFacteur()
    {
        _asFinishOrigamiFacteur = true;
    }

    public void AsFinishPecheur()
    {
        _asFinishOrigamiPecheur = true;
    }
}
