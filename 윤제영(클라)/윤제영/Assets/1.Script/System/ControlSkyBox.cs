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

    private GameManager gm;

    private float sunInitIntensity;

    public bool onGame = true;
    private void Start()
    {
        sunInitIntensity = sun.intensity;
        gm = GameManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (gm.gamestate == GameState.Stop)
            return;

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.5f);

        Sun();
        OnSpawn();

        if (onGame)
        {
            currentTime += (Time.deltaTime / dayTIme) * timeMultiplier;

            if (currentTime >= 1)
            {
                currentTime = 0;
                onGame = false;
                for (int i = 0; i < gm.Spawn.elemental.Count; i++)
                { 
                    gm.Spawn.elemental[i].transform.GetComponent<ElementalNpc>().isRandom = false;
                    gm.Spawn.elemental[i].transform.GetComponent<ElementalNpc>().target = gm.House.housePos;
                }
                //GameManager.Instance.gamestate = GameState.Stop;
            }
        }
    }
    private void Sun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 90, 170, 0);
        moon.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 90, 170, 0);
    }
    private void OnSpawn()
    {
        if (currentTime >= 0.1f && currentTime <= 0.7f)
        {
            GameManager.Instance.Spawn.onSpawn = true;
        }
        else
        {
            GameManager.Instance.Spawn.onSpawn = false;
        }
    }
}
