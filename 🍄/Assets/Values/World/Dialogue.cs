using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
struct DialogueBox
{
    private Image characterPortrait;
    string dialogueText;
}
public class Dialogue : ScriptableObject
{
    DialogueBox dialogueBox[];
}
