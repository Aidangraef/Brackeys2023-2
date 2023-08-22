using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryThoughtBalloon : ThoughtBalloon {

    void Update()
    {
        Move();
    }

    protected override void DefineTargetPosition() {
        targetPos = transform.position;
    }

}
