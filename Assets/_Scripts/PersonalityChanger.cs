using UnityEngine;
using System;

public class PersonalityChanger : Interactable
{
    [SerializeField] public int personality = 0;

    private Trigger trigger;

    void Start()
    {
        isInteractable = true;
        trigger = GameObject.Find("MainCharacter").GetComponentInChildren<Trigger>();
    }

    public override int Interact(Transform mainCharacter)
    {
        if (!isInteractable) return -1;

        Destroy(gameObject);
        if (trigger != null) trigger.RemoveFromList(GetComponentInChildren<Collider>());

        return personality;
    }
}
