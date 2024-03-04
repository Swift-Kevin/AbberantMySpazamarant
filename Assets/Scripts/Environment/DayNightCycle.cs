using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;
    [SerializeField, Range(0, 24)] private float currTime;

    private void Update()
    {
        if (preset == null)
            return;

        if (Application.isPlaying)
        {
            currTime += Time.deltaTime;
            currTime %= 24;
            UpdateDayNightCycle(currTime/24f);
        }
        else
        {
            UpdateDayNightCycle(currTime / 24f);
        }
    }

    private void UpdateDayNightCycle(float timePercent)
    {
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);

        if (directionalLight)
        {
            directionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (directionalLight == null)
            return;

        if (RenderSettings.sun != null)
            directionalLight = RenderSettings.sun;
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}
