using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Dialogue gameDialogue;
    private Conversation currentConversation;
    public void loadConversation(int i)
    {
        currentConversation = gameDialogue.gameDialogue[i];
    }

    public void startConversation()
    {

    }
}
