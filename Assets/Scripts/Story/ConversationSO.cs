using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New conversation", menuName = "Story/Conversation")]
public class ConversationSO : ScriptableObject
{
    [SerializeField] List<DialogueSO> dialogueList;

    public List<DialogueSO> GetDialogues => dialogueList;

}
