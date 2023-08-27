using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

// Forces reticle to be positioned in itself
public class PositionReticle : MonoBehaviour
{
    Vector2 basePosition;

    [SerializeField]
    Vector2 position;

    private void OnTriggerEnter2D(Collider2D collision) {
        basePosition = StandardUISelectorElements.instance.reticleInRange.rectTransform.position;
        StandardUISelectorElements.instance.reticleInRange.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        StandardUISelectorElements.instance.reticleInRange.rectTransform.position = basePosition;
    }
}
