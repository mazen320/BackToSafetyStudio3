using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    
    public string interactionPrompt { get; }
    
    public void DisablePlayerMovement(PlayerScript playerScript, RifleManager playersRifle, SwitchCamera playersCamera)
    {
        playerScript.enabled = false;
        playersRifle.enabled = false;
        playersCamera.enabled = false;
    }
    public void EnablePlayerMovement(PlayerScript playerScript, RifleManager playersRifle, SwitchCamera playersCamera)
    {
        playerScript.enabled = true;
        playersRifle.enabled = true;
        playersCamera.enabled = true;
    }
    public bool Interact(Interactor interactor);

    public bool StopInteraction(Interactor interactor);
    
}
