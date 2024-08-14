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

    public void Initialize(Difficulty _difficulty = Difficulty.Normal)
    {
        difficulty = _difficulty;

        switch (difficulty)
        {
            case Difficulty.Normal:
                maxLife = 5;
                break;
            case Difficulty.Hard:
                maxLife = 1;
                break;
            default:
                maxLife = 5;
                break;
        }

        areaIndex = 0;
        currentLife = maxLife;
        timer = 0;
    }
}
