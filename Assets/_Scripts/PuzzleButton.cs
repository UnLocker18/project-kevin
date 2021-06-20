using UnityEngine;
using System;
using DG.Tweening;
using System.Collections;

public class PuzzleButton : Interactable
{
    [SerializeField] public bool activeAtStart = false;
    [SerializeField] private int requiredPersonality = 0;
    [SerializeField] private float animationSeconds = 1f;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    public event Action<int, bool> ButtonPress;
    public int buttonNumber;

    [SerializeField] public bool isActive = false;
    [SerializeField] private bool westernVersion = false;
    private Renderer[] renderers;
    private Transform[] discs;
    private Transform movingPiece;
    private Vector3 movingPiecePos;
    private EnvironmentInteractions environmentInteractions;    

    void Awake()
    {
        buttonNumber = int.Parse(gameObject.name.Substring(gameObject.name.Length - 1));

        environmentInteractions = GameObject.FindGameObjectWithTag("Player").GetComponent<EnvironmentInteractions>();
        if (environmentInteractions != null) environmentInteractions.ChangePersonality += ToggleInteractability;
    }

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        discs = transform.GetChild(0).GetComponentsInChildren<Transform>();
        if (transform.childCount > 1)
        {
            movingPiece = transform.GetChild(1).Find("MovingPiece").GetComponent<Transform>();
            movingPiecePos = movingPiece.position;
        }       

        if (activeAtStart) StartCoroutine("DelayedToggle");
    }

    public override int Interact(Transform mainCharacter)
    {
        if (!isInteractable && !isActive) return -1;

        Toggle();

        if (!isInteractable && !isActive) outline.enabled = false;

        return -1;
    }

    public override void SetOutline(bool value)
    {
        SetUpOutline();

        if (value)
            outline.enabled = isInteractable || isActive;
        else
            outline.enabled = false;
    }

    public void Toggle()
    {
        if (isActive)
        {
            //renderers[0].material = inactiveMaterial;
            if (!westernVersion)
            {
                foreach (Renderer renderer in renderers)
                {
                    renderer.material = inactiveMaterial;
                }
            }
            PlayAnimation(0);
        }
        else
        {
            //renderers[0].material = activeMaterial;
            if (!westernVersion)
            {
                foreach (Renderer renderer in renderers)
                {
                    renderer.material = activeMaterial;
                }
            }
            PlayAnimation(1);
        }
        
        isActive = !isActive;

        if (ButtonPress != null) ButtonPress.Invoke(buttonNumber, isActive);        
    }

    private void PlayAnimation(int multiplier)
    {
        if (westernVersion)
        {
            movingPiece.DOMoveY(movingPiecePos.y + (0.1f * multiplier), animationSeconds);
        }
        else
        {
            int sign = 1;

            foreach (Transform disc in discs)
            {
                if (disc.name != "Piedistallo" && disc.name != "Mesh")
                {
                    disc.DORotate(new Vector3(0, multiplier * sign * 210, 0), animationSeconds);
                    sign = -sign;
                }
            }
        }
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

    private IEnumerator DelayedToggle()
    {
        yield return new WaitForSeconds(2f);
        Toggle();
    }
}
