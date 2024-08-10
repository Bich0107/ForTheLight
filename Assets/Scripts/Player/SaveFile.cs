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
}
