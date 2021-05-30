using UnityEngine;

public class SmallBox : Interactable
{
    private void Start()
    {
        isInteractable = true;
    }

    public override void Interact(Transform mainCharacter)
    {
        Grab(mainCharacter);
    }
}
