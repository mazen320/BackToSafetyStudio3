using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    //Script attatched only to the player.
    [Header("Interaction")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 1f;//How far the player can be to interact with NPCs.
    [SerializeField] private LayerMask interactableMask;//Layer mask for the NPCs.
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int collidersFound;

    private IInteractable _interactable;
    //public Dialogue dialogue;
    [Header("DisablingOtherScripts")]
    public GameObject weopon;
    public PlayerScript playerScript;
    public RifleManager rifleManager;
    public SwitchCamera switchCamera;

    public void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        rifleManager = GameObject.Find("WPN_AKM").GetComponent<RifleManager>();
        switchCamera = GameObject.Find("WPN_AKM").GetComponent<SwitchCamera>();

    }
    private void Update()
    {
        collidersFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);//finds up to 3 npc colliders within range of the interaction point of the player.

        if(collidersFound > 0)
        {
            _interactable = colliders[0].GetComponent<IInteractable>();//Each NPC inherits from IInteractable and they are what we are actually getting.
            if(_interactable != null)
            {
                Debug.Log("You are at " + colliders[0].gameObject.name);
                /* _interactable.Interact(this);//this is us, we are the interactor that is interacting with this interctable variable.*/
                if (!interactionPromptUI.isDisplayed)
                {
                    interactionPromptUI.OpenPanel(_interactable.interactionPrompt);//The same game object panel is always opened it is just the text 
                                                                                   //that changes based on which NPC's _interactable you've gotten.
                }
              /*  if (interactionPromptUI.UIPanel.activeInHierarchy)
                {
                    playerScript.enabled = false;
                }
                else
                {
                    playerScript.enabled = true;
                }*/
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactionPromptUI.ClosePanel();
                    _interactable.DisablePlayerMovement(playerScript, rifleManager, switchCamera, weopon);
                    _interactable.Interact(this);//That specific NPC will do what it's programmed to do.
                    _interactable.StopInteraction(this);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    _interactable.EnablePlayerMovement(playerScript, rifleManager, switchCamera, weopon);
                }
            }
           
        }
        else
        {
            if(_interactable != null)
            {
                
                _interactable = null;
            }
            if (interactionPromptUI.isDisplayed)
            {
                interactionPromptUI.ClosePanel();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
