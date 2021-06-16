using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Menu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "MainCharacter")
        {
            menu.transform.Find("EndLevel").gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
