using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector cutscene;
    [SerializeField] List<ConversationSO> conversations;
    [SerializeField] DialogueSystem dialogueSystem;
    bool triggered;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player))
        {
            ShowStory();
        }
    }

    void ShowStory()
    {
        if (cutscene == null || triggered) return;
        triggered = true;

        dialogueSystem.SetConversations(conversations);
        cutscene.Play();
    }
}
