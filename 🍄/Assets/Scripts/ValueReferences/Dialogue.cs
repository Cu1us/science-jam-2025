using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public struct DialogueBox
{
    [SerializeField] public Sprite characterPortrait;
    [SerializeField] public string dialogueText;
}
[System.Serializable]
public struct Conversation
{
    [SerializeField] public DialogueBox[] dialogueBoxes;
}
[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public Conversation[] gameDialogue;
}
