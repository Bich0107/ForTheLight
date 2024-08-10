using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Section", menuName = "Wave Section")]
public class WaveSectionSO : ScriptableObject {
    [SerializeField] GameObject enemy;
    [SerializeField] int amount;
    [Tooltip("Offset from the spawner position to the real spawn postion")]
    [SerializeField] List<Vector2> offsetList;
    [SerializeField] float spawnDelay;
    [SerializeField] float endSectionDelay;

    public GameObject GetEnemy => enemy;
    public int GetAmount => amount;
    public List<Vector2> GetOffsetList => offsetList;
    public float GetSpawnDelay => spawnDelay;
    public float GetEndSectionDelay => endSectionDelay;
}