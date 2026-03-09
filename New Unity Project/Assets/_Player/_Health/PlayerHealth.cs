using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerDeathEventSO playerDeathEventSO;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private Spawner spawner;
    [SerializeField] TextMeshProUGUI hud;

    public int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        if(currentHealth < 1)
        {
            playerDeathEventSO.Raise();
        }
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        hud.text = "Health: " + currentHealth.ToString("F0") + "%\n";
        hud.text += "Wave: " + spawner.currrentWave.ToString("F0");
    }
}
