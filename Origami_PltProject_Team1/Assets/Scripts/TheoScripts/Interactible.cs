using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    enum TypeInteraction
    {
        Feu,
        Trefles,
        Yoga,
        Chien
    }

    [SerializeField] private AnimationClip _animation;
    [SerializeField] private GameObject _bulle = null;
    [SerializeField] private TypeInteraction typeInteraction;
    [SerializeField] private Collectible col;
    public LayerMask layerBubule;

    private bool _isTrigger = false;
    private float _timer = 0.2f;
    private float duration = 0.2f;
    private bool isInAnim = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Inside Daddy");
        if (other.tag == "Player" && !_isTrigger)
        {
            Debug.Log("C'est le player");
            _isTrigger = true;
            _bulle.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isTrigger = false;
            _bulle.SetActive(false);

            if (isInAnim)
            {
                isInAnim = false;
                //stop l'anim
                HandleDialogue();
                col.InstantiateCol(true, Vector3.zero, this.gameObject);
            }
        }
    }

    private void HandleDialogue()
    {
        switch (typeInteraction)
        {
            case TypeInteraction.Feu:
                string[] dialoguesFeu = { "Pas assez cuit...", "Parfait !", "Cramé..." };
                string dialFeu = dialoguesFeu[Random.Range(0, dialoguesFeu.Length)];
                //GameManager.Instance.GetDialogueManager().StartDialogue();
                break;
            case TypeInteraction.Trefles:
                string[] dialoguesTrefles = { "Pas de chance...", "Wow la chance !" };
                string dialTrefle = dialoguesTrefles[Random.Range(0, dialoguesTrefles.Length)];
                //Lance le dialogue
                break;
            case TypeInteraction.Yoga:
                string dialYoga = "Aah... Ca détend";
                //Lance le dialogue
                break;
        }
    }

    private void ClickClickBubule()
    {
        isInAnim = true;
        Debug.Log("Trop content le chien");
        // Activer son
        // Lancer l'anim
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        ClickClickManager.Instance.RaycastClick(layerBubule);
        if (ClickClickManager.Instance.isTouch && ClickClickManager.Instance.isTouchTarget && _isTrigger)
        {
            if (_timer <= 0)
            {
                ClickClickBubule();
                _timer = duration;
            }
        }
    }
}
