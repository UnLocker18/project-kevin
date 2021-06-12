using DG.Tweening;
using UnityEngine;
using System;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject[] activators;
    [SerializeField] private bool andMode = true;
    [SerializeField] private float animationSeconds = 1f;

    private bool isOpen = false;
    private Vector3 doorAngle;
    private int[] floorButtonNumbers;
    private int activeButtons = 0;
    private int neededActivators = 1;

    // Start is called before the first frame update
    void Start()
    {
        doorAngle = transform.eulerAngles;

        floorButtonNumbers = new int[activators.Length];
        if (andMode) neededActivators = activators.Length;
        else neededActivators = 1;

        int i = 0;
        foreach (GameObject activator in activators)
        {
            if (activator.GetComponent<FloorButton>() != null)
            {
                activator.GetComponent<FloorButton>().ButtonPress += Toggle;
                floorButtonNumbers[i] = activator.GetComponent<FloorButton>().buttonNumber;
            }

            if (activator.GetComponent<PuzzleButton>() != null)
            {
                activator.GetComponent<PuzzleButton>().ButtonPress += Toggle;
                floorButtonNumbers[i] = activator.GetComponent<PuzzleButton>().buttonNumber;
            }

            i++;
        }        
    }

    public void Open()
    {        
        transform.DORotate(doorAngle + new Vector3(0, 120, 0), animationSeconds);
        isOpen = true;
    }

    public void Close()
    {
        transform.DORotate(doorAngle, animationSeconds);
        isOpen = false;
    }

    void Toggle(int buttonNumber, bool isActive)
    {
        if (Array.IndexOf(floorButtonNumbers, buttonNumber) >= 0)
        {
            if (isActive)
            {
                activeButtons++;
            }
            else activeButtons--;
        }

        if (activeButtons >= neededActivators) Open();
        else if (isOpen) Close();
    }
}
