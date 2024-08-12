using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    StartScene,
    PlayScene,
    GameOverScene
}

public class SceneLoader : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }

    public void StartNewGame(Difficulty difficulty)
    {
        if (saveManager.CurrentSaveFile == null)
        {
            saveManager.CreateNewSavefile(difficulty);
        }

        SceneManager.LoadScene((int)Scenes.PlayScene);
    }

    public void Continue()
    {

    }

    public void Quit()
    {

    }
}
