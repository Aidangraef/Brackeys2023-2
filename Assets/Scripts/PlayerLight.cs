using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("ThoughtBalloon")) {
            collision.GetComponent<ThoughtBalloon>().Frighten(transform.position);
        }
    }
}
