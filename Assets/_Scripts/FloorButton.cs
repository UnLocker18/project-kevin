using UnityEngine;
using System;

public class FloorButton : MonoBehaviour
{
    //[SerializeField] private Door door;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    public event Action<int, bool> ButtonPress;
    public int buttonNumber;

    private Renderer _renderer;
    [SerializeField] private bool isActive = false;
    [SerializeField] private bool hasBoxOn = false;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        buttonNumber = int.Parse(gameObject.name.Substring(gameObject.name.Length - 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(bool activatorIsBox)
    {
        Toggle(true, activatorIsBox);
    }

    public void Deactivate(bool activatorIsBox)
    {
        Toggle(false, activatorIsBox);
    }

    void Toggle(bool activate, bool activatorIsBox)
    {
        if (activatorIsBox && !activate) hasBoxOn = !hasBoxOn;

        if (activate != isActive)
        {
            if (!hasBoxOn)
            {
                isActive = activate;
                if (ButtonPress != null) ButtonPress.Invoke(buttonNumber, isActive);
            }
        }

        if (activatorIsBox && activate) hasBoxOn = !hasBoxOn;

        if (isActive) _renderer.material = activeMaterial;
        else _renderer.material = inactiveMaterial;
    } 
}
