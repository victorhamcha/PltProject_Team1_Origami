using System;
using System.Collections.Generic;
using UnityEngine;

//Déf d'un pliage et ce dont il à besoin
[Serializable]
public class Pliage
{
    [Header("Custom Folds")]
    public Transform goodPointSelection = null;
    public Transform endPointSelection = null;
    public AnimationClip animToPlay = null;

    [Header("Custom FX")]
    public bool isConfirmationPliage = false;
    public AnimationClip handAnim = null;
    public AnimationClip boundaryAnim = null;
    public SpriteRenderer boundarySprite = null;
    public Color colorValidationPliage = Color.green;
    public Color colorBoundary = Color.red;
    public bool drawPointSelection = true;

    [Header("Particle Manager")]
    public List<ParticleSystem> listBoundaryParticle = new List<ParticleSystem>();
    public bool playedParticleOnce = false;

    [Header("Auto Complete")]
    //Pourcentage to auto complete le pliage en cours
    [Range(0, 1)] public float prctMinValueToCompleteFold = 0.5f;
    //Vitesse de l'auto Complete
    [Range(0, 1)] public float speedAnimAutoComplete = 0.5f;
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
