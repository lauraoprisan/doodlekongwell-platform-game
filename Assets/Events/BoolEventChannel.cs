using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoolEventChannel", menuName = "ScriptableObjects/BoolEventChannel")]
public class BoolEventChannel : ScriptableObject
{
    private List<BoolEventListener> eventListeners = new List<BoolEventListener>();

    public void RegisterListener(BoolEventListener listener) 
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnRegisterListener(BoolEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }

    public void Raise(bool value)
    {
        for(int i = eventListeners.Count -1; i>=0; i--)
        {
            eventListeners[i].OnEventRaised(value);
        }
    }
}
