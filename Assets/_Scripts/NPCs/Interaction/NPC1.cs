using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : MonoBehaviour, IInteractable
{
    //Each npc will have their own version of this script and implement the IInteractable interface, so they can have their own version of the Interact(); function.
    [SerializeField] private string prompt;
    public string interactionPrompt => prompt;

    public bool Interact(Interactor interactor)//Where you do something relative to the NPC.
    {
        Debug.Log("Talking to NPC1");
        return true;
    }

   
}
