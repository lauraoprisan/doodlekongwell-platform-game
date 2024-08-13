using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VoidEventListener : MonoBehaviour {
    [Tooltip("Event to register with.")]
    public VoidEventChannel Event; //the GameEvent (actual GameEvent)

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent Response;

    private void Start() {
        Debug.Log("from listener: " + gameObject);
    }

    private void OnEnable() {

        Event.RegisterListener(this);
    }
    private void OnDisable() {
        Event.UnRegisterListener(this);
    }
    public void OnEventRaised() {
        Debug.Log("from listener. event raised: " + gameObject);
        Response.Invoke();
    }
}
