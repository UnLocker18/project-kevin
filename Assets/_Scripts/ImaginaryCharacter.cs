using DG.Tweening;
using UnityEngine;

public class ImaginaryCharacter : Interactable
{
    [SerializeField] private int requiredPersonality = 1;
    [SerializeField] private float animationSeconds = 1.5f;
    [SerializeField] private float rotationSeconds = 0.5f;

    private EnvironmentInteractions environmentInteractions;
    private Renderer renderer;
    private Vector3 afterInteractionPosition;
    private Vector3 afterInteractionRotation;    

    private void Awake()
    {
        environmentInteractions = GameObject.Find("MainCharacter").GetComponent<EnvironmentInteractions>();
        if (environmentInteractions != null) environmentInteractions.ChangePersonality += ToggleInteractability;
    }

    private void Start()
    {
        renderer = gameObject.GetComponentInChildren<Renderer>();
        if (renderer != null) renderer.enabled = false;

        afterInteractionPosition = transform.Find("AfterInteraction").position;
        afterInteractionRotation = transform.Find("AfterInteraction").eulerAngles;
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
        
        transform.DORotate(afterInteractionRotation + new Vector3(0f, 90f, 0f), rotationSeconds);
        transform.DOMove(new Vector3(afterInteractionPosition.x, 0, afterInteractionPosition.z), animationSeconds);

        return -1;
    }    
}
