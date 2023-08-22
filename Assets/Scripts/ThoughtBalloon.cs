using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtBalloon : MonoBehaviour
{
    [SerializeField]
    Vector3 targetPos;

    [SerializeField]
    float speed = 1f;

    [SerializeField]
    string thoughtText;

    SpriteRenderer spriteRenderer;
    [SerializeField]
    Canvas canvasElement;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DefineTargetPosition();
    }

    void Update()
    {
        Move();
    }

    void DefineTargetPosition() {
        Camera mainCamera = Camera.main;
        targetPos = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), -mainCamera.transform.position.z));
    }

    // Sets the sorting order for the balloon and its canvas
    public void SetSortingOrder(int sortingOrder) {
        spriteRenderer.sortingOrder = sortingOrder;
        // Make sure canvas is always in front
        canvasElement.sortingOrder = sortingOrder + 1;
    }

    public string ReadThought() {
        // Indicate balloon read
        spriteRenderer.color = Color.white;

        // Get text
        return thoughtText;
    }

    protected virtual void Move() {
        // Move around the screen view
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        if (transform.position == targetPos) {
            DefineTargetPosition();
        }
    }
}
