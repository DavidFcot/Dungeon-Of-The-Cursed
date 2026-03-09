using UnityEngine;
using UnityEngine.Events;

public class GetSword : Interactable
{   
    public UnityEvent Sword;
    public UnityEvent NoSword;

    [SerializeField] private PlayerController playerController;
    
    void Start()
    {
        BaseState();
    }
    
    void CollectSword()
    {
        Sword?.Invoke();
        playerController.swordEquipped = true;
        promptMessage = "";
    }
    
    void BaseState()
    {
        NoSword?.Invoke();
    }

    protected override void Interact()
    {
        CollectSword();
    }
}
