using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingThoughtBalloon : ThoughtBalloon {
    [SerializeField]
    Vector2 originalPos;
    [SerializeField]
    Vector2 intermediatePosition1;
    [SerializeField]
    Vector2 intermediatePosition2;

    [SerializeField]
    float currentPace = 0f;

    protected override void DefineTargetPosition() {
        targetPos = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), -mainCamera.transform.position.z));
        PrepareBezierTrajectory();
    }

    protected override void Move() {
        // Move around in a bezier curve
        currentPace += Time.deltaTime * speed;
        float t = Mathf.Min(1f, currentPace);
        float t2 = t * t;
        float t3 = t2 * t;

        // Bezier formula
        transform.position = originalPos * (-t3 + 3 * t2 - 3 * t + 1) + intermediatePosition1 * (3 * t3 - 6 * t2 + 3 * t) + intermediatePosition2 * (-3 * t3 + 3 * t2) + targetPos * t3;

        if (t == 1f) {
            DefineTargetPosition();
        }
    }

    public override void Frighten(Vector3 playerPosition) {
        base.Frighten(playerPosition);

        PrepareBezierTrajectory();
    }

    private void PrepareBezierTrajectory() {
        originalPos = transform.position;
        intermediatePosition1 = Vector3.Lerp(originalPos, Vector3.zero, Random.Range(0, 1f));
        intermediatePosition2 = Vector3.Lerp(targetPos, Vector3.zero, Random.Range(0, 1f));
        currentPace = 0f;
    }
}
