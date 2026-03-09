using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Spawner spawner;

    public int swordDamage = 1;

    void Awake()
    {
        swordDamage = spawner.currrentWave;
    }

    private void OnTriggerEnter()
    {
        playerHealth.currentHealth -= swordDamage;
    }
}