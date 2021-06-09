using UnityEngine;
using System;
using DG.Tweening;

public class FloorButton : MonoBehaviour
{
    //[SerializeField] private Material activeMaterial;
    //[SerializeField] private Material inactiveMaterial;

    [SerializeField] private float range = 0.1f;
    [SerializeField] private float animationSeconds = 0.5f;

    public event Action<int, bool> ButtonPress;
    public int buttonNumber;

    //private Renderer _renderer;    
    private bool isActive = false;
    private Transform buttonMove;
    private Vector3 buttonMovePosition;
    
    void Awake()
    {        
        buttonNumber = int.Parse(gameObject.name.Substring(gameObject.name.Length - 1));
        buttonMove = transform.Find("Pulsante").GetChild(0);
        buttonMovePosition = buttonMove.position;
    }

    void Start()
    {
        //_renderer = GetComponentInChildren<Renderer>();

    }

    public void Activate()
    {
        buttonMove.DOLocalMoveY(buttonMovePosition.y - range, animationSeconds);
        Toggle(true);
    }

    public void Deactivate()
    {
        buttonMove.DOLocalMoveY(buttonMovePosition.y, animationSeconds);
        Toggle(false);
    }

    void Toggle(bool activate)
    {
        if (activate != isActive)
        {
            isActive = activate;
            if (ButtonPress != null) ButtonPress.Invoke(buttonNumber, isActive);
        }

        //if (isActive) _renderer.material = activeMaterial;
        //else _renderer.material = inactiveMaterial;
    } 
}
