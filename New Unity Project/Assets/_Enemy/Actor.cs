using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private PlayerHealth playerHealth;
    
    public int currentHealth;
    public int maxHealth;

    void Awake()
    {
        spawner.enemyAmount += 1;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -=amount;

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        spawner.enemyAmount -= 1;
        Destroy(gameObject);
    }

    void Update()
    {
        if(playerHealth.currentHealth == 0)
        {
            currentHealth = maxHealth;
        }
    }
}
