using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    const string path = "Assets/Saves";
    public static bool Win = false;
    static int saveIndex = 0;

    static SaveManager Instance;

    [SerializeField] List<SaveFile> saveFiles;
    [SerializeField] SaveFile currentSaveFile;
    public SaveFile CurrentSaveFile => currentSaveFile;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

#if UNITY_EDITOR
    public void CreateNewSavefile(Difficulty difficulty)
    {
        // reset game state
        Win = false;

        // update save file index
        saveIndex++;

        // Make sure the directory exists
        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder("Assets", "Saves");
        }

        // create new save file
        SaveFile newSaveFile = SaveFile.CreateInstance<SaveFile>();
        newSaveFile.Initialize(difficulty);
        string fileName = "SaveFile_" + saveIndex + ".asset";

        // check if already exist a save file. if any, delete it
        AssetDatabase.DeleteAsset(path + "/" + fileName);

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
#else
    public void CreateNewSavefile(Difficulty difficulty)
    {
        // reset game state
        Win = false;

        // update save file index
        saveIndex++;

        // create new save file
        SaveFile newSaveFile = SaveFile.CreateInstance<SaveFile>();
        newSaveFile.Initialize(difficulty);
        string fileName = "SaveFile_" + saveIndex + ".json";

        // Serialize the ScriptableObject to a JSON string
        string json = JsonUtility.ToJson(newSaveFile);

        // Save the JSON string to a file
        string path = Application.persistentDataPath + "/" + fileName;
        File.WriteAllText(path, json);

        // set current save file
        currentSaveFile = newSaveFile;
        saveFiles.Add(newSaveFile);
    }
#endif

    public void ChangeSaveFile(int _index)
    {
        if (_index >= saveFiles.Count || _index == saveIndex) return;

        currentSaveFile = saveFiles[_index];
        saveIndex = _index;
    }
}
