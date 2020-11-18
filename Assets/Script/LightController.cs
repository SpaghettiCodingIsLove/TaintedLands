using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light sun;
    public float secondsInFullDay = 120f;

    [Range(0, 1)]
    public float currentTimeOfDay = 0;

    [HideInInspector]
    public float timeMultiplier = 1f;

    float sunInitialIntensity;

    Skybox skybox;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
        skybox = Camera.main.GetComponent<Skybox>();
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);
        float intensityMultiplier = 1;

        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= 0.25f)
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        else if (currentTimeOfDay >= 0.75f)
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));

        sun.intensity = sunInitialIntensity * intensityMultiplier;

    }

    private void UpdateExposure()
    {
        float exposure = 0f;

        if (currentTimeOfDay >= 0.5f)
            exposure = 2 - (currentTimeOfDay * 2);
        else
            exposure = currentTimeOfDay * 2;

        skybox.material.SetFloat("_Exposure", exposure);
    }

    void OnApplicationQuit()
    {
        skybox.material.SetFloat("_Exposure", 1);
    }

    void Update()
    {
        UpdateSun();
        UpdateExposure();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeOfDay >= 1)
            currentTimeOfDay = 0;
    }
}
