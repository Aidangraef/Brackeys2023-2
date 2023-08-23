using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [SerializeField]
    float startDelay = 0.7f;

    [Header("Mask settings")]
    [SerializeField]
    SpriteMask lightMask;
    [SerializeField]
    float targetCutoff = 0.2f;

    [Header("Light settings")]
    [SerializeField]
    SpriteRenderer lightView;

    [Header("Other components")]
    [SerializeField]
    InputController inputController;
    [SerializeField]
    CircleCollider2D circleCollider2D;

    private void Awake() {
        circleCollider2D.enabled = false;
    }

    private void Start() {
        lightMask.alphaCutoff = 1f;
        inputController.ClickEnabled = false;
    }

    private void Update() {
        lightMask.alphaCutoff = Mathf.MoveTowards(lightMask.alphaCutoff, targetCutoff, Time.deltaTime / startDelay);

        if (lightMask.alphaCutoff == targetCutoff) {
            inputController.ClickEnabled = true;
            circleCollider2D.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("ThoughtBalloon")) {
            collision.GetComponent<ThoughtBalloon>().Frighten(transform.position);
        }
    }
}
