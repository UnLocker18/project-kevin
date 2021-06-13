using UnityEngine;
using System;

public class EnvironmentInteractions : MonoBehaviour
{
    [SerializeField] public int currentPersonality = 0;
    [SerializeField] public Interactable currentInteractable;
    [SerializeField] public RopeLinkable currentRl;

    [SerializeField] private Rope currentRope;
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
        //if (currentInteractable.GetType() == typeof(Rope)) currentRope = currentInteractable.GetComponent<Rope>();

        if (currentRope != null && currentRl != null)
        {
            if (!currentRope.stickObjects.Contains(currentRl))
            {
                //currentRope.stickObjects.Add(currentRl);
                currentRl.Connect(currentPersonality, currentRope);
            }
            else
            {
                //currentRope.stickObjects.Remove(currentRl);
                currentRl.Disconnect(currentPersonality, currentRope);
            }
            //currentRope.GenerateRope(transform);

            if (currentRope.stickObjects.Count == 0) LeaveRope();
        }
        else if (currentRope == null && currentRl != null)
        {
            if (currentRl.connectedRopes.Count == 1)
            {
                currentRope = currentRl.connectedRopes[0];
                //currentRope.stickObjects.Remove(currentRl);
                currentRl.Disconnect(currentPersonality, currentRope);
                //currentRope.GenerateRope(transform);                
            }
            else if (currentRl.connectedRopes.Count > 1)
            {
                currentRl.DisconnectAll(currentPersonality);
            }
        }

        //currentRope = null;
    }

    private void TakeRope()
    {
        currentRope = currentInteractable.GetComponent<Rope>();
        uIManager.ShowRopeIndicator(currentRope.GetComponent<Outline>().OutlineColor);
    }

    public void LeaveRope()
    {
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
