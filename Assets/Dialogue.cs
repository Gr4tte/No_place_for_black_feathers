using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
	[SerializeField] private GameObject levelManager;

	[SerializeField] private GameObject dialogueObject;
	[SerializeField] private GameObject crow;
	[SerializeField] private GameObject dove;
	[SerializeField] private TextMeshProUGUI text;

	[SerializeField] private DialogueScriptableObject dialogueScriptableObject;
	private List<DialogueLine> dialogueLines;

	private int dialogueIndex = 0;
	private DialogueSpeaker currentSpeaker;
	private string currentText;

	private bool hasBeenActivated = false;
	private bool isActive = false;

	private void Start()
	{
		dialogueObject.SetActive(value: false);
		dialogueLines = dialogueScriptableObject.dialogues;
	}

	private void OnTriggerStay2D(Collider2D collider)
	{
		if (hasBeenActivated || collider.gameObject.tag != "Player" || !collider.gameObject.GetComponent<moveTest>().isGrounded || isActive || collider.gameObject.GetComponent<moveTest>().dead) return;

		levelManager.GetComponent<LevelManager>().isInDialogue = true;

		GameObject.Find("birb").GetComponent<moveTest>().pauseForBirb(true);
		isActive = true;
		dialogueObject.SetActive(true);

		SetDialogueBox();
	}

	private void Update()
	{
		if (!isActive) return;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			dialogueIndex++;

			if (dialogueIndex == dialogueLines.Count)
			{
				hasBeenActivated = true;
				isActive = false;
				dialogueObject.SetActive(false);
				text.text = "";
				levelManager.GetComponent<LevelManager>().isInDialogue = false;
				GameObject.Find("birb").GetComponent<moveTest>().pauseForBirb(false);
				return;
			}

			SetDialogueBox();
		}
	}

	private void SetDialogueBox()
	{
		currentSpeaker = dialogueLines[dialogueIndex].speaker;
		currentText = dialogueLines[dialogueIndex].text;
		crow.SetActive(currentSpeaker == DialogueSpeaker.Crow);
		dove.SetActive(currentSpeaker == DialogueSpeaker.Dove);
		text.text = currentText;
	}

	private bool IsActive()
	{
		return isActive;
	}
}
