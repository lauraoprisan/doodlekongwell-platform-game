using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeadZoneScript : MonoBehaviour
{
    public LogicScript logic;
    public GameObject player;

    public UnityEvent PlayerDeath;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        Debug.Log("from dead zone script");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            PlayerDeath?.Invoke();
            Debug.Log("game over from dead zone");
     

    }
}
