using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //message displayed to player when looking for an interactable.
    public string promptMessage;

    //This function is called from the player.
    public void BaseInteract()
    {
        Interact();
    }
    
    protected virtual void Interact()
    {
        //No code in this function. Template function to be overriden by subclasses.
    }
}