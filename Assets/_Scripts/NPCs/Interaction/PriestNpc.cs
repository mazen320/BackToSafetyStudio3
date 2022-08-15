using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PriestNpc : MonoBehaviour, IInteractable
{
    public GameObject dialogue;
    public GameObject lockedDialogue;
    public QuestManager questManager;

    //Each npc will have their own version of this script and implement the IInteractable interface, so they can have their own version of the Interact(); function.
    [SerializeField] private string personalPrompt;
    public string interactionPrompt => personalPrompt;//"interaactionPrompt" SInherited from the interface.

    public bool Interact(Interactor interactor)//Where you do something relative to the NPC.
    {
        Debug.Log("Npc 2 Talking ");
        if (questManager.objective5 == false)
        {
            Debug.Log("Finish first Objective ");
            lockedDialogue.SetActive(true);
            lockedDialogue.GetComponent<Dialogue>().textComponent.text = string.Empty;
            lockedDialogue.GetComponent<Dialogue>().StartDialogue();


        }
        else
        {
            questManager.objective6 = true;
            dialogue.SetActive(true);
            dialogue.GetComponent<Dialogue>().textComponent.text = string.Empty;
            dialogue.GetComponent<Dialogue>().StartDialogue();
        }
        return true;
    }

    public bool StopInteraction(Interactor interactor)
    {
        dialogue.GetComponent<Dialogue>().CheckForNextLine();
        return true;
        //throw new System.NotImplementedException();
    }
}
