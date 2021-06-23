using DG.Tweening;
using UnityEngine;
using System;

public class Door : MonoBehaviour
{
    [SerializeField] public GameObject[] activators;
    [SerializeField] private bool andMode = true;
    [SerializeField] private float animationSeconds = 1f;

    private bool isOpen = false;
    private Transform door1;
    private Transform door2;
    private Vector3 doorAngle;
    private int[] floorButtonNumbers;
    private int activeButtons = 0;
    private int neededActivators = 1;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        doorAngle = transform.eulerAngles;

        door1 = transform.Find("Door1");
        door2 = transform.Find("Door2");        

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
        if (audioManager == null) audioManager = FindObjectOfType<AudioManager>();

        if (door1 && door2)
        {
            door1.DORotate(doorAngle + new Vector3(0, 100, 0), animationSeconds);
            door2.DORotate(doorAngle + new Vector3(0, -100, 0), animationSeconds);
        }
        else transform.DORotate(doorAngle + new Vector3(0, 100, 0), animationSeconds);

        isOpen = true;

        audioManager.Play("doorOpen");
    }

    public void Close()
    {
        if (audioManager == null) audioManager = FindObjectOfType<AudioManager>();

        if (door1 && door2)
        {
            door1.DORotate(doorAngle, animationSeconds);
            door2.DORotate(doorAngle, animationSeconds);
        }
        else transform.DORotate(doorAngle, animationSeconds);

        isOpen = false;

        audioManager.Play("doorOpen");
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
