using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Dialogue gameDialogue;

    [SerializeField] TextMeshProUGUI textElement;
    private Conversation currentConversation;
    private bool conversing = false;
    private int step = 0;

    public void StartConversation(int i)
    {
        textElement.gameObject.SetActive(true);
        step = 0;
        conversing = true;
        currentConversation = gameDialogue.gameDialogue[i];
        textElement.text = currentConversation.dialogueBoxes[step].dialogueText;
    }
    private void Start()
    {
        StartConversation(0);
    }
    private void Update()
    {
        if (!conversing)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            step++;
            if (step >= currentConversation.dialogueBoxes.Length) 
            {
                conversing = false;
                textElement.gameObject.SetActive(false);
                return;
            }
            UpdateDialogueBox(currentConversation.dialogueBoxes[step]);
        }
    }

    private void UpdateDialogueBox(DialogueBox d)
    {
        textElement.text = d.dialogueText;
    }
}
