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
        // check if player have a save file, if yes, show a confirm window -- ongoing
        // if (saveManager.CurrentSaveFile == null)
        // {
        //     saveManager.CreateNewSavefile(difficulty);
        // }

        saveManager.CreateNewSavefile(difficulty);

        LoadPlayScene();
    }

    public void Continue()
    {
        LoadPlayScene();
    }

    public void LoadStartMenuScene() => SceneManager.LoadScene((int)Scenes.StartScene);

    public void LoadPlayScene() => SceneManager.LoadScene((int)Scenes.PlayScene);

    public void LoadGameOverScene() => SceneManager.LoadScene((int)Scenes.GameOverScene);

    public void Quit()
    {
        Application.Quit();
    }
}
