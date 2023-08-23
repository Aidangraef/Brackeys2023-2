using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryThoughtBalloon : ThoughtBalloon {
    [SerializeField]
    Vector2 basePosition;

    protected override void BalloonSpecificStart() {
        basePosition = transform.position;
    }

    protected override void DefineTargetPosition() {
        if (frightened) {
            // Define a new base position
            basePosition = transform.position;
        }
        targetPos = basePosition + Random.insideUnitCircle*0.1f;
    }

}
