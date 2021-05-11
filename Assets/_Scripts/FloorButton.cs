﻿using UnityEngine;
using System;

public class FloorButton : MonoBehaviour
{
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    public event Action<int, bool> ButtonPress;
    public int buttonNumber;

    private Renderer _renderer;    
    private bool isActive = false;
    
    void Awake()
    {        
        buttonNumber = int.Parse(gameObject.name.Substring(gameObject.name.Length - 1));
    }

    void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    public void Activate()
    {
        Toggle(true);
    }

    public void Deactivate()
    {
        Toggle(false);
    }

    void Toggle(bool activate)
    {
        if (activate != isActive)
        {
            isActive = activate;
            if (ButtonPress != null) ButtonPress.Invoke(buttonNumber, isActive);
        }

        if (isActive) _renderer.material = activeMaterial;
        else _renderer.material = inactiveMaterial;
    } 
}
