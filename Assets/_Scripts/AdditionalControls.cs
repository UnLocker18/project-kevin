using UnityEngine;

public class AdditionalControls : MonoBehaviour
{
    private EnvironmentInteractions environmentInteractions;
    private DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;
    private CustomPauseMenu customPauseMenu;

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
        if (Input.GetButtonDown("Interact")) environmentInteractions.Interaction();

        if (Input.GetButtonDown("Interact2")) environmentInteractions.StickRope();
        
        if (Input.GetButtonDown("Interact3")) environmentInteractions.LeaveRope();        

        if (Input.GetButtonDown("ContinueDialogue")) dialogueManager.DisplayNextSentence();

        if (Input.GetButtonDown("Pause")) customPauseMenu.ToggleMenu();

        if (Input.GetKeyDown(KeyCode.Alpha1)) environmentInteractions.SetPersonality(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) environmentInteractions.SetPersonality(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) environmentInteractions.SetPersonality(2);

        if (Input.GetKeyDown(KeyCode.M)) dialogueTrigger.TriggerDialogue();
    }
}
