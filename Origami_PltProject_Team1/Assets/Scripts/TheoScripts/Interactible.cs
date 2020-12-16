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

    [SerializeField] private GameObject _bulle = null;
    [SerializeField] private TypeInteraction typeInteraction;
    [SerializeField] private Collectible col;
    public LayerMask layerBubule;

    private bool _isTrigger = false;
    private float _timer = 0.2f;
    private float duration = 0.2f;
    private bool isInAnim = false;
    private int _DoggoCount = 0;
    public bool SpawnCollectible = false;

    private Animator _animator;

    void Start()
    {
        _animator = GameManager.Instance.GetEntity()._animator;
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
                _animator.SetBool(GetAnimatorBool(), false);
                HandleDialogue();

                if (SpawnCollectible)
                {
                    Debug.Log("Oui ca spawn");
                col = gameObject.GetComponent<Collectible>();
                col.InstantiateCol(SpawnCollectible, Vector3.zero, col.collectibleCurve);
                SpawnCollectible = false;
                    Debug.Log("SpawnCollectible");
                }
            }
        }
    }

    private void ClickClickBubule()
    {
        isInAnim = true;
        // Activer son
        _animator.SetBool(GetAnimatorBool(), true);
        if (typeInteraction == TypeInteraction.Chien)
        {
            SoundManager.i.PlaySound(SoundManager.Sound.VOC_SFX_Dog_Yap_03);
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
                GameManager.Instance.GetSucces("Hmmmm.....");
                break;
            case TypeInteraction.Trefles:
                string[] dialoguesTrefles = { "Pas de chance...", "Wow la chance !" };
                string dialTrefle = dialoguesTrefles[Random.Range(0, dialoguesTrefles.Length)];
                GameManager.Instance.GetSucces("It's your lucky day !");
                //Lance le dialogue
                break;
            case TypeInteraction.Yoga:
                string dialYoga = "Aah... Ca détend";
                GameManager.Instance.GetSucces("Zen");
                //Lance le dialogue
                break;
            case TypeInteraction.Chien:
                GameManager.Instance.GetSucces("Who's a good boy ?");
                _DoggoCount++;
                if (_DoggoCount >= 5)
                {
                    GameManager.Instance.GetSucces("Best Doggo");
                }
                break;
        }
    }

    private string GetAnimatorBool()
    {
        switch (typeInteraction)
        {
            case TypeInteraction.Feu:
                return "Idle";
            case TypeInteraction.Chien:
                return "Caresser";
            case TypeInteraction.Trefles:
                return "Trefles";
            case TypeInteraction.Yoga:
                return "Stretch";
        }
        return null;
    }
}
