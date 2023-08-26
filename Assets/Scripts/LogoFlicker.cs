using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoFlicker : MonoBehaviour
{
    Material material;

    [SerializeField]
    float minFlickerPeriod = 0.1f;
    [SerializeField]
    float maxFlickerPeriod = 1.5f;
    [SerializeField]
    float flickerSpeed = 20f;
    float currentFlickerPosition = 0f;

    bool isFlickering = false;

    Color targetColor;

    Color baseColor;
    float colorDecreaseDuringFlicker = 6f;

    Coroutine constantFlickerCoroutine;

    void Awake() {
        material = Instantiate(GetComponent<Image>().material);
        GetComponent<Image>().material = material;
        baseColor = material.GetColor("_EmissionColor");

        // Start weaker
        material.SetColor("_EmissionColor", baseColor / colorDecreaseDuringFlicker);

        constantFlickerCoroutine = StartCoroutine(WaitAndFlicker());
    }

    private void Update() {
        if (isFlickering) {
            currentFlickerPosition += Time.deltaTime * flickerSpeed;
            Color currentColor;
            if (targetColor == Color.black) {
                currentColor = Color.Lerp(baseColor/ colorDecreaseDuringFlicker, targetColor, Mathf.Min(1f, currentFlickerPosition));
                if (currentFlickerPosition >= 1f) {
                    targetColor = baseColor / colorDecreaseDuringFlicker;
                    currentFlickerPosition = 0f;
                }
            } else {
                currentColor = Color.Lerp(Color.black, targetColor, Mathf.Min(1f, currentFlickerPosition));
                if (currentFlickerPosition >= 1f) {
                    isFlickering = false;
                }
            }
            material.SetColor("_EmissionColor", currentColor);
        }
    }

    IEnumerator WaitAndFlicker() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(minFlickerPeriod, maxFlickerPeriod));

            Flicker();
        }
    }

    public void Flicker() {
        isFlickering = true;
        targetColor = Color.black;
        currentFlickerPosition = 0f;
        AkSoundEngine.PostEvent("neonFlicker", this.gameObject);
    }

    public void StartSequence() {
        // Increase flicker duration
        flickerSpeed -= 2f;

        // Stop constant flickering
        StopCoroutine(constantFlickerCoroutine);
        StartCoroutine(BeginStartSequence());
    }

    IEnumerator BeginStartSequence() {
        while (isFlickering) {
            yield return new WaitForSeconds(0.05f);
        }
        Flicker();
        while (isFlickering) {
            yield return new WaitForSeconds(0.1f);
        }
        Flicker();
        while (isFlickering) {
            yield return new WaitForSeconds(0.05f);
        }
        material.SetColor("_EmissionColor", baseColor);

    }

    private void OnDestroy() {
        Destroy(material);
    }
}
