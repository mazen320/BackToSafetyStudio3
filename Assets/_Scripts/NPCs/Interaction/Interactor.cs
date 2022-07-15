using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    //Script attatched only to the player.
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 1f;
    [SerializeField] private LayerMask interactableMask;//Layer mask for the NPCs.
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int collidersFound;

    private IInteractable _interactable;

    private void Update()
    {
        collidersFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);//finds up to 3 npc colliders within range of the interaction point of the player.

        if(collidersFound > 0)
        {
            _interactable = colliders[0].GetComponent<IInteractable>();
            if(_interactable != null)
            {
                /* _interactable.Interact(this);//this is us, we are the interactor that is interacting with this interctable variable.*/
                if (!interactionPromptUI.isDisplayed)
                {
                    interactionPromptUI.OpenPanel(_interactable.interactionPrompt);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _interactable.Interact(this);
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
