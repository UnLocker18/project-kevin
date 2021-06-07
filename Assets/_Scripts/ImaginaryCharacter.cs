using DG.Tweening;
using UnityEngine;

public class ImaginaryCharacter : Interactable
{
    [SerializeField] private int requiredPersonality = 1;

    private EnvironmentInteractions environmentInteractions;
    private Renderer renderer;
    private Vector3 afterInteractionPosition;
    private Vector3 afterInteractionRotation;

    private void Start()
    {
        environmentInteractions = GameObject.Find("MainCharacter").GetComponent<EnvironmentInteractions>();        
        if (environmentInteractions != null) environmentInteractions.ChangePersonality += ToggleInteractability;

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

        transform.DORotate(afterInteractionRotation, 0.5f);
        transform.DOMove(new Vector3(afterInteractionPosition.x, 0, afterInteractionPosition.z), 1);

        return -1;
    }    
}
