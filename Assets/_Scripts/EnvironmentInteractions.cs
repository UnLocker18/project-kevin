using UnityEngine;
using System;

public class EnvironmentInteractions : MonoBehaviour
{
    [SerializeField] public Interactable currentInteractable;
    [SerializeField] public RopeLinkable currentRl;

    [SerializeField] private Rope currentRope;
    


    public void Interaction()
    {
        if (currentInteractable != null) currentInteractable.Interact(gameObject.transform);
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
