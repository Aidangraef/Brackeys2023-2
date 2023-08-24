using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> balloonPrefabs;

    [SerializeField]
    int baseBalloonQty = 8;

    [SerializeField]
    int balloonIncrementByMemory = 2;

    void Start() {
        // Keep camera to get random positions
        Camera mainCamera = Camera.main;

        // TODO Load amount of memories visited from gamecontroller
        int memoriesSeen = 0;

        int allThoughts = baseBalloonQty + memoriesSeen * balloonIncrementByMemory;

        // Create all balloons
        for (int i = 0; i < allThoughts; i++) {
            GameObject newBalloon = Instantiate(balloonPrefabs[Random.Range(0, balloonPrefabs.Count)], transform);
            // Set position
            newBalloon.transform.position = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), -mainCamera.transform.position.z));

            // The first balloon is always the right one, so it gets behind all others
            if (i == 0) {
                // Set right balloon
                newBalloon.GetComponent<ThoughtBalloon>().Special = true;
            }

            // Set balloon sort order
            newBalloon.GetComponent<ThoughtBalloon>().SetSortingOrder(i * 2);
        }
    }

}
