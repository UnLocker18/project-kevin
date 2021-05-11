using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private FloorButton floorButton;
    private PuzzleButton puzzleButton;
    private SmallBox smallBox;
    private List<Collider> TriggerList = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        floorButton = GetComponentInParent<FloorButton>();
        puzzleButton = GetComponentInParent<PuzzleButton>();
        smallBox = GetComponentInParent<SmallBox>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (floorButton != null)
        {
            if (!TriggerList.Contains(other))
            {
                TriggerList.Add(other);
            }

            if (TriggerList.Count > 0) floorButton.Activate();
        }

        EnvironmentInteractions environmentInteractions = other.gameObject.GetComponent<EnvironmentInteractions>();

        if (environmentInteractions != null)
        {
            if (puzzleButton != null) environmentInteractions.currentPb = puzzleButton;
            if (smallBox != null) environmentInteractions.currentSb = smallBox;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (floorButton != null)
        {
            if (TriggerList.Contains(other))
            {
                TriggerList.Remove(other);
            }

            if (TriggerList.Count <= 0) floorButton.Deactivate();
        }

        EnvironmentInteractions environmentInteractions = other.gameObject.GetComponent<EnvironmentInteractions>();

        if (environmentInteractions != null)
        {
            if (puzzleButton != null) environmentInteractions.currentPb = null;
            if (smallBox != null) environmentInteractions.currentSb = null;
        }
    }
}
