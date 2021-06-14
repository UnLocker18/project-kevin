using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSwitcher : MonoBehaviour
{
    [SerializeField] private Texture albedoStandard;
    [SerializeField] private Texture albedoPath;
    [SerializeField] private Texture emissionPath;

    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();

        renderer.material.EnableKeyword("_EMISSION");

        renderer.material.SetTexture("_MainTex", albedoStandard);

        renderer.material.SetColor("_EmissionColor", Color.white);
        renderer.material.SetTexture("_EmissionMap", emissionPath);
    }
}
