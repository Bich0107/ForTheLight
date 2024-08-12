using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    const string path = "Assets/Saves";

    [SerializeField] List<SaveFile> saveFiles;
    [SerializeField] SaveFile currentSaveFile;
    public SaveFile CurrentSaveFile => currentSaveFile;

    int saveIndex = -1;

    public void CreateNewSavefile(Difficulty difficulty)
    {
        saveIndex++;
        SaveFile newSaveFile = SaveFile.CreateInstance<SaveFile>();
        newSaveFile.AreaIndex = 0;
        newSaveFile.Difficulty = difficulty;

        // Make sure the directory exists
        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder("Assets", "Saves");
        }

        // Save the ScriptableObject to the specified path
        string assetPath = AssetDatabase.GenerateUniqueAssetPath(path + "/SaveFile_" + saveIndex + ".asset");
        AssetDatabase.CreateAsset(newSaveFile, assetPath);

        // Save changes and refresh the AssetDatabase
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // set current save file
        currentSaveFile = newSaveFile;
        saveFiles.Add(newSaveFile);
    }

    public void ChangeSaveFile(int _index)
    {
        if (_index >= saveFiles.Count || _index == saveIndex) return;

        currentSaveFile = saveFiles[_index];
        saveIndex = _index;
    }

}
