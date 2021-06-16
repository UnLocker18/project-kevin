using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	[SerializeField] private Dialogue[] dialogueChain;
    [SerializeField] private bool triggerOnStart = false;

    private void Start()
    {
        if (triggerOnStart) StartCoroutine("DelayedTrigger");
    }

    public void TriggerDialogue ()
	{
        FindObjectOfType<DialogueManager>().StartDialogueChain(dialogueChain);        
	}

    private IEnumerator DelayedTrigger()
    {
        yield return new WaitForSeconds(1f);
        TriggerDialogue();
    }

}
