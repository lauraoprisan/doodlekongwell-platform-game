using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassablePlatform : MonoBehaviour
{
    public Collider2D col;
    public bool isDashing;
    public bool playerInside;
    private Vector2 playerLastPosition;
  
    void Update()
    {
        if (isDashing && playerInside && IsPlayerComingFromBelow())
        {
            col.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInside = true;
            playerLastPosition = collision.transform.position;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInside = false;
        if (collision.gameObject.CompareTag("Player"))
        {
            col.enabled = true;
        }
    }

    
    public void HandlePlayerDash(bool isPlayerDashing)
    {
        isDashing = isPlayerDashing;
         Debug.Log("Player is dashing: " + isDashing);
    }

 
    private bool IsPlayerComingFromBelow()
    {
        return transform.position.y > playerLastPosition.y;
    }
}
