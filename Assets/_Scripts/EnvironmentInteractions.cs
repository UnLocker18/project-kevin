using UnityEngine;
using System;

public class EnvironmentInteractions : MonoBehaviour
{
    [SerializeField] public int currentPersonality = 0;
    [SerializeField] public Interactable currentInteractable;
    [SerializeField] public RopeLinkable currentRl;
    [SerializeField] public Rope currentRope;

    public event Action<int> ChangePersonality;

    private UIManager uIManager;

    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        if (ChangePersonality != null) ChangePersonality.Invoke(currentPersonality);
    }

    public void Interaction()
    {
        int newPersonality = -1;        

        if (currentInteractable != null)
        {
            newPersonality = currentInteractable.Interact(transform);
            if (currentInteractable.GetType() == typeof(Rope)) TakeRope();
        }

        if (newPersonality != -1)
        {
            if (ChangePersonality != null) ChangePersonality.Invoke(newPersonality);
            currentPersonality = newPersonality;
        }
    }    

    public void StickRope()
    {
        if (currentRope != null && currentRl != null)
        {
            if (!currentRope.stickObjects.Contains(currentRl))
            {
                currentRl.Connect(currentPersonality, currentRope);
            }
            else
            {
                currentRl.Disconnect(currentPersonality, currentRope);
            }

            if (currentRope.stickObjects.Count == 0) LeaveRope();
        }
        else if (currentRope == null && currentRl != null)
        {
            if (currentRl.connectedRopes.Count == 1)
            {
                currentRope = currentRl.connectedRopes[0];
                currentRl.Disconnect(currentPersonality, currentRope);
            }
            else if (currentRl.connectedRopes.Count > 1)
            {
                currentRl.DisconnectAll(currentPersonality);
            }
        }
    }

    private void TakeRope()
    {
        if (currentRope != null) LeaveRope();

        currentRope = currentInteractable.GetComponent<Rope>();

        if (currentRl != null) currentRl.SetOutline(currentRope.ropeColor, true);

        uIManager.ShowRopeIndicator(currentRope.ropeColor);
    }

    public void LeaveRope()
    {
        if (currentRl != null) currentRl.SetOutline(Color.green, false);

        currentRope.DropRope();
        currentRope = null;
        uIManager.HideRopeIndicator();
    }

    public void SetPersonality(int personality)
    {
        if (ChangePersonality != null) ChangePersonality.Invoke(personality);
        currentPersonality = personality;
        if (currentInteractable != null) currentInteractable.SetOutline(true);
    }
}
