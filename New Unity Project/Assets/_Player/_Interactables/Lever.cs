using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject lever;
    [SerializeField] private PlayerController playerController;
    private bool gateOpen;
    private bool leverPull = false;
    public AudioClip gateSound;
    
    void Update()
    {
        if(playerController.swordEquipped == false)
        {
            promptMessage = "Collect a weapon before pulling this lever.";
        }
        else if(gateOpen == true)
        {
            promptMessage = "The gate is open";
        }
        else
        {
            promptMessage = "Press E to pull this lever";
        }
    }
    
    private IEnumerator GateCloseTime()
    {
        yield return new WaitForSeconds(5f);
        gateOpen = !gateOpen;
        gate.GetComponent<Animator>().SetBool("IsOpen", gateOpen);
        leverPull = !leverPull;
        lever.GetComponent<Animator>().SetBool("IsPull", leverPull);
        GetComponent<AudioSource>().PlayOneShot(gateSound);
    }
    
    protected override void Interact()
    {
        if(playerController.swordEquipped == true)
        {    
            if(gateOpen == false)
            {
                gateOpen = !gateOpen;
                gate.GetComponent<Animator>().SetBool("IsOpen", gateOpen);
                leverPull = !leverPull;
                lever.GetComponent<Animator>().SetBool("IsPull", leverPull);
                GetComponent<AudioSource>().PlayOneShot(gateSound);
                StartCoroutine(GateCloseTime());
            }
        }
    }
}
