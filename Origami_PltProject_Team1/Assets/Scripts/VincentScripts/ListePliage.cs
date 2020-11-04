using System;
using System.Collections.Generic;
using UnityEngine;

//Déf d'un pliage et ce dont il à besoin
[Serializable]
public class Pliage
{
    public Transform goodPointSelection = null;
    public Transform endPointSelection = null;
    public AnimationClip animToPlay = null;
    public bool isConfirmationPliage = false;
    public AnimationClip handAnim = null;
    public AnimationClip boundaryAnim = null;
}

public class ListePliage : MonoBehaviour
{
    [SerializeField]
    private List<Pliage> allPliageToDo = null;

    private void Start()
    {
        if (allPliageToDo.Count == 0)
        {
            Debug.LogError("/!\\ Aucun pliage de prédéfinie");
        }
    }

    public bool CanGoToFolding(int indexNextPliage)
    {
        if (allPliageToDo.Count < indexNextPliage + 1)
        {
            return false;
        }
        return true;
    }

    public Pliage GetPliage(int index)
    {
        if (!CanGoToFolding(index))
        {
            Debug.LogError("/!\\ Out off bound liste pliage");
            return null;
        }
        return allPliageToDo[index];
    }

}
