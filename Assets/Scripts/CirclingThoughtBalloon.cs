using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingThoughtBalloon : ThoughtBalloon {
    [SerializeField]
    Vector3 originalPos;
    [SerializeField]
    Vector3 intermediatePosition1;
    [SerializeField]
    Vector3 intermediatePosition2;

    [SerializeField]
    float currentPace = 0f;

    void Update()
    {
        Move();
    }

    protected override void DefineTargetPosition() {
        targetPos = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), -mainCamera.transform.position.z));
        originalPos = transform.position;
        intermediatePosition1 = Vector3.Lerp(originalPos, Vector3.zero, Random.Range(0, 1f));
        intermediatePosition2 = Vector3.Lerp(targetPos, Vector3.zero, Random.Range(0, 1f));
        currentPace = 0f;

        speed = Random.Range(0.2f, 2f);
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
}
