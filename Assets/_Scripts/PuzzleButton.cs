﻿using UnityEngine;
using System;

public class PuzzleButton : Interactable
{
    [SerializeField] private int requiredPersonality = 0;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    public event Action<int, bool> ButtonPress;
    public int buttonNumber;

    private bool isActive = false;
    private Renderer[] _renderer;
    private EnvironmentInteractions environmentInteractions;

    void Awake()
    {
        buttonNumber = int.Parse(gameObject.name.Substring(gameObject.name.Length - 1));
    }

    void Start()
    {
        _renderer = gameObject.GetComponentsInChildren<Renderer>();

        environmentInteractions = GameObject.Find("MainCharacter").GetComponent<EnvironmentInteractions>();
        if (environmentInteractions != null) environmentInteractions.ChangePersonality += ToggleInteractability;
    }

    public override int Interact(Transform mainCharacter)
    {
        if (!isInteractable) return -1;

        Toggle();

        return -1;
    }

    public void Toggle()
    {
        if (isActive)
        {
            _renderer[0].material = inactiveMaterial;
        }
        else
        {
            _renderer[0].material = activeMaterial;
        }

        isActive = !isActive;

        if (ButtonPress != null) ButtonPress.Invoke(buttonNumber, isActive);        
    }

    public void ToggleInteractability(int personality)
    {
        if (personality == requiredPersonality)
        {
            isInteractable = true;
        }
        else
        {
            isInteractable = false;
        }
    }
}
