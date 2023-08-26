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
        characterThoughts = new List<IrrelevantThought>(GameController.controller.CharacterThoughts[(int)MinigameController.controller.CurrentCharacter].thoughtsList);

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

                // TODO Define feeling by getting current memory
                thoughtScript.FillBalloon(ConvertMemoryToFeeling(), "");
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

    string ConvertMemoryToFeeling() {
        switch (MinigameController.controller.CurrentMemory) {
            case MemoryEnum.BEN_PAST:
                return "Regret";
            case MemoryEnum.BEN_WALLY_GET_TIPSY:
                return "Guilt";
            case MemoryEnum.BEN_GUN_GOES_MISSING:
                return "Guilt";
            case MemoryEnum.TINA_SINGS_LA_CANTATA:
                return "Friend";
            case MemoryEnum.TINA_KEVIN_TOGETHER:
                return "Love";
            case MemoryEnum.TINA_BUSINESS_FAIL:
                return "Anger";
            case MemoryEnum.KEVIN_BEATS_VINNIE_POOL:
                return "Fear";
            case MemoryEnum.KEVIN_HIDES_NERDY_SIDE:
                return "Shame";
            case MemoryEnum.KEVIN_KNOWS_DETECTIVE:
                return "Anxiety";
            case MemoryEnum.WALLY_LOSES_VINNIE_POKER:
                return "Anger";
            case MemoryEnum.WALLY_SUSPICIOUS_PHONE_CALL:
                return "Disdain";
            case MemoryEnum.VINNIE_YOU_SHOT_VINNIE:
                return "Pure Fear";
            default:
                Debug.LogError("Could not convert memory to feeling!");
                return "";
        }
    }

}
