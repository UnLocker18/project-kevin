using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaginaryCharacter : Interactable
{
    private EnvironmentInteractions environmentInteractions;
    private Renderer renderer;

    private void Start()
    {
        environmentInteractions = GameObject.Find("MainCharacter").GetComponent<EnvironmentInteractions>();        
        renderer = gameObject.GetComponentInChildren<Renderer>();

        renderer.enabled = false;
        if (environmentInteractions != null) environmentInteractions.ChangePersonality += ToggleVisibility;
    }

    private void ToggleVisibility(int personality)
    {
        if (renderer == null) return;

        if (personality == 1)
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
        return -1;
    }    
}
