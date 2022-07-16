using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2 : MonoBehaviour, IInteractable
{
    public GameObject dialogue;
    //Each npc will have their own version of this script and implement the IInteractable interface, so they can have their own version of the Interact(); function.
    [SerializeField] private string personalPrompt;
    public string interactionPrompt => personalPrompt;//"interaactionPrompt" SInherited from the interface.

    public bool Interact(Interactor interactor)//Where you do something relative to the NPC.
    {
        Debug.Log("I am NPC2 bro...");
        dialogue.SetActive(true);
        dialogue.GetComponent<Dialogue>().StartDialogue();
        return true;
    }

    public bool StopInteraction(Interactor interactor)
    {
        dialogue.GetComponent<Dialogue>().CheckForNextLine();
        return true;
        //throw new System.NotImplementedException();
    }
}
