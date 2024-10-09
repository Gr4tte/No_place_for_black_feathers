using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 2)]
public class DialogueScriptableObject : ScriptableObject
{
	public List<DialogueLine> dialogues;
}

[System.Serializable]
public struct DialogueLine
{
	public DialogueSpeaker speaker;
	public string text;
}

public enum DialogueSpeaker
{
	Crow = 0,
	Dove = 1
}

