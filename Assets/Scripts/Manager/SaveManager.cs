using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] List<SaveFile> saveFiles;
    [SerializeField] SaveFile currentSaveFile;
    public SaveFile CurrentSaveFile => currentSaveFile;

    int index = 0;

    public void ChangeSaveFile(int _index) {
        if (_index >= saveFiles.Count) return;

        currentSaveFile = saveFiles[_index];
        index = _index;
    }

}
