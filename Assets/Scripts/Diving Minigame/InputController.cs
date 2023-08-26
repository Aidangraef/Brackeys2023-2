using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    Transform playerLight;

    Camera mainCamera;

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
                if (hit.collider.CompareTag("ThoughtBalloon"))
                {
                    // Catch thought balloon
                    ThoughtBalloon thought = hit.collider.GetComponent<ThoughtBalloon>();
                    AkSoundEngine.PostEvent("divePop", this.gameObject);

                    // If no one was clicked yet, select this one
                    if (clickedThought == null || currentClickedThoughtSortingOrder < thought.SpriteRenderer.sortingOrder) {
                        clickedThought = hit.collider.gameObject;
                        currentClickedThoughtSortingOrder = thought.SpriteRenderer.sortingOrder;
                    } 
                }
            }

            // Check if any was selected
            if (clickedThought != null) {
                ThoughtBalloon balloon = clickedThought.GetComponent<ThoughtBalloon>();
                if (balloon.Special) {
                    MinigameController.controller.PlaySound("diveSuccess");
                    MinigameController.controller.EndMinigame(true);
                }
                else {
                    MinigameController.controller.PlaySound("divePop");
                    string thought = balloon.ReadThought();

                    MinigameController.controller.ShowThoughtText(thought);
                }
            }
        }
    }
}
