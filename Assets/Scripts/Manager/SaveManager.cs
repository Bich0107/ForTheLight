using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
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

        currentSaveFile = LoadSaveFile();
    }

    SaveFile LoadSaveFile()
    {
        string filePath = Application.persistentDataPath + "/SaveFile_1.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveFile scriptableObject = ScriptableObject.CreateInstance<SaveFile>();
            JsonUtility.FromJsonOverwrite(json, scriptableObject);
            Debug.Log("ScriptableObject loaded from " + filePath);
            return scriptableObject;
        }
        else
        {
            Debug.LogError("File not found at " + filePath);
            return null;
        }
    }

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

    public void ChangeSaveFile(int _index)
    {
        if (_index >= saveFiles.Count || _index == saveIndex) return;

        currentSaveFile = saveFiles[_index];
        saveIndex = _index;
    }
}
