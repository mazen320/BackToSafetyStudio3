using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : MonoBehaviour, IInteractable
{
    public GameObject dialogue;
    public QuestMaybe quest;
    //Each npc will have their own version of this script and implement the IInteractable interface, so they can have their own version of the Interact(); function.
    [SerializeField] private string personalPrompt;
    public string interactionPrompt => personalPrompt;//"interaactionPrompt" SInherited from the interface.

    public bool Interact(Interactor interactor)//Where you do something relative to the NPC.
    {
        Debug.Log("Talking to NPC1");
        if(quest.objective1 == false)
        {
            Debug.Log("you cant talk just yet");
        }
        else if (dialogue.activeInHierarchy == false)
        {
            dialogue.SetActive(true);
            dialogue.GetComponent<Dialogue>().textComponent.text = string.Empty;
            dialogue.GetComponent<Dialogue>().StartDialogue();
        }
        return true;
    }

    public bool StopInteraction(Interactor interactor)
    {
        dialogue.GetComponent<Dialogue>().CheckForNextLine();
        return false;
        //throw new System.NotImplementedException();
    }
}
