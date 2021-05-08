using DG.Tweening;
using UnityEngine;
using System;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject[] activators;
    
    private int[] floorButtonNumbers;
    [SerializeField] private int activeButtons = 0;

    // Start is called before the first frame update
    void Start()
    {
        floorButtonNumbers = new int[activators.Length];

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        transform.DORotate(new Vector3(0, 120, 0), 1);
    }

    public void Close()
    {
        transform.DORotate(new Vector3(0, 0, 0), 1);
    }

    void Toggle(int buttonNumber, bool isActive)
    {
        if (Array.IndexOf(floorButtonNumbers, buttonNumber) >= 0)
        {
            if (isActive) activeButtons++;
            else activeButtons--;
        }

        if (activeButtons >= activators.Length) Open();
        else Close();
    }
}
