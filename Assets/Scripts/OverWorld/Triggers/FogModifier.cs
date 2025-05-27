using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogModifier : MonoBehaviour
{
    [SerializeField] private float targetDensity = 0.2f;

    [Tooltip("2Fade duration in seconds")]
    [SerializeField] private float fadeDuration = 1.0f;

    private float currentFogDensity;
    private float perc;

    private void OnTriggerEnter(Collider other)
    {
        currentFogDensity = RenderSettings.fogDensity;
        if (other.tag == "Player")
        {
            perc = 0;
            StartCoroutine(FadeFog());
        }
    }

    private IEnumerator FadeFog()
    {
        while (RenderSettings.fogDensity != targetDensity)
        {
            perc += Time.deltaTime / fadeDuration;
            RenderSettings.fogDensity = Mathf.Lerp(currentFogDensity, targetDensity, perc);
            print(RenderSettings.fogDensity);

            // Let this frame be rendered and continue from here in the next frame
            yield return null;
        }
    }
}
