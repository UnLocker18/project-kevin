using UnityEngine;
using System;

public class PuzzleButton : MonoBehaviour
{
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    public event Action<int, bool> ButtonPress;
    public int buttonNumber;

    private bool isActive = false;
    private Renderer[] _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = gameObject.GetComponentsInChildren<Renderer>();
        buttonNumber = int.Parse(gameObject.name.Substring(gameObject.name.Length - 1));
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
