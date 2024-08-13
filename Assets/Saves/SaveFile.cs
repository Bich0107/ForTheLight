using UnityEngine;

[CreateAssetMenu(fileName = "New save file", menuName = "Save file")]
public class SaveFile : ScriptableObject
{
    [SerializeField] int areaIndex;
    public int AreaIndex
    {
        get { return areaIndex; }
        set { areaIndex = value; }
    }

    [SerializeField] Difficulty difficulty;
    public Difficulty Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }

    [SerializeField] int maxLife;
    public int MaxLife
    {
        get { return maxLife; }
        set { maxLife = value; }
    }

    [SerializeField] int currentLife;
    public int CurrentLife
    {
        get { return currentLife; }
        set { currentLife = value; }
    }

    [SerializeField] float timer;
    public float Timer
    {
        get { return timer; }
        set { timer = value; }
    }
}
