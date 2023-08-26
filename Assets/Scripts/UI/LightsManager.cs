using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightsManager : MonoBehaviour
{
    public void LightCigar()
    {
        GetComponentInChildren<Light2D>().intensity = 1;
    }

    public void StopLightCigar()
    {
        GetComponentInChildren<Light2D>().intensity = 0;
    }
}
