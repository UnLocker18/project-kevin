using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private FloorButton floorButton;    
    private EnvironmentInteractions environmentInteractions;
    private UIManager uIManager;
    [SerializeField] private List<Collider> triggerList = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        environmentInteractions = GetComponentInParent<EnvironmentInteractions>();
        floorButton = GetComponentInParent<FloorButton>();
        uIManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("TriggerVisible")) return;

        if (floorButton != null)
        {
            if (other.gameObject.GetComponentInParent<Rope>() == null) AddToList(other);
            if (triggerList.Count > 0) floorButton.Activate();
        }            

        if (environmentInteractions != null)
        {
            AddToList(other);

            Interactable interactable = triggerList[0].gameObject.GetComponentInParent<Interactable>();
            RopeLinkable rl = null;

            foreach (Collider trigger in triggerList)
            {
                if (trigger.gameObject.GetComponentInParent<RopeLinkable>() != null) rl = trigger.gameObject.GetComponentInParent<RopeLinkable>();
            }

            environmentInteractions.currentInteractable = interactable;
            if (environmentInteractions.currentInteractable != null)
            {
                environmentInteractions.currentInteractable.SetOutline(true);
                uIManager.ShowHint(environmentInteractions.currentInteractable);
            }
            if (rl != null)
            {
                environmentInteractions.currentRl = rl;

                if (environmentInteractions.currentRope != null)
                {
                    environmentInteractions.currentRl.SetOutline(environmentInteractions.currentRope.ropeColor, true);                    
                }

                uIManager.ShowRopeHint(environmentInteractions.currentRope, environmentInteractions.currentRl);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("TriggerVisible")) return;

        RemoveFromList(other);

        if (floorButton != null)
        {
            if (triggerList.Count <= 0) floorButton.Deactivate();
        }

        if (environmentInteractions != null)
        {
            if (triggerList.Count <= 0)
            {
                if (environmentInteractions.currentInteractable != null)
                {
                    environmentInteractions.currentInteractable.SetOutline(false);                    
                }
                environmentInteractions.currentInteractable = null;

                if (environmentInteractions.currentRl != null)
                    environmentInteractions.currentRl.SetOutline(Color.black, false);
            }
            environmentInteractions.currentRl = null;

            uIManager.HideHint();
        }
    }

    private void AddToList(Collider col)
    {
        if (!triggerList.Contains(col))
        {
            triggerList.Add(col);
        }
    }

    public void RemoveFromList(Collider col)
    {
        if (triggerList.Contains(col) && !col.transform.IsChildOf(transform.parent))
        {
            triggerList.Remove(col);
        }
    }
}
