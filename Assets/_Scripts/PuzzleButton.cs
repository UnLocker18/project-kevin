using UnityEngine;
using System;

public class PuzzleButton : Interactable
{
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    public event Action<int, bool> ButtonPress;
    public int buttonNumber;

    private bool isActive = false;
    private Renderer[] _renderer;
    
    void Awake()
    {
        buttonNumber = int.Parse(gameObject.name.Substring(gameObject.name.Length - 1));
    }

    void Start()
    {
        isInteractable = true;
        _renderer = gameObject.GetComponentsInChildren<Renderer>();
    }

    public override void Interact(Transform mainCharacter)
    {
        Toggle();
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
}
