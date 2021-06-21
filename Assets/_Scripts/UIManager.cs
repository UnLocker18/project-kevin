using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] string[] hints;
    [SerializeField] Texture[] icons;

    private GameObject ropeIndicator;
    private GameObject hintGroup;
    private TMP_Text hintText;
    private RawImage hintIcon;

    private void Start()
    {
        //ropeIndicator = GameObject.Find("RopeIndicator");
        //hintText = GameObject.Find("Hint").GetComponent<TMP_Text>();

        ropeIndicator = transform.GetChild(0).gameObject;
        hintGroup = transform.GetChild(2).gameObject;
        hintText = transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>();
        hintIcon = transform.GetChild(2).GetChild(1).GetComponent<RawImage>();

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

    public void SetAttachedNumber(int count)
    {
        ropeIndicator.transform.Find("AttachedNumber").GetComponent<TMP_Text>().text = count.ToString();
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
            if (currentInteractable.GetType() == typeof(PuzzleButton) && currentInteractable.GetComponent<PuzzleButton>().isActive) SetHint(2);
            else return;
        }

        if (currentInteractable.GetType() == typeof(SmallBox)) SetHint(0);
        if (currentInteractable.GetType() == typeof(PuzzleButton))
        {
            if (currentInteractable.GetComponent<PuzzleButton>().isActive) SetHint(2);
            else SetHint(1);
        }
        if (currentInteractable.GetType() == typeof(PersonalityChanger)) SetHint(3);
        if (currentInteractable.GetType() == typeof(Rope)) SetHint(4);
        if (currentInteractable.GetType() == typeof(ImaginaryCharacter)) SetHint(8);
    }
    
    private void SetHint(int index)
    {
        hintGroup.SetActive(true);

        string hint = hints[index];
        hintText.text = hint.Remove(hint.Length - 1);

        int iconIndex = 0;
        if (hint[hint.Length - 1] == 'A') iconIndex = 0;
        if (hint[hint.Length - 1] == 'X') iconIndex = 1;

        //Debug.Log(icons[1]);
        //Debug.Log(icons[iconIndex]);

        hintIcon.texture = icons[iconIndex];
    }

    public void ShowRopeHint(Rope currentRope, RopeLinkable currentRopeLinkable)
    {
        if (currentRope != null)
        {
            if (!currentRopeLinkable.connectedRopes.Contains(currentRope)) SetHint(5);
            else SetHint(6);
        }
        else
        {
            if (currentRopeLinkable.connectedRopes.Count == 1)
            {
                SetHint(6);
            }
            else if (currentRopeLinkable.connectedRopes.Count > 1) SetHint(7);
        }
    }

    public void HideHint()
    {
        //hintText.text = "";
        hintGroup.SetActive(false);
    }
}
