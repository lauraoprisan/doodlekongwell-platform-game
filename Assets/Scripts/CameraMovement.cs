using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float smoothSpeed = 0.125f;
    private float playerYPosition;
    public Vector3 offset = new Vector3(0, 3.5f, 0);

    void LateUpdate() {
        playerYPosition = Player.Instance.transform.position.y;
       
        Vector3 desiredPosition = new Vector3(transform.position.x, playerYPosition, transform.position.z) + offset;

        if (desiredPosition.y < 0) {
            desiredPosition.y = 0;
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); 
        transform.position = smoothedPosition;
        
    }
}
