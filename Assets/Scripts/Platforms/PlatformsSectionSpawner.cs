using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsSectionSpawner : MonoBehaviour {
    //make a list with all the platformsSection
    //create a helper function that instantiate a random platformSection to a certain position
    //for the position, take the lastPlatformsSection's endpoint and maybe add additional y space (check that)
    //generate a new section if distance between player and lastPlatformSection is less then x amount

    [SerializeField] private Transform firstPlatformsSection;
    [SerializeField] private List<Transform> platformsSectionsList;
    //SerializeField] private Player player;
    private Vector3 lastEndPosition;
    [SerializeField] private float ySpawnOffset;
    private int PLAYER_PLATFORM_SPAWN_DISTANCE = 10;


    private void Awake() {
        lastEndPosition = firstPlatformsSection.Find("EndPosition").position;
    }

    void Update() {
        if (Player.Instance != null) {
            if (Vector3.Distance(Player.Instance.transform.position, lastEndPosition) < PLAYER_PLATFORM_SPAWN_DISTANCE) {
                SpawnPlatformsSection();
            }

        } else {
            Debug.LogError("Player is not assigned.");
        }

    }

    private void SpawnPlatformsSection() {
        Transform chosenPlatformsSection = platformsSectionsList[Random.Range(0, platformsSectionsList.Count)];
        Transform lastPlatformsSectionTransform = SpawnPlatformsSectionHelper(chosenPlatformsSection, lastEndPosition);
        lastEndPosition = lastPlatformsSectionTransform.Find("EndPosition").position;
    }

    private Transform SpawnPlatformsSectionHelper(Transform platformsSection, Vector3 spawnPosition) {
        Transform platformsSectionTransform = Instantiate(platformsSection, spawnPosition + new Vector3(0, ySpawnOffset, 0), Quaternion.identity);
        return platformsSectionTransform;
    }
}
