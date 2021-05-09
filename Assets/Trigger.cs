using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private FloorButton floorButton;
    private List<Collider> TriggerList = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        floorButton = GetComponentInParent<FloorButton>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!TriggerList.Contains(other))
        {
            TriggerList.Add(other);
        }

        if (TriggerList.Count > 0) floorButton.Activate();
    }

    private void OnTriggerExit(Collider other)
    {
        if (TriggerList.Contains(other))
        {
            TriggerList.Remove(other);
        }

        if (TriggerList.Count <= 0) floorButton.Deactivate();
    }
}
