using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtBalloon : MonoBehaviour
{
    [SerializeField]
    protected Vector3 targetPos;

    [SerializeField]
    protected float speed = 1f;

    [SerializeField]
    protected string thoughtText;

    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    protected Canvas canvasElement;

    protected Camera mainCamera;

    protected bool frightened = false;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start() {
        mainCamera = Camera.main;
        DefineTargetPosition();
    }

    void Update()
    {
        if (frightened) {
            Retreat();
        }
        else {
            Move();
        }
    }

    protected virtual void DefineTargetPosition() {
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

    void Retreat() {
        // Move straight into target and then stop being frightened
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        if (transform.position == targetPos) {
            frightened = false;
            speed /= 2f;

            DefineTargetPosition();
        }
    }

    public void Frighten(Vector3 playerPosition) {
        frightened = true;
        targetPos = transform.position + (transform.position - playerPosition) * 2;

        speed *= 2f;
    }
}
