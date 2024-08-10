using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Wave", menuName = "Enemy Wave")]
public class EnemyWaveSO : ScriptableObject {
    [SerializeField] List<WaveSectionSO> sectionList;
    [SerializeField] float delayToNextWave;

    public List<WaveSectionSO> GetSectionList => sectionList;
    public float GetDelay => delayToNextWave;
}