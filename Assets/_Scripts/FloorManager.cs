using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private Transform prova;

    [SerializeField] private Transform floorOrigin;
    [SerializeField] private Transform floorMax;

    public TextureSwitcher[,] floorMatrix;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 matrixDimensions = WorldToFloorCoordinates(floorMax.position);

        floorMatrix = new TextureSwitcher[(int) matrixDimensions.x + 1, (int) matrixDimensions.y + 1];

        Debug.Log(floorMatrix.GetLength(0));
        Debug.Log(floorMatrix.GetLength(1));

        PopulateMatrix();

        Debug.Log(BlockFromFloorCoordinates(8, 0).transform.position);
    }

    private void PopulateMatrix()
    {
        foreach (TextureSwitcher floorBlock in FindObjectsOfType<TextureSwitcher>())
        {
            Vector2 floorCoordinates = WorldToFloorCoordinates(floorBlock.transform.position);

            floorMatrix[(int)floorCoordinates.x, (int)floorCoordinates.y] = floorBlock;
        }
    }

    private Vector2 WorldToFloorCoordinates(Vector3 position)
    {
        Vector2 floorCoordinates;

        floorCoordinates.x = (position.x - floorOrigin.position.x) / 2;
        floorCoordinates.y = (position.z - floorOrigin.position.z) / 2;

        return floorCoordinates;
    }

    public TextureSwitcher BlockFromFloorCoordinates(int x, int y)
    {
        return floorMatrix[x, y];
    }
}
