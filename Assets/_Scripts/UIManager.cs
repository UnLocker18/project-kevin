using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] string[] hints;

    private GameObject ropeIndicator;
    private TMP_Text hintText;

    private void Start()
    {
        ropeIndicator = GameObject.Find("RopeIndicator");
        hintText = GameObject.Find("Hint").GetComponent<TMP_Text>();

        HideHint();
    }

    public void ShowRopeIndicator(Color color)
    {
        ropeIndicator.GetComponentInChildren<RawImage>().color = color;
        ropeIndicator.GetComponentInChildren<RawImage>().enabled = true;

        bool first = true;
        foreach (Transform child in ropeIndicator.transform)
        {
            if (!first) child.gameObject.SetActive(true);
            first = false;
        }
    }

    public void HideRopeIndicator()
    {
        ropeIndicator.GetComponentInChildren<RawImage>().enabled = false;

        bool first = true;
        foreach (Transform child in ropeIndicator.transform)
        {
            if (!first) child.gameObject.SetActive(false);
            first = false;
        }
    }

    public void ShowHint(Interactable currentInteractable)
    {
        if (!currentInteractable.isInteractable)
        {
            if (currentInteractable.GetType() == typeof(PuzzleButton) && currentInteractable.GetComponent<PuzzleButton>().isActive) hintText.text = hints[2];
            else return;
        }

        if (currentInteractable.GetType() == typeof(SmallBox)) hintText.text = hints[0];
        if (currentInteractable.GetType() == typeof(PuzzleButton))
        {
            if (currentInteractable.GetComponent<PuzzleButton>().isActive) hintText.text = hints[2];
            else hintText.text = hints[1];
        }
        if (currentInteractable.GetType() == typeof(PersonalityChanger)) hintText.text = hints[3];
        if (currentInteractable.GetType() == typeof(Rope)) hintText.text = hints[4];
    }

    public void ShowRopeHint(Rope currentRope, RopeLinkable currentRopeLinkable)
    {
        if (currentRope != null)
        {
            if (!currentRopeLinkable.connectedRopes.Contains(currentRope)) hintText.text = hints[5];
            else hintText.text = hints[6];
        }
        else
        {
            if (currentRopeLinkable.connectedRopes.Count == 1)
            {
                hintText.text = hints[6];
            }
            else if (currentRopeLinkable.connectedRopes.Count > 1) hintText.text = hints[7];
        }
    }

    public void HideHint()
    {
        hintText.text = "";
    }
}
