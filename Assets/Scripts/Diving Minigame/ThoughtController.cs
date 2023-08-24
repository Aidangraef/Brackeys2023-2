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

    [SerializeField]
    List<IrrelevantThought> characterThoughts;

    void Start() {
        // Keep camera to get random positions
        Camera mainCamera = Camera.main;

        // Load amount of memories visited from gamecontroller
        int memoriesSeen = GameController.controller.MemoriesSeen.Count;

        int allThoughts = baseBalloonQty + memoriesSeen * balloonIncrementByMemory;

        // Load irrelevant thoughts
        characterThoughts = new List<IrrelevantThought>(GameController.controller.CharacterThoughts[(int)GameController.controller.CurrentCharacterDive].thoughtsList);

        // Create all balloons
        for (int i = 0; i < allThoughts; i++) {
            GameObject newBalloon = Instantiate(balloonPrefabs[Random.Range(0, balloonPrefabs.Count)], transform);
            // Set position
            newBalloon.transform.position = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), -mainCamera.transform.position.z));

            ThoughtBalloon thoughtScript = newBalloon.GetComponent<ThoughtBalloon>();

            // The first balloon is always the right one, so it gets behind all others
            if (i == 0) {
                // Set right balloon
                thoughtScript.Special = true;
            } else {
                // Fill with irrelevant thought
                int thoughtIndex = Random.Range(0, characterThoughts.Count);
                IrrelevantThought irrelevantThought = characterThoughts[thoughtIndex];
                thoughtScript.FillBalloon(irrelevantThought.feeling, irrelevantThought.thought);
                if (characterThoughts.Count > 1) {
                    characterThoughts.RemoveAt(thoughtIndex);
                }
            }

            // Set balloon sort order
            thoughtScript.SetSortingOrder(i * 2);
        }
    }

}
