using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private FloorButton floorButton;    
    private EnvironmentInteractions environmentInteractions;
    private List<Collider> TriggerList = new List<Collider>();

    //private PuzzleButton puzzleButton;
    //private SmallBox smallBox;
    //private Rope rope;    
    //private Piston piston;

    // Start is called before the first frame update
    void Start()
    {
        environmentInteractions = GetComponentInParent<EnvironmentInteractions>();
        floorButton = GetComponentInParent<FloorButton>();

        //puzzleButton = GetComponentInParent<PuzzleButton>();
        //smallBox = GetComponentInParent<SmallBox>();
        //rope = GetComponentInParent<Rope>();
        //piston = GetComponentInParent<Piston>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (floorButton != null && other.gameObject.name != "Trigger")
        {
            if (!TriggerList.Contains(other))
            {
                TriggerList.Add(other);
            }

            if (TriggerList.Count > 0) floorButton.Activate();
        }

        //EnvironmentInteractions environmentInteractions = other.gameObject.GetComponent<EnvironmentInteractions>();

        PuzzleButton puzzleButton = other.gameObject.GetComponent<PuzzleButton>();
        SmallBox smallBox = other.gameObject.GetComponent<SmallBox>();
        Rope rope = other.gameObject.GetComponent<Rope>();
        Piston piston = other.gameObject.GetComponent<Piston>();

        if (environmentInteractions != null)
        {
            if (puzzleButton != null) environmentInteractions.currentPb = puzzleButton;
            if (smallBox != null) environmentInteractions.currentSb = smallBox;
            if (rope != null) environmentInteractions.currentRope = rope;
            if (piston != null) environmentInteractions.currentPiston = piston;
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

        //EnvironmentInteractions environmentInteractions = other.gameObject.GetComponent<EnvironmentInteractions>();

        PuzzleButton puzzleButton = other.gameObject.GetComponent<PuzzleButton>();
        SmallBox smallBox = other.gameObject.GetComponent<SmallBox>();
        Rope rope = other.gameObject.GetComponent<Rope>();
        Piston piston = other.gameObject.GetComponent<Piston>();

        if (environmentInteractions != null)
        {
            if (puzzleButton != null) environmentInteractions.currentPb = null;
            if (smallBox != null) environmentInteractions.currentSb = null;
            if (rope != null) environmentInteractions.currentRope = null;
            if (piston != null) environmentInteractions.currentPiston = null;
        }
    }
}
