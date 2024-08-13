using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolEventListener : MonoBehaviour {
    [Tooltip("Event to register with.")]
    public BoolEventChannel Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<bool> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }
    private void OnDisable() {
        Event.UnRegisterListener(this);
    }
    public void OnEventRaised(bool value) {
        Response.Invoke(value);
    }
}

