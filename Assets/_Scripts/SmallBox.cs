using UnityEngine;

public class SmallBox : Interactable
{
    [SerializeField] private int requiredPersonality = 2;

    private EnvironmentInteractions environmentInteractions;

    private void Start()
    {
        environmentInteractions = GameObject.Find("MainCharacter").GetComponent<EnvironmentInteractions>();
        if (environmentInteractions != null) environmentInteractions.ChangePersonality += ToggleInteractability;
    }

    public override int Interact(Transform mainCharacter)
    {
        if (!isInteractable) return -1;

        Grab(mainCharacter);

        return -1;
    }

    private void ToggleInteractability(int personality)
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
