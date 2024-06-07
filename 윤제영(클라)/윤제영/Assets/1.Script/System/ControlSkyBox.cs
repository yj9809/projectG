using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSkyBox : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private Light moon;
    [SerializeField] private float dayTIme = 120f;
    [SerializeField, Range(0, 1)] private float currentTime = 0f;
    [SerializeField] private float timeMultiplier = 1f;
    [SerializeField] private Material[] skyBoxMaterial; 

    private float sunInitIntensity;
    private void Start()
    {
        sunInitIntensity = sun.intensity;
    }
    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.5f);
        
        Sun();

        currentTime += (Time.deltaTime / dayTIme) * timeMultiplier;

        if (currentTime >= 1)
        {
            currentTime = 0;
        }
    }
    private void Sun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 90, 170, 0);
        moon.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 90, 170, 0);
        float intensityMultiplier = 1;
        //if (currentTime <= 0.23f || currentTime >= 0.75f)
        //{
        //    intensityMultiplier = 0;
        //}
        //else if (currentTime <= 0.25f)
        //{
        //    intensityMultiplier = Mathf.Clamp01((currentTime - 0.23f) * (1 / 0.02f));
        //}
        //else if (currentTime >= 0.73f)
        //{
        //    intensityMultiplier = Mathf.Clamp01(1 - ((currentTime - 0.73f) * (1 / 0.02f)));
        //}

        //sun.intensity = currentTime * intensityMultiplier;
    }
}
