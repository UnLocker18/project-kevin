using DG.Tweening;
using UnityEngine;
using System;

public class Piston : MonoBehaviour
{
    [SerializeField] private GameObject[] activators;
    [SerializeField] private float animationSeconds = 0.3f;
    
    public bool isExtended = false;
    public float range = 1f;
    [SerializeField] private Transform head;
    [SerializeField] private Vector3 headPosition;
    [SerializeField] private int[] floorButtonNumbers;
    [SerializeField] private int activeButtons = 0;

    // Start is called before the first frame update
    void Start()
    {
        head = transform.GetChild(0);
        headPosition = head.localPosition;

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

    public void Extend()
    {
        head.DOLocalMoveX(headPosition.x + range, animationSeconds);
        isExtended = true;
    }

    public void Contract()
    {
        head.DOLocalMoveX(headPosition.x, animationSeconds);
        isExtended = false;
    }

    void Toggle(int buttonNumber, bool isActive)
    {
        //if (currentRope != null) currentRope.MoveRope();

        if (Array.IndexOf(floorButtonNumbers, buttonNumber) >= 0)
        {
            if (isActive)
            {
                activeButtons++;
            }
            else activeButtons--;
        }

        if (activeButtons >= activators.Length) Extend();
        else if (isExtended) Contract();        
    }
}
