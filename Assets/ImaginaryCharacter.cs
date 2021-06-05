using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaginaryCharacter : Interactable
{
    [SerializeField] private int requiredPersonality = 1;

    private EnvironmentInteractions environmentInteractions;
    private Renderer renderer;

    private void Start()
    {
        environmentInteractions = GameObject.Find("MainCharacter").GetComponent<EnvironmentInteractions>();        
        if (environmentInteractions != null) environmentInteractions.ChangePersonality += ToggleInteractability;

        renderer = gameObject.GetComponentInChildren<Renderer>();
        if (renderer != null) renderer.enabled = false;
    }

    private void ToggleInteractability(int personality)
    {
        if (renderer == null) return;

        if (personality == requiredPersonality)
        {
            renderer.enabled = true;
            isInteractable = true;
        }
        else
        {
            renderer.enabled = false;
            isInteractable = false;
        }
    }

    public override int Interact(Transform mainCharacter)
    {
        if (!isInteractable) return -1;

        return -1;
    }    
}
