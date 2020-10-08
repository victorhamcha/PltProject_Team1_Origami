using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Pliage
{
    public Transform[] pointSelections = null;
    public Transform goodPointSelection = null;
    public AnimationClip animToPlay = null;
}

public class ListePliage : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private List<Pliage> allPliageToDo = null;

    private void Start()
    {
        if (allPliageToDo.Count == 0)
        {
            Debug.LogError("/!\\ Aucun pliage de prédéfinie");
        }
    }

    public bool CanGoToNextPliage(int indexNextPliage)
    {
        if (allPliageToDo.Count < indexNextPliage + 1)
        {
            return false;
        }
        return true;
    }

    public Pliage GetPliage(int index)
    {
        if (!CanGoToNextPliage(index))
        {
            Debug.LogError("Out off bound liste pliage");
            return null;
        }
        return allPliageToDo[index];
    }

    public Animator GetAnimator()
    {
        return _animator;
    }

}
