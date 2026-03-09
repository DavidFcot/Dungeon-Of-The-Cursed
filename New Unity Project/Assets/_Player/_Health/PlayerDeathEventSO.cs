using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerDeathEventSO", menuName = "Scriptable Objects/PlayerDeathEventSO")]
public class PlayerDeathEventSO : ScriptableObject
{
    [Tooltip("The action to perform")]
    public UnityAction OnEventRaised;

    public void Raise()
    {
        if(OnEventRaised != null)
        {
            OnEventRaised.Invoke();
        }
    }
}
