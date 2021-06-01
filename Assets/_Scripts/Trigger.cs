using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private FloorButton floorButton;    
    private EnvironmentInteractions environmentInteractions;
    [SerializeField] private List<Collider> triggerList = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        environmentInteractions = GetComponentInParent<EnvironmentInteractions>();
        floorButton = GetComponentInParent<FloorButton>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.GetComponent<CharacterController>() && GetComponentInParent<CharacterController>()) return;

        if (!triggerList.Contains(other))
        {
            triggerList.Add(other);
        }

        if (floorButton != null && other.gameObject.name != "Trigger")
        {
            if (triggerList.Count > 0) floorButton.Activate();
        }
        
        Interactable interactable = triggerList[0].gameObject.GetComponent<Interactable>();
        RopeLinkable rl = null;
        
        foreach (Collider trigger in triggerList)
        {
            if (trigger.gameObject.GetComponent<RopeLinkable>() != null) rl = trigger.gameObject.GetComponent<RopeLinkable>();
        }        

        if (environmentInteractions != null)
        {
            environmentInteractions.currentInteractable = interactable;
            if (environmentInteractions.currentInteractable != null)
                environmentInteractions.currentInteractable.SetOutline(true);
            if (rl != null) environmentInteractions.currentRl = rl;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
        if (other.GetComponent<CharacterController>() && GetComponentInParent<CharacterController>()) return;

        if (triggerList.Contains(other) && !other.transform.IsChildOf(transform.parent))
        {
            triggerList.Remove(other);
        }

        if (floorButton != null)
        {
            if (triggerList.Count <= 0) floorButton.Deactivate();
        }

        if (environmentInteractions != null)
        {
            if (triggerList.Count <= 0)
            {
                if (environmentInteractions.currentInteractable != null)
                    environmentInteractions.currentInteractable.SetOutline(false);
                environmentInteractions.currentInteractable = null;
            }
            environmentInteractions.currentRl = null;
        }
    }
}
