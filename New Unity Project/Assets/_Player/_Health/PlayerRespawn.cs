using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private PlayerDeathEventSO playerDeathEventSO;
    [SerializeField] private PlayerHealth playerHealth;
    CharacterController controller;
    
    private void OnEnable()
    {
        playerDeathEventSO.OnEventRaised += HandleEvent;
        controller = GetComponentInParent<CharacterController>();
    }

    private void OnDisable()
    {
        playerDeathEventSO.OnEventRaised -= HandleEvent;
    }

    private void HandleEvent()
    {
        controller.enabled = false;
        playerHealth.currentHealth = playerHealth.maxHealth;
        transform.position = new Vector3(-1.5f, 1.43f, -30.54f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        controller.enabled = true;
    }
}
