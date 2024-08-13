using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEventChannel", menuName = "ScriptableObjects/VoidEventChannel")]
public class VoidEventChannel : ScriptableObject {

    private List<VoidEventListener> eventListeners = new List<VoidEventListener>();

    public void RegisterListener(VoidEventListener listener) {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnRegisterListener(VoidEventListener listener) {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }

    public void Raise() {
        for (int i = eventListeners.Count - 1; i >= 0; i--) {
            eventListeners[i].OnEventRaised();
            Debug.Log("void listeners: " + eventListeners[i]);
        }
    }
}
