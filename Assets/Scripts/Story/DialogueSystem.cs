using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public enum Language
{
    Vietnamese,
    English
}

public class DialogueSystem : MonoSingleton<DialogueSystem>
{
    [SerializeField] Language language;
    [Header("Display")]
    [SerializeField] GameObject dialogueCanvas;
    [SerializeField] TextMeshProUGUI actorText;
    [SerializeField] TextMeshProUGUI contextText;
    [Header("Conversation settings")]
    [SerializeField] List<ConversationSO> conversationsList;
    [SerializeField] ConversationSO currentConversation;
    [SerializeField] int conversationsIndex = 0;
    [Header("Dialogue settings")]
    [SerializeField] List<DialogueSO> dialoguesList;
    [SerializeField] DialogueSO currentDialogue;
    [SerializeField] int dialogueIndex = 0;

    bool isTyping = false;

    void Start()
    {
        NextConversation();
    }

    public void NextConversation()
    {
        if (conversationsIndex >= conversationsList.Count)
        {
            Debug.Log("all conversations end");
        }
        else
        {
            currentConversation = conversationsList[conversationsIndex];
            dialoguesList = currentConversation.GetDialogues;
            conversationsIndex++;
        }

        dialogueIndex = 0;
    }

    public void NextDialogue()
    {
        if (isTyping) return;

        if (dialogueIndex >= dialoguesList.Count)
        {
            NextConversation();
        }
        else
        {
            currentDialogue = dialoguesList[dialogueIndex];
            dialogueIndex++;

            StartCoroutine(CR_ShowDialogue(currentDialogue));
        }
    }

    public void SetConversation(List<ConversationSO> _conversations)
    {
        conversationsList = _conversations;
        conversationsIndex = 0;
    }

    IEnumerator CR_ShowDialogue(DialogueSO dialogue)
    {
        isTyping = true;

        string text = "";
        string content = dialogue.GetContentList[(int)language];
        DisplayActorName(dialogue.GetActor);
        for (int i = 0; i < content.Length; i++)
        {
            text += content[i];
            DisplayText(text);
            yield return new WaitForSeconds(dialogue.GetWordDelayTime);
        }

        isTyping = false;
    }

    void DisplayText(string _text)
    {
        contextText.text = _text;
    }

    void DisplayActorName(ActorEnum _actor)
    {
        string actorName = Actors.ActorList[(int)_actor];
        actorText.text = actorName;
    }
}
