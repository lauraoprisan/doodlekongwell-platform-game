using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeadlyPlatform : MonoBehaviour
{
    public Collider2D col;
    public UnityEvent PlayerDeath;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerDeath?.Invoke();
            Debug.Log("game over from dead zone");
        }
    }
}
