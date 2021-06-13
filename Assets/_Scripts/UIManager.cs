using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Image ropeIndicator;

    private void Start()
    {
        ropeIndicator = GameObject.Find("RopeIndicator").GetComponent<Image>();
    }

    public void ShowRopeIndicator(Color color)
    {
        ropeIndicator.color = color;
        ropeIndicator.enabled = true;
    }

    public void HideRopeIndicator()
    {
        ropeIndicator.enabled = false;
    }
}
