using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaMovement : MonoBehaviour {
    [SerializeField] private float minimumSpeed;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float maxPlayerLavaDistance;
    [SerializeField] private float startDelay;
    private float timer;
    private float stopTimerAt = -0.5f;

    private void Start() {
        timer = startDelay;
    }

    private void Update() {
        if (timer > stopTimerAt) {
            timer -= Time.deltaTime;
        }

        float playerLavaDistance = Vector3.Distance(transform.position, Player.Instance.transform.position);
        bool isLavaBelowPlayer = transform.position.y < Player.Instance.transform.position.y;


        // Constant upward movement of the lava
        if (timer <= 0) {
            Vector3 upwardMovement = Vector3.up * minimumSpeed * Time.deltaTime;
            transform.Translate(upwardMovement);

            // Managing max player-lava distance movement
            if (playerLavaDistance > maxPlayerLavaDistance && isLavaBelowPlayer) {
                Vector3 desiredPosition = new Vector3(transform.position.x, Player.Instance.transform.position.y - maxPlayerLavaDistance, transform.position.z);
                Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

                // Combine upward movement with smoothing to max distance permitted
                transform.position = new Vector3(smoothPosition.x, smoothPosition.y + upwardMovement.y, smoothPosition.z);
            }
        }
    }
}
