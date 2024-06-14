using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSkyBox : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private Light moon;
    [SerializeField] private float dayTIme = 120f;
    [SerializeField] private float timeMultiplier = 1f;
    [SerializeField] private Material[] skyBoxMaterial;
    [SerializeField] private Color[] color;

    [Range(0, 1)] public float currentTime = 0f;
    private GameManager gm;

    public bool onGame = true;
    private void Start()
    {
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
        SkyBoxChange();

        if (onGame)
        {
            currentTime += (Time.deltaTime / dayTIme) * timeMultiplier;
            //gm.Ui.RotationClockHand(currentTime);

            if (currentTime >= 1 && gm.Spawn.customerList.Count == 0)
            {
                onGame = false;

                gm.MainCharacter.GoHouse();
                for (int i = 0; i < gm.Spawn.elemental.Count; i++)
                {
                    ElementalNpc elem = gm.Spawn.elemental[i].transform.GetComponent<ElementalNpc>();
                    elem.GoHouse();
                    elem.sType = ServingType.Idle;
                }
            }
        }
    }
    private void Sun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 50, 170, 0);
        moon.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 50, 170, 0);
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
    private void SkyBoxChange()
    {
        if (currentTime >= 0.5f && currentTime <0.8f)
        {
            RenderSettings.skybox = skyBoxMaterial[1];
            sun.color = color[1];
            RenderSettings.fogColor = color[1];
        }
        else if (currentTime >=0.8f)
        {
            RenderSettings.skybox = skyBoxMaterial[2];
            RenderSettings.fogColor = color[2];
            moon.gameObject.SetActive(true);
            sun.gameObject.SetActive(false);
        }
        else
        {
            RenderSettings.skybox = skyBoxMaterial[0];
            sun.color = color[0];
            RenderSettings.fogColor = color[0];
            moon.gameObject.SetActive(false);
            sun.gameObject.SetActive(true);
        }
    }
    public void NextDay()
    {
        onGame = true;
        currentTime = 0;
        gm.Ui.OnNextButton();
        gm.MainCharacter.GoCounter();

        for (int i = 0; i < gm.Spawn.elemental.Count; i++)
        {
            ElementalNpc elem = gm.Spawn.elemental[i].transform.GetComponent<ElementalNpc>();
            elem.GoStore();
        }
        gm.House.partner.Clear();
        gm.Spawn.customerList.Clear();
        gm.gamestate = GameState.Start;
    }
}
