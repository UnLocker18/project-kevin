using UnityEngine;
using System;

public class EnvironmentInteractions : MonoBehaviour
{
    [SerializeField] public int currentPersonality;
    [SerializeField] public Interactable currentInteractable;
    [SerializeField] public RopeLinkable currentRl;

    [SerializeField] private Rope currentRope;
    public event Action<int> ChangePersonality;

    public void Interaction()
    {
        int newPersonality = -1;
        if (currentInteractable != null) newPersonality = currentInteractable.Interact(gameObject.transform);
        if (newPersonality != -1)
        {
            if (ChangePersonality != null) ChangePersonality.Invoke(newPersonality);
            currentPersonality = newPersonality;
        }
    }

    public void StickRope()
    {
        if (currentInteractable.GetType() == typeof(Rope)) currentRope = currentInteractable.GetComponent<Rope>();

        if (currentRope != null && currentRl != null)
        {
            if (!currentRope.stickObjects.Contains(currentRl))
            {
                currentRope.stickObjects.Add(currentRl);
                currentRl.Connect();
            }
            else
            {
                currentRope.stickObjects.Remove(currentRl);
                currentRl.Disconnect();
            }            
            currentRope.GenerateRope();
        }

        currentRope = null;
    }
}
