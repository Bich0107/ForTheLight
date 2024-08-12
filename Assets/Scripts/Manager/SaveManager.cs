using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] List<SaveFile> saveFiles;
    [SerializeField] SaveFile currentSaveFile;
    public SaveFile CurrentSaveFile => currentSaveFile;

    int index = 0;

    public void CreateNewSavefile() {
        SaveFile newSaveFile = new SaveFile();
        currentSaveFile = newSaveFile;
        saveFiles.Add(newSaveFile);
        index = 0;
    }

    public void ChangeSaveFile(int _index) {
        if (_index >= saveFiles.Count || _index == index) return;

        currentSaveFile = saveFiles[_index];
        index = _index;
    }

}
