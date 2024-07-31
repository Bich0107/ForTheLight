using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector cutscene;
    [SerializeField] List<ConversationSO> conversations;
    [SerializeField] DialogueSystem dialogueSystem;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player))
        {
            ShowStory();
        }
    }

    void ShowStory()
    {
        if (cutscene != null)
        {
            GameManager.Instance.SetPlayerControlStatus(false);
            cutscene.Play();
        }
        else
        {
            dialogueSystem.SetConversation(conversations);
        }
    }
}
