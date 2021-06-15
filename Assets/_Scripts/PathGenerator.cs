using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private FloorManager floorManager;
    private Door door;
    //private GameObject activator;
    private Vector2 selfFloorCoordinates;
    private Vector2 activatorFloorCoordinates;

    private List<Path[]> pathList = new List<Path[]>();

    // Start is called before the first frame update
    void Start()
    {
        floorManager = FindObjectOfType<FloorManager>();
        door = GetComponent<Door>();
        //activator = door.activators[0];

        int i = 0;
        foreach (GameObject activator in door.activators)
        {
            Path[] path = GeneratePath(activator);

            path = SetPathTypes(path);
            pathList.Add(path);

            ApplyTextures(path);
            SubscribeToEvent(activator);
            
            i++;
        }        
    }

    private void ApplyTextures(Path[] path)
    {
        foreach (Path pathBlock in path)
        {
            floorManager.floorMatrix[pathBlock.x, pathBlock.y].SetTexture(pathBlock.type, false);
        }
    }

    private void SwitchEmission(int buttonNumber, bool isActive)
    {
        int i = 0;
        foreach (GameObject activator in door.activators)
        {
            if (activator.GetComponent<FloorButton>() != null && activator.GetComponent<FloorButton>().buttonNumber == buttonNumber) break;
            if (activator.GetComponent<PuzzleButton>() != null && activator.GetComponent<PuzzleButton>().buttonNumber == buttonNumber) break;

            i++;
        }

        Path[] path = pathList[i];

        foreach (Path pathBlock in path)
        {
            floorManager.floorMatrix[pathBlock.x, pathBlock.y].SetTexture(pathBlock.type, isActive);
        }
    }

    private void SubscribeToEvent(GameObject activator)
    {
        if (activator.GetComponent<FloorButton>() != null)
        {
            activator.GetComponent<FloorButton>().ButtonPress += SwitchEmission;
        }

        if (activator.GetComponent<PuzzleButton>() != null)
        {
            activator.GetComponent<PuzzleButton>().ButtonPress += SwitchEmission;
        }
    }

    private Path[] GeneratePath(GameObject activator)
    {
        selfFloorCoordinates = floorManager.WorldToFloorCoordinates(transform.position);
        activatorFloorCoordinates = floorManager.WorldToFloorCoordinates(activator.transform.position);

        int xMovement = (int)(selfFloorCoordinates.x - activatorFloorCoordinates.x);
        int yMovement = (int)(selfFloorCoordinates.y - activatorFloorCoordinates.y);

        Path[] path = new Path[Mathf.Abs(xMovement) + Mathf.Abs(yMovement) + 1];

        for (int i = 0; i <= Mathf.Abs(yMovement); i++)
        {
            string type = "";

            if (yMovement < 0) path[i] = new Path((int)activatorFloorCoordinates.x, -i + (int)activatorFloorCoordinates.y, type);
            else path[i] = new Path((int)activatorFloorCoordinates.x, i + (int)activatorFloorCoordinates.y, type);
        }

        for (int i = 0; i <= Mathf.Abs(xMovement); i++)
        {
            string type = "";

            if (xMovement < 0) path[i + Mathf.Abs(yMovement)] = new Path(-i + (int)activatorFloorCoordinates.x, yMovement + (int)activatorFloorCoordinates.y, type);
            else path[i + Mathf.Abs(yMovement)] = new Path(i + (int)activatorFloorCoordinates.x, yMovement + (int)activatorFloorCoordinates.y, type);
        }

        return path;
    }

    private Path[] SetPathTypes(Path[] path)
    {
        int j = 0;
        foreach (Path pathBlock in path)
        {
            int lastIndex = path.Length - 1;
            string type = "";

            if (j == 0)
            {
                bool top = false, bottom = false, left = false, right = false;

                if (path[j + 1].x < pathBlock.x) left = true;
                if (path[j + 1].x > pathBlock.x) right = true;
                if (path[j + 1].y < pathBlock.y) bottom = true;
                if (path[j + 1].y > pathBlock.y) top = true;

                if (top) type = "centertop";
                if (bottom) type = "centerbottom";
                if (left) type = "centerleft";
                if (right) type = "centerright";
            }
            else if (j == lastIndex)
            {
                bool top = false, bottom = false, left = false, right = false;

                if (path[j - 1].x < pathBlock.x) left = true;
                if (path[j - 1].x > pathBlock.x) right = true;
                if (path[j - 1].y < pathBlock.y) bottom = true;
                if (path[j - 1].y > pathBlock.y) top = true;

                if (top) type = "centertop";
                if (bottom) type = "centerbottom";
                if (left) type = "centerleft";
                if (right) type = "centerright";
            }
            else
            {
                bool top = false, bottom = false, left = false, right = false;

                if (path[j - 1].x < pathBlock.x || path[j + 1].x < pathBlock.x) left = true;
                if (path[j - 1].x > pathBlock.x || path[j + 1].x > pathBlock.x) right = true;

                if (path[j - 1].y < pathBlock.y || path[j + 1].y < pathBlock.y) bottom = true;
                if (path[j - 1].y > pathBlock.y || path[j + 1].y > pathBlock.y) top = true;

                if (left)
                {
                    if (right) type = "leftright";
                    if (top) type = "topleft";
                    if (bottom) type = "bottomleft";
                }
                if (right)
                {
                    if (top) type = "topright";
                    if (bottom) type = "bottomright";
                }
                if (top && bottom) type = "topbottom";                
            }

            pathBlock.type = type;

            j++;            
        }

        return path;
    }
}
