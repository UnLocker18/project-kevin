using UnityEngine;

public class SmallBox : Interactable
{
    private void Start()
    {
        isInteractable = true;
    }

    public override int Interact(Transform mainCharacter)
    {
        Grab(mainCharacter);

        return -1;
    }
}
