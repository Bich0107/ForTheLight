using UnityEngine;

[CreateAssetMenu(fileName = "New dialogue", menuName = "Story/Dialogue")]
public class DialogueSO : ScriptableObject
{
    [SerializeField] ActorEnum actor;
    [TextArea(3, 5)]
    [SerializeField] string[] contentList; 
    [Tooltip("Delay time (second) between each word when displayed on the canvas (the smaller this value the faster)")]
    [SerializeField] float wordDelayTime = 0.1f;

    public ActorEnum GetActor => actor;
    public string[] GetContentList => contentList;
    public float GetWordDelayTime => wordDelayTime;
}
