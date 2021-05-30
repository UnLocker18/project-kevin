using UnityEngine;

public class SmallBox : Interactable
{
    private void Awake()
    {
        interactable = true;
    }

    public override void Interact(Transform mainCharacter)
    {
        Grab(mainCharacter);
    }
}
