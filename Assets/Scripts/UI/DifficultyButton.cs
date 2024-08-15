using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    SaveManager saveManager;
    SceneLoader sceneLoader;

    [SerializeField] Button button;
    [SerializeField] Button startGameButton;
    [SerializeField] AnimationController confirmWindow;
    [SerializeField] Difficulty difficulty;

    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void Start()
    {
        button = GetComponent<Button>();
        SetUpOnClickEvents();
    }

    void SetUpOnClickEvents()
    {
        // if there is a save file
        if (saveManager.CurrentSaveFile != null)
        {
            // add animation to open confirm window
            button.onClick.AddListener(() => confirmWindow.PlayAnimations());
        }
        else
        {
            // if not, add event to start new game on this button
            button.onClick.AddListener(() => sceneLoader.StartNewGame(difficulty));
        }
    }

    public void AddOnClickEvents()
    {
        // if there is a save file
        if (saveManager.CurrentSaveFile != null)
        {
            // add start new game event on confirm button
            startGameButton.onClick.AddListener(() => sceneLoader.StartNewGame(difficulty));
        }
    }
}
