using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private Transform floorOrigin;
    [SerializeField] private Transform floorMax;

    public TextureSwitcher[,] floorMatrix;

    // Start is called before the first frame update
    void Awake()
    {
        Vector2 matrixDimensions = WorldToFloorCoordinates(floorMax.position);

        floorMatrix = new TextureSwitcher[(int) matrixDimensions.x + 1, (int) matrixDimensions.y + 1];

        PopulateMatrix();
    }

    private void PopulateMatrix()
    {
        foreach (TextureSwitcher floorBlock in FindObjectsOfType<TextureSwitcher>())
        {
            Vector2 floorCoordinates = WorldToFloorCoordinates(floorBlock.transform.position);

            floorMatrix[(int) floorCoordinates.x, (int) floorCoordinates.y] = floorBlock;
        }
    }

    public Vector2 WorldToFloorCoordinates(Vector3 position)
    {
        Vector2 floorCoordinates;

        floorCoordinates.x = (int) ((position.x - floorOrigin.position.x) / 2);
        floorCoordinates.y = (int) ((position.z - floorOrigin.position.z) / 2);

        return floorCoordinates;
    }

    public TextureSwitcher BlockFromFloorCoordinates(int x, int y)
    {
        return floorMatrix[x, y];
    }
}
