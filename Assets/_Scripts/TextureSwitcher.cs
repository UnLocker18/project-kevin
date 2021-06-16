using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSwitcher : MonoBehaviour
{
    [SerializeField] private Texture albedoStandard;
    [SerializeField] private Texture[] albedoPath;
    [SerializeField] private Texture[] emissionPath;

    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();

        renderer.material.EnableKeyword("_EMISSION");
    }

    public void SetTexture(string type, bool emission)
    {
        switch (type)
        {
            case "bottomleft":
                SetEmissive(emission, 0);
                break;
            case "bottomright":
                SetEmissive(emission, 1);
                break;
            case "centerbottom":
                SetEmissive(emission, 2);
                break;
            case "centerleft":
                SetEmissive(emission, 3);
                break;
            case "centerright":
                SetEmissive(emission, 4);
                break;
            case "centertop":
                SetEmissive(emission, 5);
                break;
            case "leftright":
                SetEmissive(emission, 6);
                break;
            case "topbottom":
                SetEmissive(emission, 7);
                break;
            case "topleft":
                SetEmissive(emission, 8);
                break;
            case "topright":
                SetEmissive(emission, 9);
                break;
        }
    }

    private void SetEmissive(bool value, int texIndex)
    {
        if (renderer == null) renderer = GetComponentInChildren<Renderer>();

        if (value)
        {
            renderer.material.SetTexture("_MainTex", albedoStandard);
            renderer.material.SetColor("_EmissionColor", Color.white);
            renderer.material.SetTexture("_EmissionMap", emissionPath[texIndex]);
        }
        else
        {
            renderer.material.SetColor("_EmissionColor", Color.black);
            renderer.material.SetTexture("_MainTex", albedoPath[texIndex]);
        }
    }
}
