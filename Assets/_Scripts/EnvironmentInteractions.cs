using UnityEngine;
using System;

public class EnvironmentInteractions : MonoBehaviour
{
    [SerializeField] public int currentPersonality = 0;
    [SerializeField] public Interactable currentInteractable;
    [SerializeField] public RopeLinkable currentRl;
    [SerializeField] public Rope currentRope;

    public event Action<int> ChangePersonality;

    private CheckpointManager checkpointManager;
    private UIManager uIManager;

    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        checkpointManager = FindObjectOfType<CheckpointManager>();

        if (checkpointManager.lastCheckpointPos != null && checkpointManager.lastCheckpointPos != Vector3.zero)
        {
            currentPersonality = checkpointManager.personality;
            transform.position = checkpointManager.lastCheckpointPos;
            checkpointManager.respawned = true;
        }

        if (ChangePersonality != null) ChangePersonality.Invoke(currentPersonality);        
    }

    public void Interaction()
    {
        int newPersonality = -1;        

        if (currentInteractable != null)
        {
            newPersonality = currentInteractable.Interact(transform);
            if (currentInteractable.GetType() == typeof(Rope)) TakeRope();
            uIManager.ShowHint(currentInteractable);
        }

        if (newPersonality != -1)
        {
            if (ChangePersonality != null) ChangePersonality.Invoke(newPersonality);
            currentPersonality = newPersonality;            
        }

        if (currentRl == null) uIManager.HideHint();
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

            uIManager.SetAttachedNumber(currentRope.stickObjects.Count);
            uIManager.ShowRopeHint(currentRope, currentRl);

            if (currentRope.stickObjects.Count == 0) LeaveRope();            
        }
        else if (currentRope == null && currentRl != null)
        {
            if (currentRl.connectedRopes.Count == 1)
            {
                currentRope = currentRl.connectedRopes[0];
                currentRl.Disconnect(currentPersonality, currentRope);
                currentRope = null;
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

        uIManager.SetAttachedNumber(currentRope.stickObjects.Count);
        uIManager.ShowRopeIndicator(currentRope.ropeColor);
    }

    public void LeaveRope()
    {
        if (currentRl != null) currentRl.SetOutline(Color.black, false);        

        if (currentRope != null) currentRope.DropRope();
        currentRope = null;
                
        uIManager.HideRopeIndicator();
    }

    public void SetPersonality(int personality)
    {
        if (ChangePersonality != null) ChangePersonality.Invoke(personality);
        currentPersonality = personality;

        if (currentInteractable != null)
        {
            currentInteractable.SetOutline(true);
            uIManager.ShowHint(currentInteractable);
        }
    }
}
