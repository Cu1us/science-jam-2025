using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int conversation;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("works");
        FindObjectOfType<DialogueManager>().StartConversation(conversation);
    }
}
