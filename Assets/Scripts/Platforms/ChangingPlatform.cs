using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingPlatform : MonoBehaviour {
    [SerializeField] private Renderer platformRenderer; 
    [SerializeField] private Color redColor = Color.red;
    [SerializeField] private Color greenColor = Color.green;
    [SerializeField] private MonoBehaviour deadlyPlatformScript; 
    [SerializeField] private MonoBehaviour boolListenerScript;

    private float intervalToChange = 2f;
    private float timer = 0;
    private bool isRed = true;

    void Start() {
        timer = intervalToChange;
        UpdatePlatform();
    }

    void Update() {
        timer -= Time.deltaTime;

        if (timer <= 0) {
            HandleChangePlatform();
            timer = intervalToChange; 
        }
    }

    void HandleChangePlatform() {
        isRed = !isRed;
        UpdatePlatform();
    }

    void UpdatePlatform() {
        if (isRed) {
            platformRenderer.material.color = redColor;
            deadlyPlatformScript.enabled = true;
            boolListenerScript.enabled = false;
        } else {
            platformRenderer.material.color = greenColor;
            deadlyPlatformScript.enabled = false;
            boolListenerScript.enabled = true;
        }
    }
}
