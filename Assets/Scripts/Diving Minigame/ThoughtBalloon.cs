using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThoughtBalloon : MonoBehaviour
{
    protected const float FRIGHTENED_SPEED = 4f;

    [SerializeField]
    protected Vector2 targetPos;

    [SerializeField]
    protected float minSpeed = 1f;
    [SerializeField]
    protected float maxSpeed = 2f;
    [SerializeField]
    protected float speed = 2f;

    [SerializeField]
    protected TextMeshProUGUI feelingTMP;
    [SerializeField]
    protected string thoughtText;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    protected ParticleSystem trailParticles;
    [SerializeField]
    protected ParticleSystem pulseParticles;
    [SerializeField]
    protected Collider2D thoughtCollider;

    [SerializeField]
    protected Canvas canvasElement;

    protected Camera mainCamera;

    protected bool frightened = false;

    protected bool special = false;

    [SerializeField]
    protected ThoughtBalloonFade fadeEffect;

    public bool Special { get => special; set => special = value; }
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }

    void Start() {
        mainCamera = Camera.main;
        DefineTargetPosition();

        BalloonSpecificStart();

        // Define speed
        DefineSpeed();

        PrepareAppearance();
    }

    public void FillBalloon(string feeling, string thought) {
        feelingTMP.text = feeling;
        thoughtText = thought;
    }

    protected virtual void BalloonSpecificStart() {

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
        // Indicate balloon read by fading away
        fadeEffect.enabled = true;

        // Disable collider
        thoughtCollider.enabled = false;

        // Stop particles
        trailParticles.Stop();
        pulseParticles.Stop();

        // Get text
        return thoughtText;
    }

    protected virtual void Move() {
        // Move around the screen view
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        if ((Vector2)transform.position == targetPos) {
            DefineTargetPosition();
        }
    }

    void Retreat() {
        // Move straight into target and then stop being frightened
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        if ((Vector2)transform.position == targetPos) {
            DefineTargetPosition();

            frightened = false;
            DefineSpeed();
        }
    }

    public virtual void Frighten(Vector3 playerPosition) {
        frightened = true;
        PrepareFrightenedTargetPosition(playerPosition);
        Debug.DrawLine(transform.position, targetPos);

        speed = FRIGHTENED_SPEED;
    }

    private void PrepareFrightenedTargetPosition(Vector3 playerPosition) {
        targetPos = transform.position + (transform.position - playerPosition) * 2;

        // Check if position valid
        if (CheckValidTargetPosition()) {
            return;
        }

        // Try to make a tangent escape
        targetPos = Quaternion.Euler(0, 0, Random.Range(0, 2) == 1 ? 90f : -90f) * ((transform.position - playerPosition).normalized * 2);

        // Check if position valid
        if (CheckValidTargetPosition()) {
            return;
        }

        // Escape through player
        targetPos = transform.position + (playerPosition - transform.position) * 2;
    }

    private bool CheckValidTargetPosition() {
        // Checks if target position is inside camera viewport
        Vector2 targetViewportPoint = mainCamera.WorldToViewportPoint(targetPos);

        return (0f <= targetViewportPoint.x && targetViewportPoint.x <= 1f &&
            0f <= targetViewportPoint.y && targetViewportPoint.y <= 1f);
    }

    protected void DefineSpeed() {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void PrepareAppearance() {
        if (special) {
            spriteRenderer.color = Color.white;

            // Change font color
            feelingTMP.color = Color.black;

            // Activate pulse
            pulseParticles.Play();
        } else {
            Color spriteColor = Color.Lerp(Color.gray / 2, Color.black, Random.Range(0f, 1f));
            spriteColor.a = 1f;
            spriteRenderer.color = spriteColor;

            // Prepare trail particles
            ParticleSystem.MainModule main = trailParticles.main;
            ParticleSystem.MinMaxGradient startColor = main.startColor;
            startColor.color = spriteColor;
            main.startColor = startColor;

            trailParticles.Play();
        }
    }
}
