using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private CustomPauseMenu menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = FindObjectOfType<CustomPauseMenu>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "MainCharacter")
        {
            menu.ShowEndLevel();
        }
    }
}
