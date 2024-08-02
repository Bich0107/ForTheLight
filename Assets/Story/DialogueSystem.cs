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

    bool autoPlay = true;
    bool isTyping = false;

    public void StartConversation()
    {
        NextDialogue();
        StartCoroutine(CR_ShowDialogue());
    }

    public bool NextConversation()
    {
        if (conversationsIndex >= conversationsList.Count)
        {
            Debug.Log("all conversations end");
            return false;
        }
        else
        {
            currentConversation = conversationsList[conversationsIndex];
            dialoguesList = currentConversation.GetDialogues;
            conversationsIndex++;
        }

        dialogueIndex = 0;

        return true;
    }

    public bool NextDialogue()
    {
        if (isTyping) return false;

        if (dialogueIndex >= dialoguesList.Count)
        {
            return NextConversation();
        }

        currentDialogue = dialoguesList[dialogueIndex];
        dialogueIndex++;
        return true;
    }

    public void SetConversations(List<ConversationSO> _conversations)
    {
        conversationsList = _conversations;
        conversationsIndex = 0;
    }

    IEnumerator CR_ShowDialogue()
    {
        do
        {
            isTyping = true;

            string text = "";
            string content = currentDialogue.GetContentList[(int)language];
            DisplayActorName(currentDialogue.GetActor);
            for (int i = 0; i < content.Length; i++)
            {
                text += content[i];
                DisplayText(text);
                yield return new WaitForSeconds(currentDialogue.GetWordDelayTime);
            }

            isTyping = false;

            yield return new WaitForSeconds(currentDialogue.GetReadTime);
        } while (autoPlay && NextDialogue());
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
