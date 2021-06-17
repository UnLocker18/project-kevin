using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private GameObject dialogueCanvas;
	private Text nameText;
	private Text dialogueText;

	public Animator animator;

	private Queue<string> sentences;
    private Queue<Dialogue> dialogues;

    private AdditionalControls additionalControls;
    private SimpleThirdPRigidbodyController characterController;
    private ImaginaryCharacter imaginaryCharacter;

    // Use this for initialization
    void Start () {
        dialogueCanvas = transform.GetChild(0).gameObject;
        dialogueCanvas.SetActive(true);

        sentences = new Queue<string>();
        dialogues = new Queue<Dialogue>();
        nameText = GameObject.FindGameObjectsWithTag("Dialogue")[0].GetComponent<Text>();
        dialogueText = GameObject.FindGameObjectsWithTag("Dialogue")[1].GetComponent<Text>();

        additionalControls = FindObjectOfType<AdditionalControls>();
        characterController = FindObjectOfType<SimpleThirdPRigidbodyController>();
    }

    public void StartDialogueChain(Dialogue[] dialogueChain, ImaginaryCharacter imaginaryCharacter = null)
    {
        if (imaginaryCharacter != null) this.imaginaryCharacter = imaginaryCharacter;

        dialogues.Clear();

        foreach (Dialogue dialogue in dialogueChain)
        {
            dialogues.Enqueue(dialogue);
        }

        NextDialogue();
    }

	public void StartDialogue (Dialogue dialogue)
	{
        additionalControls.DisableControls();
        characterController.DisableControls();

        animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			NextDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

    private void NextDialogue()
    {
        if (dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue dialogue = dialogues.Dequeue();

        StartDialogue(dialogue);
    }

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);

        characterController.EnableControls();
        additionalControls.EnableControls();

        if (imaginaryCharacter != null) imaginaryCharacter.Move();
    }

}
