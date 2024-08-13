using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatformsBelowLava : MonoBehaviour
{
    private Transform endPosition;

    private void Start() {
        endPosition = transform.Find("EndPosition");
        Debug.Log("endPosition: " + endPosition);
    }

    private void Update() {
        if (Lava.Instance && (endPosition.position.y < Lava.Instance.transform.position.y)) {
            Debug.Log("platform section is below lava");
            Destroy(gameObject);
        }
    }

  
}
