using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightHandler : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.IsNight()) // π„¿œ ∂ß
        {
            foreach (var light in FindObjectsOfType<Light2D>())
            {
                if (light.CompareTag("Light"))
                {
                    light.enabled = true;
                }
            }
        }
        else // ≥∑¿œ ∂ß
        {
            foreach (var light in FindObjectsOfType<Light2D>())
            {
                if (light.CompareTag("Light"))
                {
                    light.enabled = false;
                }
            }
        }
    }
}
