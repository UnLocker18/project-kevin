using UnityEngine;

public class SmallBox : Interactable
{
    [SerializeField] public int requiredPersonality = 2;

    private EnvironmentInteractions environmentInteractions;
    public bool isLocked = false;

    private void Awake()
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

    public void ToggleInteractability(int personality)
    {
        if (personality == requiredPersonality && !isLocked)
        {
            isInteractable = true;
        }
        else
        {
            isInteractable = false;
        }
    }
}
