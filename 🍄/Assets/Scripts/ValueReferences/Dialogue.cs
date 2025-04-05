using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
struct DialogueBox
{
    [SerializeField] private Sprite characterPortrait;
    [SerializeField] private string dialogueText;
}
[System.Serializable]
public struct Conversation
{
    [SerializeField] private DialogueBox[] dialogueBoxes;
}
[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public Conversation[] gameDialogue;
}
