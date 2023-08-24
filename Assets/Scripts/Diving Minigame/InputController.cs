using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    Transform playerLight;

    Camera mainCamera;

    [SerializeField]
    bool clickEnabled;

    public bool ClickEnabled { get => clickEnabled; set => clickEnabled = value; }

    private void Start() {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Move light
        playerLight.position = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));

        // Check if click
        if (clickEnabled && Input.GetMouseButtonDown(0)) {
            // Check if click hit any of the thoughts
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);

            GameObject clickedThought = null;
            int currentClickedThoughtSortingOrder = -1;
            foreach (RaycastHit2D hit in hits) {
                if (hit.collider.CompareTag("ThoughtBalloon")) {
                    // If no one was clicked yet, select this one
                    if (clickedThought == null) {
                        clickedThought = hit.collider.gameObject;
                        currentClickedThoughtSortingOrder = hit.collider.GetComponent<SpriteRenderer>().sortingOrder;
                    } else if (currentClickedThoughtSortingOrder < hit.collider.GetComponent<SpriteRenderer>().sortingOrder) {
                        clickedThought = hit.collider.gameObject;
                        currentClickedThoughtSortingOrder = hit.collider.GetComponent<SpriteRenderer>().sortingOrder;
                    }
                }
            }

            // Check if any was selected
            if (clickedThought != null) {
                ThoughtBalloon balloon = clickedThought.GetComponent<ThoughtBalloon>();
                if (balloon.Special) {
                    MinigameController.controller.EndMinigame(true);
                }
                else {
                    string thought = balloon.ReadThought();

                    // TODO Avoid find first
                    FindFirstObjectByType<MinigameController>().ShowThoughtText(thought);
                }
            }
        }
    }
}
