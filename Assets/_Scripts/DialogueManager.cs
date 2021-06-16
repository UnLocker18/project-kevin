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

    private AdditionalControls additionalControls;
    private SimpleThirdPRigidbodyController characterController;

    // Use this for initialization
    void Start () {
        dialogueCanvas = transform.GetChild(0).gameObject;
        dialogueCanvas.SetActive(true);

        sentences = new Queue<string>();
        nameText = GameObject.FindGameObjectsWithTag("Dialogue")[0].GetComponent<Text>();
        dialogueText = GameObject.FindGameObjectsWithTag("Dialogue")[1].GetComponent<Text>();

        additionalControls = FindObjectOfType<AdditionalControls>();
        characterController = FindObjectOfType<SimpleThirdPRigidbodyController>();
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
			EndDialogue();
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

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);

        characterController.EnableControls();
    }

}
