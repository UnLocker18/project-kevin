using System;
using UnityEngine;

public class AdditionalControls : MonoBehaviour
{
    [SerializeField] private bool enableCheats = true;

    private EnvironmentInteractions environmentInteractions;
    private DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;
    private CustomPauseMenu customPauseMenu;
    private bool controlsEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        environmentInteractions = GetComponent<EnvironmentInteractions>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        customPauseMenu = FindObjectOfType<CustomPauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) Debug.Log("any");
        if (enableCheats)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) environmentInteractions.SetPersonality(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) environmentInteractions.SetPersonality(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) environmentInteractions.SetPersonality(2);

            if (Input.GetKeyDown(KeyCode.M)) dialogueTrigger.TriggerDialogue(-1);
            if (Input.GetKeyDown(KeyCode.O)) OpenAllDoors();
        }

        if (controlsEnabled)
        {
            if (Input.GetButtonDown("Interact")) environmentInteractions.Interaction();

            if (Input.GetButtonDown("Interact2")) environmentInteractions.StickRope();

            if (Input.GetButtonDown("Interact3")) environmentInteractions.LeaveRope();

            if (Input.GetButtonDown("Pause")) customPauseMenu.ToggleMenu();

            if (Input.GetButtonDown("Restart"))
            {
                customPauseMenu.transform.Find("RestartLevel").gameObject.SetActive(true);
                Time.timeScale = 0f;                
            }
        }
        else if (Input.GetButtonDown("ContinueDialogue")) dialogueManager.DisplayNextSentence();
    }

    private void OpenAllDoors()
    {
        Door[] doors = FindObjectsOfType<Door>();

        foreach (Door door in doors)
        {
            door.Open();
        }
    }

    public void DisableControls()
    {
        controlsEnabled = false;
        GetComponent<SimpleThirdPRigidbodyController>().DisableControls();
    }

    public void EnableControls()
    {
        controlsEnabled = true;
        GetComponent<SimpleThirdPRigidbodyController>().EnableControls();
    }
}
