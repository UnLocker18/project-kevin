using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	[SerializeField] private Dialogue[] dialogueChain;
    [SerializeField] private bool triggerOnStart = false;
    [SerializeField] private bool zoneTrigger = false;
    [SerializeField] private bool eventTrigger = false;
    [SerializeField] private int triggerPersonality = 2;

    private EnvironmentInteractions environmentInteractions;
    
    private void Start()
    {
        environmentInteractions = GameObject.FindGameObjectWithTag("Player").GetComponent<EnvironmentInteractions>();

        if (triggerOnStart) StartCoroutine("DelayedTrigger");
        if (eventTrigger) StartCoroutine("DelayedSubscription");
    }

    public void TriggerDialogue(int personality)
	{
        if (triggerPersonality != -1 && personality != triggerPersonality) return;

        FindObjectOfType<DialogueManager>().StartDialogueChain(dialogueChain);

        if (zoneTrigger) zoneTrigger = false;
    }

    public void TriggerTalk(ImaginaryCharacter imaginaryCharacter)
    {
        FindObjectOfType<DialogueManager>().StartDialogueChain(dialogueChain, imaginaryCharacter);
    }

    private IEnumerator DelayedTrigger()
    {
        yield return new WaitForSeconds(1f);
        TriggerDialogue(environmentInteractions.currentPersonality);
    }

    private IEnumerator DelayedSubscription()
    {
        yield return new WaitForSeconds(5f);
        if (environmentInteractions != null) environmentInteractions.ChangePersonality += TriggerDialogue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (zoneTrigger && other.gameObject.name == "MainCharacter")
        {
            TriggerDialogue(environmentInteractions.currentPersonality);            
        }
    }
}
